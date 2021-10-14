using DFC.App.SkillsHealthCheck.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.ViewModels.Home;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.HomeControllerTests
{
    [Trait("Category", "Home Controller Unit Tests")]
    public class HomeControllerErrorTests : BaseHomeControllerTests
    {
        [Fact]
        public void HomeControllerErrorTestsReturnsSuccess()
        {
            // Arrange
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            // Act
            var result = controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _ = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);
        }
    }

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
            Assert.Equal(HomeController.DefaultPageTitleSuffix, viewModel.Title);
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

            var result = await controller.Body().ConfigureAwait(false);

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);
            Assert.Equal($"/{HomeController.RegistrationPath}/your-assessment", viewModel.YourAssessmentsURL);
        }
    }
}
