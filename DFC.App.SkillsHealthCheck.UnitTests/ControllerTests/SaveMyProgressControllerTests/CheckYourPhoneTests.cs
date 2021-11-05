using System.Collections.Generic;
using System.Net.Mime;

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
        public void CheckYourPhoneBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", "123" } });

            var result = controller.CheckYourPhoneBody(null);

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<ReferenceNumberViewModel>()
                .Which.PhoneNumber.Should().Be("123");
        }

        [Fact]
        public void CheckYourPhoneRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", "123" } });

            var result = controller.CheckYourPhone(null);

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<GetCodeViewModel>()
                .Which.BodyViewModel?.PhoneNumber.Should().Be("123");
        }
    }
}
