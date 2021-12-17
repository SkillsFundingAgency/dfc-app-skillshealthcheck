using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.ViewModels;

using FluentAssertions;

using Xunit;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.QuestionController
{
    [Trait("Category", "Question Controller Integration Tests")]
    public class HeadRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public HeadRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        public static IEnumerable<object[]> RouteData => new List<object[]>
        {
            new object[] { "skills-health-check/question/head" },
            new object[] { "skills-health-check/question/answer-question/head" },
            new object[] { "skills-health-check/question/answer-multiple-question/head" },
            new object[] { "skills-health-check/question/answer-elimination-question/head" },
            new object[] { "skills-health-check/question/answer-feedback-question/head" },
            new object[] { "skills-health-check/question/answer-checking-question/head" },
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
            result.Title.Should().Be("Question | Skills Health Check | National Careers Service");
        }
    }
}
