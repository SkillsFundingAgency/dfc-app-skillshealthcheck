using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class GetCodeTests : SaveMyProgressControllerTestsBase
    {
        [Fact]
        public async Task GetCodeBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.GetCodeBody(null);

            result.Should().BeOfType<ViewResult>()
                 .Which.ViewData.Model.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCodePostRequestReturnsRedirectResult()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.GetCode(new ReferenceNumberViewModel(), null);

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/sms?type=");
        }

        [Fact]
        public async Task GetCodeBodyPostRequestReturnsRedirectResult()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.GetCodeBody(new ReferenceNumberViewModel(), null);

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/sms?type=");
        }
    }
}
