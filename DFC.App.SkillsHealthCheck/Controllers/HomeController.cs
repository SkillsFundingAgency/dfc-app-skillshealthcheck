using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.Compui.Cosmos.Contracts;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.ViewModels.Home;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class HomeController : Controller
    {
        public const string RegistrationPath = "skills-health-check";
        public const string BradcrumbTitle = "Skills Health Check";
        public const string DefaultPageTitleSuffix = BradcrumbTitle + " | National Careers Service";

        private readonly ILogger<SkillsHealthCheckController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions _cmsApiClientOptions;

        public HomeController(
            ILogger<SkillsHealthCheckController> logger,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this._cmsApiClientOptions = cmsApiClientOptions;
        }

        [HttpGet]
        [Route("skills-health-check/home/document")]
        [Route("skills-health-check/document")]
        public async Task<IActionResult> Document()
        {
            var bodyViewModel = await GetHomeBodyViewModel().ConfigureAwait(false);
            var htmlHeadViewModel = GetHtmlHeadViewModel();
            var breadcrumbViewModel = BuildBreadcrumb();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                HomeBodyViewModel = bodyViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
            });
        }

        [Route("skills-health-check/home/breadcrumb")]
        [Route("skills-health-check/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        protected static BreadcrumbViewModel BuildBreadcrumb()
        {
            return new BreadcrumbViewModel
            {
                Breadcrumbs = new List<BreadcrumbItemViewModel>()
                {
                    new BreadcrumbItemViewModel()
                    {
                        Route = "/",
                        Title = "Home",
                    },
                },
            };
        }

        [HttpGet]
        [Route("skills-health-check/home/htmlhead")]
        [Route("skills-health-check/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel();

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        private HtmlHeadViewModel GetHtmlHeadViewModel()
        {
            return new HtmlHeadViewModel
            {
                CanonicalUrl = new Uri($"{Request.GetBaseAddress()}/skills-health-check", UriKind.RelativeOrAbsolute),
                Title = DefaultPageTitleSuffix,
            };
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
            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(_cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                      .GetByIdAsync(new Guid(_cmsApiClientOptions.ContentIds));
            }

            var rightBarViewModel = new RightBarViewModel();
            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }

            return new ViewModels.Home.BodyViewModel
            {
                YourAssessmentsURL = $"/{RegistrationPath}/your-assessment",
                RightBarViewModel = rightBarViewModel,
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
