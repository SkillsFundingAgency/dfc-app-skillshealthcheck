using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FakeItEasy;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.SaveMyProgressController
{
    [Trait("Category", "SaveMyProgress Controller Integration Tests")]
    public class GetCodeRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public GetCodeRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetCodeBodyEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task GetCodeBodyEndpointWithActiveSessionReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }

        [Fact]
        public async Task GetCodeBodyEndpointWithActiveSessionReturnSuccessAndCorrectContentTypeBreadcrumb()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/breadcrumb", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }

        [Fact]
        public async Task PostGetCodeBodyEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.PostAsJsonAsync(uri, new ReferenceNumberViewModel());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task InvalidPostGetCodeBodyEndpointWithActiveSessionReturns()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            var response = await client.PostAsJsonAsync(uri, new ReferenceNumberViewModel { PhoneNumber = "asdsa" });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK); // do not redirect for invalid request
        }

        [Fact]
        public async Task ValidPostGetCodeBodyEndpointWithActiveSessionRedirectsToSmsSentPage()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            SetSendSms(true);

            // Act
            using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["PhoneNumber"] = "07700 900 982"
            });
            var response = await client.PostAsync(uri, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/save-my-progress/sms");
        }

        [Fact]
        public async Task ValidPostGetCodeBodyEndpointWithActiveSessionAndGovNotifyFailedToSendSmsRedirectsToSmsFailedPage()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/getcode/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            SetSendSms(false);

            // Act
            using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["PhoneNumber"] = "07700 900 982"
            });
            var response = await client.PostAsync(uri, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/save-my-progress/smsfailed");
        }

        [Fact]
        public async Task CheckYourPhoneBodyEndpointWithActiveSessionReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/sms/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }

        [Fact]
        public async Task SmsFailedBodyEndpointWithActiveSessionReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/smsfailed/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }

        private void SetSendSms(bool isSuccess)
        {
            A.CallTo(() => factory.FakeGovNotifyService.SendSmsAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(new NotifyResponse { IsSuccess = isSuccess });
        }
    }
}
