using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.YourAssessmentsControllerTests
{
    [Trait("Category", "Your Assessements Unit Tests")]
    public class YourAssessmentsControllerTests : BaseYourAssessmentsControllerTests
    {
        [Fact]
        public void YourAssessmentsControllerHtmlHeadRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = controller.HtmlHead();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<HtmlHeadViewModel>(viewResult.ViewData.Model);
            Assert.Equal($"{YourAssessmentsController.PageTitle} | {YourAssessmentsController.DefaultPageTitleSuffix}", viewModel.Title);
        }

        [Fact]
        public void YourAssessmentsControllerBreadCrumbRequestReturnsSuccess()
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
        public async Task YourAssessmentsControllerBodyRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = await controller.Body();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(viewModel.AssessmentsPersonal);
            Assert.Equal(4, viewModel.AssessmentsPersonal.Count);
            Assert.Equal(6, viewModel.AssessmentsActivity.Count);
            Assert.Equal(0, viewModel.AssessmentsCompleted.Count);
        }

    }
}
