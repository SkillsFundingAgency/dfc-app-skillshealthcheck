﻿using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class EmailTests : SaveMyProgressControllerTestsBase
    {
        [Fact]
        public void EmailBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.EmailBody(null);

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailViewModel>()
                .And.NotBeNull();
        }

        [Fact]
        public void EmailRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = controller.Email(null);

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailDocumentViewModel>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task EmailBodyPostRequestReturnsRedirectResponse()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.Email(new EmailViewModel(), null);

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/emailsent?type=");
        }

        [Fact]
        public async Task EmailPostRequestReturnsRedirectResponse()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.EmailBody(new EmailViewModel(), null);

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/emailsent?type=");
        }

        [Fact]
        public void CheckYourEmailBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "Email", "123@abc.com" } });

            var result = controller.CheckYourEmailBody(null);

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailViewModel>()
                .Which.EmailAddress.Should().Be("123@abc.com");
        }

        [Fact]
        public void CheckYourEmailRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "Email", "123@abc.com" } });

            var result = controller.CheckYourEmail(null);

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailDocumentViewModel>()
                .Which.BodyViewModel?.EmailAddress.Should().Be("123@abc.com");
        }
    }
}
