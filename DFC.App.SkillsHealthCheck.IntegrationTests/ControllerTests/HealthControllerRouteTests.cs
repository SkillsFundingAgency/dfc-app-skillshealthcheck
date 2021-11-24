using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using FluentAssertions;

using Xunit;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests
{
    [Trait("Category", "Health Controller Integration Tests")]
    public class HealthControllerRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public HealthControllerRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        public static IEnumerable<object[]> HealthContentRouteData => new List<object[]>
        {
            new object[] { "/skills-health-check/health" },
            new object[] { "/health" },
        };

        public static IEnumerable<object[]> HealthOkRouteData => new List<object[]>
        {
            new object[] { "/health/ping" },
        };

        [Theory]
        [MemberData(nameof(HealthContentRouteData))]
        public async Task GetHealthHtmlContentEndpointsReturnSuccessAndCorrectContentType(string path)
        {
            // Arrange
            var uri = new Uri(path, UriKind.Relative);
            var client = this.factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            A.CallTo(() => this.factory.MockCosmosRepo.PingAsync()).Returns(true);

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
            response.Content.Headers.ContentType.CharSet.Should().Be(Encoding.UTF8.WebName);
        }

        [Theory]
        [MemberData(nameof(HealthContentRouteData))]
        public async Task GetHealthJsonContentEndpointsReturnSuccessAndCorrectContentType(string path)
        {
            // Arrange
            var uri = new Uri(path, UriKind.Relative);
            var client = this.factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            A.CallTo(() => this.factory.MockCosmosRepo.PingAsync()).Returns(true);

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);
            response.Content.Headers.ContentType.CharSet.Should().Be(Encoding.UTF8.WebName);
        }

        [Theory]
        [MemberData(nameof(HealthOkRouteData))]
        public async Task GetHealthOkEndpointsReturnSuccess(string path)
        {
            // Arrange
            var uri = new Uri(path, UriKind.Relative);
            var client = this.factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
