using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.Common.SharedContent.Pkg.Netcore.Interfaces;
using DFC.Compui.Sessionstate;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.YourAssessmentsControllerTests
{
    public abstract class BaseYourAssessmentsControllerTests
    {
        protected ILogger<YourAssessmentsController> Logger { get; } = A.Fake<ILogger<YourAssessmentsController>>();

        protected ISessionStateService<SessionDataModel> SessionStateService { get; } = A.Fake<ISessionStateService<SessionDataModel>>();

        protected ISharedContentRedisInterface sharedContentRedisInterface;

        protected IOptions<SessionStateOptions> SessionStateOptions { get; } = Options.Create(new SessionStateOptions());

        protected IYourAssessmentsService FakeYourAssessmentsService { get; } = A.Fake<IYourAssessmentsService>();

        protected const string TestContentId = "87dfb08e-13ec-42ff-9405-5bbde048827a";

        protected YourAssessmentsController BuildHomeController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();
            sharedContentRedisInterface = A.Fake<ISharedContentRedisInterface>();
            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new YourAssessmentsController(Logger, SessionStateService, SessionStateOptions, sharedContentRedisInterface, FakeYourAssessmentsService)
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
