using System;
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
using DFC.Common.SharedContent.Pkg.Netcore;
using DFC.Common.SharedContent.Pkg.Netcore.Interfaces;
using DFC.Common.SharedContent.Pkg.Netcore.Model.ContentItems.SharedHtml;
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
        private const string SharedContentStaxId = "2c9da1b3-3529-4834-afc9-9cd741e59788";
        private readonly ILogger<YourAssessmentsController> logger;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly IYourAssessmentsService yourAssessmentsService;
        private readonly ISharedContentRedisInterface sharedContentRedis;

        public YourAssessmentsController(
            ILogger<YourAssessmentsController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            ISharedContentRedisInterface sharedContentRedis,
            CmsApiClientOptions cmsApiClientOptions,
            IYourAssessmentsService yourAssessmentsService)
        : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.sharedContentRedis = sharedContentRedis;
            this.cmsApiClientOptions = cmsApiClientOptions;
            this.yourAssessmentsService = yourAssessmentsService;
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/document")]
        [Route("skills-health-check/your-assessments")]
        public async Task<IActionResult> Document()
        {
            var sessionDataModel = await GetSessionDataModel();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetBodyViewModel(sessionDataModel.DocumentId);

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
            var speakToAnAdviser = await sharedContentRedis.GetDataAsync<SharedHtml>("SharedContent/" + SharedContentStaxId);

            var rightBarViewModel = new RightBarViewModel
            {
                ReturnToAssessmentViewModel = new ReturnToAssessmentViewModel
                {
                    ActionUrl = "/skills-health-check/your-assessments/return-to-assessment",
                },
            };
            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser.Html;
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
                HtmlHeadViewModel = GetHtmlHeadViewModel(PageTitle),
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
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.Empty);
            var breadcrumbViewModel = BuildBreadcrumb();
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }
    }
}
