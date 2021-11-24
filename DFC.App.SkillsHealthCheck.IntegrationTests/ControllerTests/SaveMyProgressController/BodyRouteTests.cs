using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.SaveMyProgressController
{
    [Trait("Category", "SaveMyProgress Controller Integration Tests")]
    public class BodyRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public BodyRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetBodyEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task GetBodyEndpointWithActiveSessionReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }

        [Theory]
        [InlineData(null, "/skills-health-check/your-assessments", "Return to your skills health check")]
        [InlineData("Skills", "/skills-health-check/question?assessmentType=Skills", "Return to your skills health check assessment")]
        public async Task GetBodyEndpointReturnSuccessAndCorrectContent(string? type, string expectedReturnLink, string expectedReturnLinkText)
        {
            // Arrange
            var uri = new Uri($"skills-health-check/save-my-progress/body?type={type}", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            SetSession(client, factory);

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var result = await response.Content.ReadAsAsync<SaveMyProgressViewModel>();
            result.Should().NotBeNull();
            result.ReturnLink.Should().Be(expectedReturnLink);
            result.ReturnLinkText.Should().Be(expectedReturnLinkText);
        }

        [Fact]
        public async Task PostBodyEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.PostAsJsonAsync(uri, new SaveMyProgressViewModel { SelectedOption = Enums.SaveMyProgressOption.Email });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Theory]
        [InlineData(Enums.SaveMyProgressOption.Email, "/skills-health-check/save-my-progress/email")]
        [InlineData(Enums.SaveMyProgressOption.ReferenceCode, "/skills-health-check/save-my-progress/getcode")]
        public async Task ValidPostBodyEndpointWithActiveSessionRedirectsToCorrectUrl(Enums.SaveMyProgressOption option, string expectedRedirectUrl)
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);

            // Act
            var response = await client.PostAsJsonAsync(uri, new SaveMyProgressViewModel { SelectedOption = option });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().Be(expectedRedirectUrl);
        }

        [Fact]
        public async Task InvalidPostBodyEndpointWithActiveSessionReturnsOkWithValidationError()
        {
            // Arrange
            var uri = new Uri("skills-health-check/save-my-progress/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);

            // Act
            var response = await client.PostAsJsonAsync(uri, new SaveMyProgressViewModel { SelectedOption = Enums.SaveMyProgressOption.None });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK); // do not redirect for invalid request
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain(SaveMyProgressViewModel.SelectedOptionValidationError);
        }
    }
}
