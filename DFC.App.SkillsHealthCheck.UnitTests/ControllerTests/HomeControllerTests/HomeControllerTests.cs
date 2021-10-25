using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.Home;

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
    }
}
