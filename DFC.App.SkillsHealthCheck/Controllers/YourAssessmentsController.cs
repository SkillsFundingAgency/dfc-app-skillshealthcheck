using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
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
            IYourAssessmentsService yourAssessmentsService,)

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
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/your-assessments/breadcrumb")]
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

        private async Task<BodyViewModel> GetBodyViewModel()
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

            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                    .GetByIdAsync(new Guid(cmsApiClientOptions.ContentIds));
            }

            var rightBarViewModel = new RightBarViewModel();
            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }

            var bodyViewModel = yourAssessmentsService.GetAssessmentListViewModel(documentId);
            bodyViewModel.RightBarViewModel = rightBarViewModel;

            return bodyViewModel;
        }

        [HttpPost]
        [Route("skills-health-check/your-assessments/download-document")]
        [Route("skills-health-check/your-assessments/download-document/body")]
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
                var downloadDocumentResponse = yourAssessmentsService.GetDownloadDocument(sessionDataModel, formatter, out string documentTitle);
                if (downloadDocumentResponse.Success)
                {
                    return File(downloadDocumentResponse.DocumentBytes, formatter.ContentType, $"{documentTitle}{formatter.FileExtension}");
                }
            }

            var errors = ModelState.Where(val => val.Value.Errors.Count > 0).Select(md => md.Key);

            var errorList = errors as IList<string> ?? errors.ToList();
            ViewData["selectionListError"] = errorList.Any(key => key.ToLower().Contains("selectedjobs"));

            //TODO: need to error properly
            return this.NegotiateContentResult(model);

            //return View("AssessmentsList", GetAssessmentListViewModel(string.Empty, default(long), string.Empty, model.JobFamilyList?.SelectedJobs));
        }
    }
}
