using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.Common.SharedContent.Pkg.Netcore.Interfaces;
using DFC.Compui.Sessionstate;

using FakeItEasy;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.HomeControllerTests
{
    public abstract class BaseHomeControllerTests
    {
        protected ILogger<HomeController> Logger { get; } = A.Fake<ILogger<HomeController>>();

        protected ISessionStateService<SessionDataModel> SessionStateService { get; } = A.Fake<ISessionStateService<SessionDataModel>>();

        protected IOptions<SessionStateOptions> SessionStateOptions { get; } = Options.Create(new SessionStateOptions());

        protected ISkillsHealthCheckService FakeSkillsHealthCheckService { get; } = A.Fake<ISkillsHealthCheckService>();

        protected IYourAssessmentsService FakeYourAssessmentsService { get; } = A.Fake<IYourAssessmentsService>();

        protected ISharedContentRedisInterface SharedContentRedisInterface { get; } = A.Fake<ISharedContentRedisInterface>();

        protected const string TestContentId = "87dfb08e-13ec-42ff-9405-5bbde048827a";

        protected HomeController BuildHomeController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new HomeController(Logger, SessionStateService, SessionStateOptions, SharedContentRedisInterface, FakeSkillsHealthCheckService, FakeYourAssessmentsService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                },
            };

            return controller;
        }
    }
}
