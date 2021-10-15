using DFC.App.SkillsHealthCheck.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
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
}
