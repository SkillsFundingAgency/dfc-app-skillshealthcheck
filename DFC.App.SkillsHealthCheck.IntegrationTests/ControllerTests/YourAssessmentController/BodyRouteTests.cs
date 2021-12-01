using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.YourAssessmentController
{
    [Trait("Category", "YourAssessment Controller Integration Tests")]
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
            var uri = new Uri("skills-health-check/your-assessments/body", UriKind.Relative);
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
    }
}
