using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
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
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            IYourAssessmentsService yourAssessmentsService)

        : base(logger, sessionStateService)
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
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetBodyViewModel();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/htmlhead")]
        [Route("skills-health-check/your-assessments/download-document/htmlhead")]
        [Route("skills-health-check/your-assessments/return-to-assessment/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

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
            var viewModel = await GetBodyViewModel();
            return this.NegotiateContentResult(viewModel);
        }

        private async Task<BodyViewModel> GetBodyViewModel(IEnumerable<string> selectedJobs = null)
        {
            var sessionDataModel = await GetSessionDataModel();
            long documentId = 0;
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }
            else
            {
                documentId = sessionDataModel.DocumentId;
            }

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
        public async Task<IActionResult> DownloadDocument(BodyViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }
            else if (ModelState.IsValid)
            {
                var formatter = yourAssessmentsService.GetFormatter(model.DownloadType);
                var selectedJobs = model.SkillsAssessmentComplete.HasValue && model.SkillsAssessmentComplete.Value
                    ? model.JobFamilyList?.SelectedJobs.ToList() ?? new List<string>() : new List<string>();
                var downloadDocumentResponse = yourAssessmentsService.GetDownloadDocument(sessionDataModel, formatter, selectedJobs, out string documentTitle);
                if (downloadDocumentResponse.Success)
                {
                    return File(downloadDocumentResponse.DocumentBytes, formatter.ContentType, $"{documentTitle}{formatter.FileExtension}");
                }
            }

            ViewData["selectionListError"] = ModelState.Where(val => val.Value.Errors.Count > 0).Any(md => md.Key.Contains("selectedjobs", StringComparison.InvariantCultureIgnoreCase));
            var bodyViewModel = await GetBodyViewModel(model.JobFamilyList.SelectedJobs);
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = GetHtmlHeadViewModel(PageTitle),
                BreadcrumbViewModel = BuildBreadcrumb(),
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/download-document/body")]
        public async Task<IActionResult> DownloadDocumentBody(BodyViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            var selectedJobs = new List<string>();
            if (model.SkillsAssessmentComplete.HasValue && model.SkillsAssessmentComplete.Value && model.JobFamilyList != null && model.JobFamilyList.SelectedJobs.Any())
            {
                if (model.JobFamilyList.SelectedJobs.Count() == 1 && model.JobFamilyList.SelectedJobs.First().Contains(','))
                {
                    model.JobFamilyList.SelectedJobs = model.JobFamilyList.SelectedJobs.First().Split(',');
                }

                ModelState.Clear();
                TryValidateModel(model);
                selectedJobs = model.JobFamilyList?.SelectedJobs.ToList();
            }

            if (ModelState.IsValid)
            {
                var formatter = yourAssessmentsService.GetFormatter(model.DownloadType);
                var downloadDocumentResponse = yourAssessmentsService.GetDownloadDocument(sessionDataModel, formatter, selectedJobs, out string documentTitle);
                if (downloadDocumentResponse.Success)
                {
                    return File(downloadDocumentResponse.DocumentBytes, formatter.ContentType, $"{documentTitle}{formatter.FileExtension}");
                }
            }

            ViewData["selectionListError"] = ModelState.Where(val => val.Value.Errors.Count > 0).Any(md => md.Key.Contains("selectedjobs", StringComparison.InvariantCultureIgnoreCase));
            var bodyViewModel = await GetBodyViewModel(selectedJobs);
            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/return-to-assessment/body")]
        public async Task<IActionResult> ReturnToAssessment(ReturnToAssessmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var referenceFound = await ReferenceFound(viewModel.ReferenceId);
                if (referenceFound)
                {
                    return Redirect(YourAssessmentsURL);
                }

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetBodyViewModel();
            viewModel.HasError = true;
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/return-to-assessment")]
        public async Task<IActionResult> ReturnToAssessmentDocument(ReturnToAssessmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var referenceFound = await ReferenceFound(viewModel.ReferenceId);
                if (referenceFound)
                {
                    return Redirect(YourAssessmentsURL);
                }

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetBodyViewModel();
            viewModel.HasError = true;
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.Empty);
            var breadcrumbViewModel = BuildBreadcrumb();
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        private async Task<bool> ReferenceFound(string referenceId)
        {
            var response = yourAssessmentsService.GetSkillsDocumentByReference(referenceId);
            if (response.Success && response.DocumentId > 0)
            {
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                sessionStateModel.DocumentId = response.DocumentId;
                await SetSessionStateAsync(sessionStateModel);
                return true;
            }

            return false;
        }
    }
}
