using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.ViewModels.Question;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.QuestionController
{
    [Trait("Category", "Question Controller Integration Tests")]
    public class ExtendSessionRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string Url = "skills-health-check/question/extend-session/body";
        private readonly CustomWebApplicationFactory<Startup> factory;

        public ExtendSessionRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task PostRequestWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri(Url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.PostAsJsonAsync(uri, new TabularAnswerQuestionViewModel { });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location!.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task ValidPostRequestWithActiveSessionAndValidAssessmentTypesRedirectsAsExpected()
        {
            // Arrange
            var uri = new Uri(Url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["AssessmentType"] = "Abstract",
            });
            var response = await client.PostAsync(uri, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location!.ToString().Should().Contain($"skills-health-check/question?assessmentType=Abstract");
        }

        [Fact]
        public async Task ValidPostRequestWithActiveSessionAndNoAssessmentTypesRedirectsAsExpected()
        {
            // Arrange
            var uri = new Uri(Url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();

            // Act
            var response = await client.PostAsync(uri, null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location!.ToString().Should().Contain($"/skills-health-check/home");
        }
    }
}
