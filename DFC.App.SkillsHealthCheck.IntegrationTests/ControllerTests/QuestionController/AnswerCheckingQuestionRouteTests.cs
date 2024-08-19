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
    public class AnswerCheckingQuestionRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string Url = "skills-health-check/question/answer-checking-question/body";
        private readonly CustomWebApplicationFactory<Startup> factory;

        public AnswerCheckingQuestionRouteTests(CustomWebApplicationFactory<Startup> factory)
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
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        //[Fact]
        //public async Task ValidPostRequestWithActiveSessionRedirectsToNextQuestion()
        //{
        //    // Arrange
        //    var uri = new Uri(Url, UriKind.Relative);
        //    var client = factory.CreateClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
        //    SetSession(client, factory);
        //    factory.SetSkillsDocument();

        //    // Act
        //    using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
        //    {
        //        ["QuestionAnswer"] = "some answer",
        //        ["AnswerSelection[0]"] = "some answer",
        //        ["Question.AssessmentType"] = AssessmentType.SkillAreas.ToString(),
        //        ["Question.Level"] = Level.Level1.ToString(),
        //        ["Question.Accessibility"] = Accessibility.Full.ToString(),
        //    });
        //    var response = await client.PostAsync(uri, content);

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        //    response.Headers.Location.ToString().Should().Contain($"skills-health-check/question?assessmentType={AssessmentType.SkillAreas}");
        //}
    }
}
