using DFC.App.SkillsHealthCheck.Services;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ServiceTests
{
    public class QuestionServiceTests
    {
        private readonly ISkillsHealthCheckService skillsHealthCheckService;
        private readonly IQuestionService questionService;

        public QuestionServiceTests()
        {
            skillsHealthCheckService = A.Fake<ISkillsHealthCheckService>();
            questionService = new QuestionService(skillsHealthCheckService);
        }

        [Fact]
        public async Task GetSkillsDocumentSuccess()
        {
            // Arrange
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(A<GetSkillsDocumentRequest>.Ignored));
            aCallToSHCServiceGetSkillsDocument.Returns(new GetSkillsDocumentResponse
            {
                Success = true,
                SkillsDocument = new SkillsDocument
                {
                    SkillsDocumentTitle = "Skills Health Check",
                },
            });

            // Act
            var response = questionService.GetSkillsDocument(new GetSkillsDocumentRequest
            {
                DocumentId = 123,
            });

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task GetAssessmentQuestionViewModelSuccess()
        {
            // Arrange
            var aCallToSHCServiceGetAssessmentQuestion = A.CallTo(() => skillsHealthCheckService.GetAssessmentQuestion(A<GetAssessmentQuestionsRequest>.Ignored));
            aCallToSHCServiceGetAssessmentQuestion.Returns(new GetAssessmentQuestionsResponse
            {
                Success = true,
                Question = new Question
                {
                    QuestionTitle = "Fake question",
                    QuestionNumber = 1,
                    AssessmentTitle = "Fake assessment title",
                    QuestionData = string.Empty
                },
            });
            var skillsDocument = new SkillsDocument
            {
                DocumentId = 123,
                SkillsDocumentDataValues = new List<SkillsDocumentDataValue>(),
            };
            var assessmentQuestionOverview = new AssessmentQuestionsOverView
            {
                TotalQuestionsNumberPlusFeedback = 1,
            };

            // Act
            var viewModel = questionService.GetAssessmentQuestionViewModel(Services.SkillsCentral.Enums.Level.Level1, Services.SkillsCentral.Enums.Accessibility.Full, Services.SkillsCentral.Enums.AssessmentType.Abstract, skillsDocument, assessmentQuestionOverview);

            // Assert
            Assert.Equal("Fake assessment title", viewModel.AssessmentTitle);
        }
    }
}
