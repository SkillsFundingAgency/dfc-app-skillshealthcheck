using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.Compui.Cosmos.Contracts;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels.Home;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : BaseController
    {
        public const string PageTitle = "Home";

        private readonly ILogger<SkillsHealthCheckController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;

        public HomeController(
            ILogger<SkillsHealthCheckController> logger,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            ISkillsHealthCheckService skillsHealthCheckService)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
            this.skillsHealthCheckService = skillsHealthCheckService;
        }

        [HttpGet]
        [Route("skills-health-check/home/document")]
        [Route("skills-health-check/document")]
        public async Task<IActionResult> Document()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetHomeBodyViewModel().ConfigureAwait(false);

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpGet]
        [Route("skills-health-check/home/htmlhead")]
        [Route("skills-health-check/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/home/breadcrumb")]
        [Route("skills-health-check/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/home/body")]
        [Route("skills-health-check/body")]
        public async Task<IActionResult> Body()
        {
            var viewModel = await GetHomeBodyViewModel().ConfigureAwait(false);
            return this.NegotiateContentResult(viewModel);
        }

        private async Task<BodyViewModel> GetHomeBodyViewModel()
        {
            var viewModel = new BodyViewModel
            {
                YourAssessmentsURL = $"/{RegistrationPath}/your-assessments",
            };
            var apiResult = skillsHealthCheckService.GetListTypeFields(new GetListTypeFieldsRequest
            {
                DocumentType = Constants.SkillsHealthCheck.DocumentType,

            });
            if (apiResult.Success)
            {
                viewModel.ListTypeFields = apiResult.TypeFields;
                viewModel.ListTypeFieldsString = string.Join(",", apiResult.TypeFields);
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

            viewModel.RightBarViewModel = rightBarViewModel;

            return viewModel;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
