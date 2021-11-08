using System.Net.Mime;

using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Enums;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class SaveMyProgressControllerTests : SaveMyProgressControllerTestsBase
    {
        [Fact]
        public void HtmlHeadRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.HtmlHead();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<HtmlHeadViewModel>().Which;
            Assert.Equal($"{SaveMyProgressController.PageTitle} | {SaveMyProgressController.DefaultPageTitleSuffix}", viewModel.Title);
        }

        [Fact]
        public void BreadCrumbRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.Breadcrumb();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<BreadcrumbViewModel>().Which;
            viewModel.Breadcrumbs.Should().NotBeNull();
            viewModel.Breadcrumbs.Should().HaveCount(1);
            viewModel.Breadcrumbs[0].Title.Should().Be("Home");
        }

        [Theory]
        [InlineData(null, "/skills-health-check/your-assessments", "Return to your skills health check")]
        [InlineData("Skills", "/skills-health-check/question?assessmentType=Skills", "Return to your skills health check assessment")]
        public void BodyGetRequestReturnsSuccessAndCorrectReturnLink(string? type, string expectedReturnLink, string expectedReturnLinkText)
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.Body(type);

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var model = viewResult.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<SaveMyProgressViewModel>().Which;

            model.ReturnLink.Should().Be(expectedReturnLink);
            model.ReturnLinkText.Should().Be(expectedReturnLinkText);
        }

        [Theory]
        [InlineData(SaveMyProgressOption.Email, "/skills-health-check/save-my-progress/email")]
        [InlineData(SaveMyProgressOption.ReferenceCode, "/skills-health-check/save-my-progress/getcode")]
        public void BodyPostRequestRedirectsToGetCode(SaveMyProgressOption option, string expectedUrl)
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var model = new SaveMyProgressViewModel { SelectedOption = option };
            var result = controller.Body(model, null);

            result.Should().NotBeNull();
            var redirectResult = result.Should().BeOfType<RedirectResult>().Which;
            redirectResult.Url.Should().Be(expectedUrl);
        }
    }
}
