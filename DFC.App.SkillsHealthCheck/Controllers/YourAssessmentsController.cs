﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Filters;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    [ServiceFilter(typeof(SessionStateFilter))]
    public class YourAssessmentsController : BaseController<YourAssessmentsController>
    {
        public const string PageTitle = "Your assessments";
        private readonly ILogger<YourAssessmentsController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly IYourAssessmentsService yourAssessmentsService;

        public YourAssessmentsController(
            ILogger<YourAssessmentsController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            IYourAssessmentsService yourAssessmentsService)
        : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
            this.yourAssessmentsService = yourAssessmentsService;
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/document")]
        [Route("skills-health-check/your-assessments")]
        public async Task<IActionResult> Document()
        {
            var sessionDataModel = await GetSessionDataModel();
            var headViewModel = GetHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetBodyViewModel(sessionDataModel.DocumentId);

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HeadViewModel = headViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/head")]
        [Route("skills-health-check/your-assessments/download-document/head")]
        [Route("skills-health-check/your-assessments/return-to-assessment/head")]
        public IActionResult Head()
        {
            var viewModel = GetHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(Head)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/your-assessments/breadcrumb")]
        [Route("skills-health-check/your-assessments/download-document/breadcrumb")]
        [Route("skills-health-check/your-assessments/return-to-assessment/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/body")]
        public async Task<IActionResult> Body()
        {
            var sessionDataModel = await GetSessionDataModel();
            var viewModel = await GetBodyViewModel(sessionDataModel.DocumentId);
            return this.NegotiateContentResult(viewModel);
        }

        private async Task<BodyViewModel> GetBodyViewModel(long documentId, IEnumerable<string> selectedJobs = null)
        {
            var bodyViewModel = yourAssessmentsService.GetAssessmentListViewModel(documentId, selectedJobs);
            bodyViewModel.RightBarViewModel = await GetRightBarViewModel();

            return bodyViewModel;
        }

        private async Task<RightBarViewModel> GetRightBarViewModel()
        {
            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                    .GetByIdAsync(new Guid(cmsApiClientOptions.ContentIds));
            }

            var rightBarViewModel = new RightBarViewModel
            {
                ReturnToAssessmentViewModel = new ReturnToAssessmentViewModel
                {
                    ActionUrl = "/skills-health-check/your-assessments/return-to-assessment",
                },
            };
            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }

            return rightBarViewModel;
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/download-document")]
        public async Task<IActionResult> DownloadDocument([FromForm] BodyViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var formatter = yourAssessmentsService.GetFormatter(model.DownloadType);
                var selectedJobs = model.SkillsAssessmentComplete.HasValue && model.SkillsAssessmentComplete.Value
                    ? model.JobFamilyList?.SelectedJobs.ToList() ?? new List<string>() : new List<string>();
                var downloadDocumentResponse = await yourAssessmentsService.GetDownloadDocumentAsync(sessionDataModel, formatter, selectedJobs);
                if (downloadDocumentResponse.Success)
                {
                    return File(downloadDocumentResponse.DocumentBytes, formatter.ContentType, $"{downloadDocumentResponse.DocumentName}{formatter.FileExtension}");
                }
            }

            ViewData["selectionListError"] = ModelState.Where(val => val.Value.Errors.Count > 0).Any(md => md.Key.Contains("selectedjobs", StringComparison.InvariantCultureIgnoreCase));
            var bodyViewModel = await GetBodyViewModel(sessionDataModel.DocumentId, model.JobFamilyList.SelectedJobs);
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HeadViewModel = GetHeadViewModel(PageTitle),
                BreadcrumbViewModel = BuildBreadcrumb(),
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/download-document/body")]
        public async Task<IActionResult> DownloadDocumentBody([FromForm] BodyViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            var selectedJobs = new List<string>();
            if (model?.SkillsAssessmentComplete is true && model?.JobFamilyList?.SelectedJobs?.Any() is true)
            {
                if (model.JobFamilyList.SelectedJobs.Count() == 1 && model.JobFamilyList.SelectedJobs.First().Contains(','))
                {
                    model.JobFamilyList.SelectedJobs = model.JobFamilyList.SelectedJobs.First().Split(',');
                }

                ModelState.Clear();
                TryValidateModel(model);
                selectedJobs = model.JobFamilyList.SelectedJobs.ToList();
            }

            if (ModelState.IsValid)
            {
                var formatter = yourAssessmentsService.GetFormatter(model.DownloadType);
                var downloadDocumentResponse = await yourAssessmentsService.GetDownloadDocumentAsync(sessionDataModel, formatter, selectedJobs);
                if (downloadDocumentResponse.Success)
                {
                    return File(downloadDocumentResponse.DocumentBytes, formatter.ContentType, $"{downloadDocumentResponse.DocumentName}{formatter.FileExtension}");
                }
            }

            ViewData["selectionListError"] = ModelState.Where(val => val.Value.Errors.Count > 0).Any(md => md.Key.Contains("selectedjobs", StringComparison.InvariantCultureIgnoreCase));
            var bodyViewModel = await GetBodyViewModel(sessionDataModel.DocumentId, selectedJobs);
            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/return-to-assessment/body")]
        public async Task<IActionResult> ReturnToAssessment([FromForm] ReturnToAssessmentViewModel viewModel)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var referenceFound = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionDataModel, viewModel.ReferenceId);
                if (referenceFound)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    return Redirect(YourAssessmentsURL);
                }

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetBodyViewModel(sessionDataModel.DocumentId);
            viewModel.HasError = true;
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/return-to-assessment")]
        public async Task<IActionResult> ReturnToAssessmentDocument([FromForm] ReturnToAssessmentViewModel viewModel)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var referenceFound = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionDataModel, viewModel.ReferenceId);
                if (referenceFound)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    return Redirect(YourAssessmentsURL);
                }

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetBodyViewModel(sessionDataModel.DocumentId);
            viewModel.HasError = true;
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            var headViewModel = GetHeadViewModel(string.Empty);
            var breadcrumbViewModel = BuildBreadcrumb();
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HeadViewModel = headViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }
    }
}
