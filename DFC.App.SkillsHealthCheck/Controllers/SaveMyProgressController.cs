using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class SaveMyProgressController : BaseController
    {
        public const string PageTitle = "Save My Progress";

        private readonly ILogger<SaveMyProgressController> logger;

        public SaveMyProgressController(ILogger<SaveMyProgressController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/save-my-progress/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/document")]
        [Route("skills-health-check/save-my-progress/")]
        public IActionResult Document()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                SaveMyProgressViewModel = new SaveMyProgressViewModel(),
            });
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/document")]
        [Route("skills-health-check/save-my-progress/")]
        public IActionResult Document(SaveMyProgressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
                var breadcrumbViewModel = BuildBreadcrumb();

                return this.NegotiateContentResult(new DocumentViewModel
                {
                    HtmlHeadViewModel = htmlHeadViewModel,
                    BreadcrumbViewModel = breadcrumbViewModel,
                    SaveMyProgressViewModel = new SaveMyProgressViewModel(),
                });
            }

            return RedirectToAction("GetCode");
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/body")]
        public IActionResult Body()
        {
            return this.NegotiateContentResult(null);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/body")]
        public async Task<IActionResult> Body(SaveMyProgressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.NegotiateContentResult(null);
            }

            return RedirectToAction("GetCode");
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/getcode/document")]
        [Route("skills-health-check/save-my-progress/getcode")]
        public IActionResult GetCode()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(new GetCodeViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
            });
        }
    }
}
