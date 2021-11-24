using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Question;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.QuestionController
{
    [Trait("Category", "Question Controller Integration Tests")]
    public class AnswerQuestionRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string Url = "skills-health-check/question/answer-question/body";
        private readonly CustomWebApplicationFactory<Startup> factory;

        public AnswerQuestionRouteTests(CustomWebApplicationFactory<Startup> factory)
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
            var response = await client.PostAsJsonAsync(uri, new AssessmentQuestionViewModel { });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task ValidPostRequestWithActiveSessionRedirectsToNextQuestion()
        {
            // Arrange
            var uri = new Uri(Url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            var model = new AssessmentQuestionViewModel
            {
                QuestionAnswer = "some answer",
                Question = new Question
                {
                    AssessmentType = AssessmentType.SkillAreas,
                    Level = Level.Level1,
                    Accessibility = Accessibility.Full,
                },
            };

            // Act
            var response = await client.PostAsJsonAsync(uri, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().Contain($"skills-health-check/question?assessmentType={model.Question.AssessmentType}");
        }
    }
}
