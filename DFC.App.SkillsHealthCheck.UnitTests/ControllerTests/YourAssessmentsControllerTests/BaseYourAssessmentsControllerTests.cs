using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.Compui.Cosmos.Contracts;
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

        protected ILogger<SkillsHealthCheckController> Logger { get; }

        protected CmsApiClientOptions CmsApiClientOptions { get; set; }

        protected const string testContentId = "87dfb08e-13ec-42ff-9405-5bbde048827a";

        protected BaseYourAssessmentsControllerTests()
        {
            Logger = A.Fake<ILogger<SkillsHealthCheckController>>();
            FakeSharedContentItemDocumentService = A.Fake<IDocumentService<SharedContentItemModel>>();
            CmsApiClientOptions = new CmsApiClientOptions() { ContentIds = testContentId };
        }

        protected YourAssessmentsController BuildHomeController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new YourAssessmentsController(Logger, FakeSharedContentItemDocumentService, CmsApiClientOptions)
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
