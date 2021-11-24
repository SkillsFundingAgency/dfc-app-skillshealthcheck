using System;
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
    public class EmailRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public EmailRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task EmailBodyGetEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/email/body", UriKind.Relative);
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
        public async Task EmailBodyGetEndpointWithActiveSessionReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/email/body", UriKind.Relative);
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
        public async Task EmailBodyPostEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/email/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.PostAsJsonAsync(uri, new EmailViewModel());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task InvalidEmailBodyPostEndpointWithActiveSessionReturns()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/email/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            var response = await client.PostAsJsonAsync(uri, new EmailViewModel { EmailAddress = "asdads" });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK); // do not redirect for invalid request
        }

        [Fact]
        public async Task ValidEmailBodyPostEndpointWithActiveSessionRedirectsToSmsSentPage()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/email/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            SetSendEmail(true);

            // Act
            var response = await client.PostAsJsonAsync(uri, new EmailViewModel { EmailAddress = "123@abc.com" });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/save-my-progress/emailsent");
        }

        [Fact]
        public async Task ValidEmailBodyPostEndpointWithActiveSessionAndGovNotifyFailedToSendSmsRedirectsToSmsFailedPage()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/email/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            SetSendEmail(false);

            // Act
            var response = await client.PostAsJsonAsync(uri, new EmailViewModel { EmailAddress = "123@abc.com" });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/save-my-progress/emailfailed");
        }

        [Fact]
        public async Task CheckYourEmailBodyEndpointWithActiveSessionReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/emailsent/body", UriKind.Relative);
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
            var uri = new Uri("skills-health-check/save-my-progress/emailfailed/body", UriKind.Relative);
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

        private void SetSendEmail(bool isSuccess)
        {
            A.CallTo(() => factory.FakeGovNotifyService.SendEmailAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(new NotifyResponse { IsSuccess = isSuccess });
        }
    }
}
