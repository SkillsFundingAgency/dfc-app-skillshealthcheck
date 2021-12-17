using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.ViewModels;

using FluentAssertions;

using Xunit;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.YourAssessmentController
{
    [Trait("Category", "YourAssessment Controller Integration Tests")]
    public class HeadRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public HeadRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        public static IEnumerable<object[]> RouteData => new List<object[]>
        {
            new object[] { "skills-health-check/your-assessments/head" },
            new object[] { "skills-health-check/your-assessments/download-document/head" },
            new object[] { "skills-health-check/your-assessments/return-to-assessment/head" },
        };

        [Theory]
        [MemberData(nameof(RouteData))]
        public async Task GetHeadContentEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var uri = new Uri(url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }

        [Theory]
        [MemberData(nameof(RouteData))]
        public async Task GetHeadContentEndpointsReturnSuccessAndCorrectContent(string url)
        {
            // Arrange
            var uri = new Uri(url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            // Act
            var response = await client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);
            var result = await response.Content.ReadAsAsync<HeadViewModel>();
            result.Should().NotBeNull();
            result.Title.Should().Be("Your assessments | Skills Health Check | National Careers Service");
        }
    }
}
