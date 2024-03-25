using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;
using DFC.SkillsCentral.Api.Domain.Models;
using FakeItEasy;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class EmailTests : SaveMyProgressControllerTestsBase
    {
        private const string Email = "123@abc.com";
        private const string Code = "Code";

        [Fact]
        public async Task EmailBodyGetRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.EmailBody();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailViewModel>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task EmailGetRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);

            var result = await controller.Email();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailDocumentViewModel>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task EmailBodyPostRequestReturnsRedirectResponseToEmailSent()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);
            A.CallTo(() => GovNotifyService.SendEmailAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(new NotifyResponse { IsSuccess = true });

            var result = await controller.Email(new EmailViewModel());

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/emailsent");
        }

        [Fact]
        public async Task EmailPostRequestReturnsRedirectResponseToEmailSent()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);
            A.CallTo(() => GovNotifyService.SendEmailAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(new NotifyResponse { IsSuccess = true });

            var result = await controller.EmailBody(new EmailViewModel());

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/emailsent");
        }

        [Fact]
        public async Task EmailBodyPostRequestReturnsRedirectResponseToEmailFailed()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);
            A.CallTo(() => GovNotifyService.SendEmailAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(new NotifyResponse());

            var result = await controller.Email(new EmailViewModel());

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/emailfailed");
        }

        [Fact]
        public async Task EmailPostRequestReturnsRedirectResponseToEmailFailed()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html);
            A.CallTo(() => GovNotifyService.SendEmailAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(new NotifyResponse());

            var result = await controller.EmailBody(new EmailViewModel());

            result.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be("/skills-health-check/save-my-progress/emailfailed");
        }

        [Fact]
        public async Task CheckYourEmailBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "Email", Email } });

            var result = await controller.CheckYourEmailBody();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailViewModel>()
                .Which.EmailAddress.Should().Be(Email);
        }

        [Fact]
        public async Task CheckYourEmailRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "Email", Email } });

            var result = await controller.CheckYourEmail();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<EmailDocumentViewModel>()
                .Which.BodyViewModel?.EmailAddress.Should().Be(Email);
        }

        [Fact]
        public async Task EmailFailedBodyRequestsReturnsSuccess()
        {
            var skillsDocumentIdentifier = new SkillsDocumentIdentifier
            {
                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
                Value = Code,
            };

            A.CallTo(() => SkillsHealthCheckService.GetSkillsDocument(A<int>.Ignored))
                .Returns(new SkillsCentral.Api.Domain.Models.SkillsDocument { Id = 1 });
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "Email", Email } });

            var result = await controller.EmailFailedBody();

            var model = result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<ErrorViewModel>()
                .Which;
            model.SendTo.Should().Be(Email);
            model.Code.Should().Be(Code.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        }

        [Fact]
        public async Task EmailFailedRequestsReturnsSuccess()
        {
            var skillsDocumentIdentifier = new SkillsDocumentIdentifier
            {
                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
                Value = Code,
            };

            A.CallTo(() => SkillsHealthCheckService.GetSkillsDocument(A<int>.Ignored))
                 .Returns(new SkillsCentral.Api.Domain.Models.SkillsDocument { Id = 1 });
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "Email", Email } });

            var result = await controller.EmailFailed();

            var model = result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<ErrorDocumentViewModel>()
                .Which.BodyViewModel;

            model.Should().NotBeNull();
            model!.SendTo.Should().Be(Email);
            model.Code.Should().Be(Code.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        }
    }
}
