using System.Net.Mime;

using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.ViewModels;

using FakeItEasy;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class SaveMyProgressControllerTests
    {
        protected ILogger<SaveMyProgressController> Logger { get; } = A.Fake<ILogger<SaveMyProgressController>>();

        [Fact]
        public void HtmlHeadRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.HtmlHead();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<HtmlHeadViewModel>().Which;
            Assert.Equal($"{SaveMyProgressController.PageTitle} | {BaseController.DefaultPageTitleSuffix}", viewModel.Title);
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

        [Fact]
        public void BodyGetRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.Body();

            var viewResult = result.Should().BeOfType<ViewResult>().Which;
            viewResult.ViewData.Model.Should().BeNull();
        }

        private SaveMyProgressController BuildController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new SaveMyProgressController(Logger)
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
