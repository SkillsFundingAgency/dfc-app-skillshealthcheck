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

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.YourAssessmentsControllerTests
{
    public abstract class BaseYourAssessmentsControllerTests
    {
        protected IDocumentService<SharedContentItemModel> FakeSharedContentItemDocumentService { get; }

        protected ILogger<YourAssessmentsController> Logger { get; }

        protected ISessionStateService<SessionDataModel> SessionStateService { get; }

        protected CmsApiClientOptions CmsApiClientOptions { get; set; }

        protected IYourAssessmentsService FakeYourAssessmentService { get; }
        protected ISkillsHealthCheckService FakeSkillsHealthCheckService { get; }

        protected const string testContentId = "87dfb08e-13ec-42ff-9405-5bbde048827a";

        protected BaseYourAssessmentsControllerTests()
        {
            Logger = A.Fake<ILogger<YourAssessmentsController>>();
            SessionStateService = A.Fake<ISessionStateService<SessionDataModel>>();
            FakeSharedContentItemDocumentService = A.Fake<IDocumentService<SharedContentItemModel>>();
            CmsApiClientOptions = new CmsApiClientOptions() { ContentIds = testContentId };
            FakeYourAssessmentService = A.Fake<IYourAssessmentsService>();
            FakeSkillsHealthCheckService = A.Fake<ISkillsHealthCheckService>();
        }

        protected YourAssessmentsController BuildHomeController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new YourAssessmentsController(Logger, SessionStateService, FakeSharedContentItemDocumentService, CmsApiClientOptions, FakeYourAssessmentService, FakeSkillsHealthCheckService)
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
