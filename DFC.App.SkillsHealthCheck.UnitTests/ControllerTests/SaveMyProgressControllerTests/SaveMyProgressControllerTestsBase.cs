using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.Compui.Sessionstate;

using FakeItEasy;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    public class SaveMyProgressControllerTestsBase
    {
        protected ILogger<SaveMyProgressController> Logger { get; } = A.Fake<ILogger<SaveMyProgressController>>();

        protected ISessionStateService<SessionDataModel> SessionStateService { get; } = A.Fake<ISessionStateService<SessionDataModel>>();

        protected ISkillsHealthCheckService SkillsHealthCheckService { get; } = A.Fake<ISkillsHealthCheckService>();

        protected SaveMyProgressController BuildController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new SaveMyProgressController(Logger, SessionStateService, SkillsHealthCheckService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                },
                TempData = new TempDataDictionary(httpContext, A.Fake<ITempDataProvider>()),
            };

            return controller;
        }
    }
}
