using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class CheckYourPhoneTests : SaveMyProgressControllerTestsBase
    {
        [Fact]
        public async Task CheckYourPhoneBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", "123" } });

            var result = await controller.CheckYourPhoneBody();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<ReferenceNumberViewModel>()
                .Which.PhoneNumber.Should().Be("123");
        }

        [Fact]
        public async Task CheckYourPhoneRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", "123" } });

            var result = await controller.CheckYourPhone();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<GetCodeViewModel>()
                .Which.BodyViewModel?.PhoneNumber.Should().Be("123");
        }
    }
}
