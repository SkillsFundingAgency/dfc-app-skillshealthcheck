using System;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.Home;
using DFC.Compui.Sessionstate;

using FakeItEasy;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.HomeControllerTests
{

    [Trait("Category", "Home Controller Unit Tests")]
    public class HomeControllerTests : BaseHomeControllerTests
    {
        [Fact]
        public void HomeControllerHtmlHeadRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = controller.HtmlHead();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<HtmlHeadViewModel>(viewResult.ViewData.Model);
            Assert.Equal($"{YourAssessmentsController.DefaultPageTitleSuffix}", viewModel.Title);
        }

        [Fact]
        public void HomeControllerBreadCrumbRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = controller.Breadcrumb();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BreadcrumbViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(viewModel.Breadcrumbs);
            Assert.Single(viewModel.Breadcrumbs);
            Assert.Equal("Home", viewModel.Breadcrumbs[0].Title);
        }

        [Fact]
        public async Task HomeControllerBodyRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = await controller.Body();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task ReloadRequestWithKnownSessionIdRedirectsToYourAssessment()
        {
            // Arrange
            var sessionId = "some id";
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);
            controller.Request.Headers.Add(ConstantStrings.CompositeSessionIdHeaderName, Guid.NewGuid().ToString());
            var sessionState = new SessionStateModel<SessionDataModel>
            {
                State = new SessionDataModel { DocumentId = 1 },
            };

            A.CallTo(() => SessionStateService.GetAsync(A<Guid>.Ignored)).Returns(sessionState);
            var response = new GetSkillsDocumentIdResponse
            {
                Success = true,
                DocumentId = 12345,
            };
            A.CallTo(() => FakeSkillsHealthCheckService.GetSkillsDocumentByIdentifier(sessionId)).Returns(response);

            // Act
            var result = await controller.Reload(sessionId);

            // Assert
            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/your-assessments");

            A.CallTo(() => SessionStateService.GetAsync(A<Guid>.Ignored)).MustHaveHappenedTwiceExactly();
            A.CallTo(() => SessionStateService.SaveAsync(A<SessionStateModel<SessionDataModel>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReloadRequestWithUnknownSessionIdRedirectsToError()
        {
            // Arrange
            var sessionId = "some id";
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);
            controller.Request.Headers.Add(ConstantStrings.CompositeSessionIdHeaderName, Guid.NewGuid().ToString());
            var sessionState = new SessionStateModel<SessionDataModel>
            {
                State = new SessionDataModel { DocumentId = 1 },
            };

            A.CallTo(() => SessionStateService.GetAsync(A<Guid>.Ignored)).Returns(sessionState);
            var response = new GetSkillsDocumentIdResponse
            {
                Success = false,
                DocumentId = 0,
            };
            A.CallTo(() => FakeSkillsHealthCheckService.GetSkillsDocumentByIdentifier(sessionId)).Returns(response);

            // Act
            var result = await controller.Reload(sessionId);

            // Assert
            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/alerts/500?errorcode=saveProgressResponse");

            A.CallTo(() => SessionStateService.GetAsync(A<Guid>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => SessionStateService.SaveAsync(A<SessionStateModel<SessionDataModel>>.Ignored)).MustNotHaveHappened();
        }
    }
}
