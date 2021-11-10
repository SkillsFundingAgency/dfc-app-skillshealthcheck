using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FakeItEasy;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Notify.Models.Responses;

using Xunit;

namespace DFC.App.SkillsHealthCheck.Services.GovNotify.UnitTests
{
    public class GovNotifyServiceTests
    {
        private readonly IGovUkNotifyClientProxy fakeClientProxy = A.Fake<IGovUkNotifyClientProxy>();
        private readonly GovNotifyOptions govNotifyOptions;

        private readonly GovNotifyService govNotifyService;

        public GovNotifyServiceTests()
        {
            govNotifyOptions = new GovNotifyOptions
            {
                APIKey = "Some Key",
                EmailTemplateId = "Email Template Id",
                SmsTemplateId = "SMS Template Id",
            };
            govNotifyService = new GovNotifyService(fakeClientProxy, Options.Create(govNotifyOptions));
        }

        [Fact]
        public async Task SendEmailAsynReturnsSuccessfulNotifyResponse()
        {
            // Arrange
            var email = "some email";
            A.CallTo(() => fakeClientProxy.SendEmailAsync(email, govNotifyOptions.EmailTemplateId, A<Dictionary<string, dynamic>>.Ignored))
                .Returns(new EmailNotificationResponse());

            // Act
            var response = await govNotifyService.SendEmailAsync("domain", email, "sessionId");

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task SendEmailAsynReturnsFailedNotifyResponse()
        {
            // Arrange
            var email = "some email";
            var exception = new Exception("Failed to send Email.");
            A.CallTo(() => fakeClientProxy.SendEmailAsync(email, govNotifyOptions.EmailTemplateId, A<Dictionary<string, dynamic>>.Ignored))
                .Throws(exception);

            // Act
            var response = await govNotifyService.SendEmailAsync("domain", email, "sessionId");

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.Message.Should().Be(exception.Message);
        }

        [Fact]
        public async Task SendSmsAsynReturnsSuccessfulNotifyResponse()
        {
            // Arrange
            var phoneNumber = "0121212";
            A.CallTo(() => fakeClientProxy.SendSmsAsync(phoneNumber, govNotifyOptions.SmsTemplateId, A<Dictionary<string, dynamic>>.Ignored))
                .Returns(new SmsNotificationResponse());

            // Act
            var response = await govNotifyService.SendSmsAsync("domain", phoneNumber, "sessionId");

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();

        }

        [Fact]
        public async Task SendSmsAsynReturnsFailedNotifyResponse()
        {
            // Arrange
            var phoneNumber = "0121212";
            var exception = new Exception("Failed to send SMS.");
            A.CallTo(() => fakeClientProxy.SendSmsAsync(phoneNumber, govNotifyOptions.SmsTemplateId, A<Dictionary<string, dynamic>>.Ignored))
                .Throws(exception);

            // Act
            var response = await govNotifyService.SendSmsAsync("domain", phoneNumber, "sessionId");

            // Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.Message.Should().Be(exception.Message);
        }
    }
}
