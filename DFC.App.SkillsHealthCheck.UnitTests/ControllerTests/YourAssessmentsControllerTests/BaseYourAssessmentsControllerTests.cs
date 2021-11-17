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
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.YourAssessmentsControllerTests
{
    public abstract class BaseYourAssessmentsControllerTests
    {
        protected ILogger<YourAssessmentsController> Logger { get; } = A.Fake<ILogger<YourAssessmentsController>>();

        protected ISessionStateService<SessionDataModel> SessionStateService { get; } = A.Fake<ISessionStateService<SessionDataModel>>();

        protected IOptions<SessionStateOptions> SessionStateOptions { get; } = A.Fake<IOptions<SessionStateOptions>>();

        protected IDocumentService<SharedContentItemModel> FakeSharedContentItemDocumentService { get; } = A.Fake<IDocumentService<SharedContentItemModel>>();

        protected CmsApiClientOptions CmsApiClientOptions { get;  } = new CmsApiClientOptions() { ContentIds = testContentId };

        protected IYourAssessmentsService FakeYourAssessmentsService { get; } = A.Fake<IYourAssessmentsService>();

        protected const string testContentId = "87dfb08e-13ec-42ff-9405-5bbde048827a";

        protected YourAssessmentsController BuildHomeController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new YourAssessmentsController(Logger, SessionStateService, SessionStateOptions, FakeSharedContentItemDocumentService, CmsApiClientOptions, FakeYourAssessmentsService)
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
