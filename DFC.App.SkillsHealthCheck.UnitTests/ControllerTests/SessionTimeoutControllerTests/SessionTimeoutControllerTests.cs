using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;
using DFC.App.SkillsHealthCheck.ViewModels.SessionTimout;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SessionTimeoutControllerTests
{
    [Trait("Category", "Session Timeout Controller Unit Tests")]
    public class SessionTimeoutControllerTests : BaseSessionTimeoutControllerTests
    {
        [Fact]
        public void HtmlHeadRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.HtmlHead();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<HtmlHeadViewModel>().Which;
            Assert.Equal($"{SessionTimeoutController.PageTitle} | {SessionTimeoutController.DefaultPageTitleSuffix}", viewModel.Title);
        }

        [Fact]
        public void BreadCrumbRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.Breadcrumb();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<BreadcrumbViewModel>().Which;
            viewModel.Breadcrumbs.Should().NotBeNull();
            viewModel.Breadcrumbs.Should().HaveCount(2);
            viewModel.Breadcrumbs![0].Title.Should().Be("Home");
            viewModel.Breadcrumbs![1].Title.Should().Be("Skills assessment");
        }

        [Theory]
        [InlineData("/skills-health-check/home")]
        public async Task BodyGetRequestReturnsSuccessAndCorrectReturnLink(string expectedReturnLink)
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.Body();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var model = viewResult.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<BodyViewModel>().Which;

            model.HomePageUrl.Should().Be(expectedReturnLink);
        }
    }
}
