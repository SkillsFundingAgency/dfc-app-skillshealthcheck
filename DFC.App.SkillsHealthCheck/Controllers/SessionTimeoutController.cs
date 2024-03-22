using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels.SessionTimout;
using DFC.Compui.Sessionstate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class SessionTimeoutController : BaseController<SessionTimeoutController>
    {
        private readonly ILogger<SessionTimeoutController> logger;
        public const string PageTitle = "Session timed out";

        public SessionTimeoutController(
            ILogger<SessionTimeoutController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions) : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("skills-health-check/session-timeout")]
        public IActionResult Document()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.Empty);
            var breadcrumbViewModel = BuildBreadcrumb();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = new BodyViewModel
                {
                    HomePageUrl = HomeURL,
                },
            });
        }

        [HttpGet]
        [Route("skills-health-check/session-timeout/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/session-timeout/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/session-timeout/body")]
        public IActionResult Body()
        {
            return this.NegotiateContentResult(new BodyViewModel
            {
                HomePageUrl = HomeURL,
            });
        }

        [HttpGet]
        [Route("skills-health-check/session-timeout/pop-up")]
        public IActionResult Popup()
        {
            return this.NegotiateContentResult(new SessionTimeOutModel
            {
                Timeoutpop = SessionTimeoutURL,
            });
        }
    }
}
