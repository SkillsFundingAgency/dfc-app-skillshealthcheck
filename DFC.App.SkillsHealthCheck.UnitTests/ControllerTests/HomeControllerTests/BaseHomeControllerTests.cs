using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;

using FakeItEasy;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.HomeControllerTests
{
    public abstract class BaseHomeControllerTests
    {
        protected IDocumentService<SharedContentItemModel> FakeSharedContentItemDocumentService { get; }

        protected ILogger<HomeController> Logger { get; }

        protected ISessionStateService<SessionDataModel> SessionStateService { get; }

        protected CmsApiClientOptions CmsApiClientOptions { get; set; }

        protected ISkillsHealthCheckService FakeSkillsHealthCheckService { get; }

        protected IYourAssessmentsService FakeYourAssessmentsService { get; }

        protected const string testContentId = "87dfb08e-13ec-42ff-9405-5bbde048827a";

        protected BaseHomeControllerTests()
        {
            Logger = A.Fake<ILogger<HomeController>>();
            SessionStateService = A.Fake<ISessionStateService<SessionDataModel>>();
            FakeSharedContentItemDocumentService = A.Fake<IDocumentService<SharedContentItemModel>>();
            FakeSkillsHealthCheckService = A.Fake<ISkillsHealthCheckService>();
            FakeYourAssessmentsService = A.Fake<IYourAssessmentsService>();
            CmsApiClientOptions = new CmsApiClientOptions() { ContentIds = testContentId };

        }

        protected HomeController BuildHomeController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new HomeController(Logger, SessionStateService, FakeSharedContentItemDocumentService, CmsApiClientOptions, FakeSkillsHealthCheckService, FakeYourAssessmentsService)
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
