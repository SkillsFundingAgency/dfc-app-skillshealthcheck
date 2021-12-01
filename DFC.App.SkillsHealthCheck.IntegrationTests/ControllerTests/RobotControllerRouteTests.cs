using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests
{
    [Trait("Category", "Robot Controller Integration Tests")]
    public class RobotControllerRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public RobotControllerRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        public static IEnumerable<object[]> RobotRouteData => new List<object[]>
        {
            new object[] { "/robots.txt" },
            new object[] { "/skills-health-check/robots" },
        };

        [Theory]
        [MemberData(nameof(RobotRouteData))]
        public async Task GetRobotTextContentEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var uri = new Uri(url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Plain));

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Plain);
        }
    }
}
