using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;
using DFC.Compui.Sessionstate;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class SaveMyProgressController : BaseController<SaveMyProgressController>
    {
        public const string PageTitle = "Save My Progress";

        private readonly ILogger<SaveMyProgressController> logger;

        public SaveMyProgressController(ILogger<SaveMyProgressController> logger, ISessionStateService<SessionDataModel> sessionStateService) : base(logger, sessionStateService)
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
        [Route("skills-health-check/save-my-progress/")]
        [Route("skills-health-check/save-my-progress/document")]
        public IActionResult Document([FromQuery] string? type)
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                SaveMyProgressViewModel = GetSaveMyProgressViewModel(type),
            });
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/")]
        [Route("skills-health-check/save-my-progress/document")]
        public IActionResult Document(SaveMyProgressViewModel model, [FromQuery] string? type)
        {
            if (!ModelState.IsValid)
            {
                var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
                var breadcrumbViewModel = BuildBreadcrumb();

                return this.NegotiateContentResult(new DocumentViewModel
                {
                    HtmlHeadViewModel = htmlHeadViewModel,
                    BreadcrumbViewModel = breadcrumbViewModel,
                    SaveMyProgressViewModel = GetSaveMyProgressViewModel(type),
                });
            }

            return Redirect("/skills-health-check/save-my-progress/getcode");
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/body")]
        public IActionResult Body([FromQuery] string? type)
        {
            var model = GetSaveMyProgressViewModel(type);
            return this.NegotiateContentResult(model);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/body")]
        public async Task<IActionResult> Body(SaveMyProgressViewModel model, [FromQuery] string? type)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = GetSaveMyProgressViewModel(type);
                return this.NegotiateContentResult(viewModel);
            }

            return Redirect("/skills-health-check/save-my-progress/getcode");
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/getcode")]
        [Route("skills-health-check/save-my-progress/getcode/document")]
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

        private static SaveMyProgressViewModel GetSaveMyProgressViewModel(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return new SaveMyProgressViewModel
                {
                    ReturnLink = "/skills-health-check/your-assessments",
                    ReturnLinkText = "Return to your skills health check",
                };
            }
            else
            {
                return new SaveMyProgressViewModel
                {
                    ReturnLink = $"/skills-health-check/question?assessmentType={type}",
                    ReturnLinkText = "Return to your skills health check assessment",
                };
            }
        }
    }
}
