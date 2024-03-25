using DFC.App.SkillsHealthCheck.Services;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.SkillsCentral.Api.Domain.Models;
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
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(A<int>.Ignored))
            .Returns(new SkillsCentral.Api.Domain.Models.SkillsDocument
            {
                Id = 1,
            });

            // Act
            var response = A.CallTo(() => questionService.GetSkillsDocument(A<int>.Ignored))
                .Returns(new SkillsCentral.Api.Domain.Models.SkillsDocument
            {
                Id = 1,
            });

            // Assert
            Assert.Equal(aCallToSHCServiceGetSkillsDocument, response);
        }

        [Fact]
        public async Task GetAssessmentQuestionViewModelSuccess()
        {
            // Arrange
            var aCallToSHCServiceGetAssessmentQuestion = A.CallTo(() => skillsHealthCheckService.GetAssessmentQuestions(A<string>.Ignored));
            aCallToSHCServiceGetAssessmentQuestion.Returns(new AssessmentQuestions
            {
               Assessment = new Assessment
               { Id = 1,
               Type = "testType",
               Title = "Test"},

               Questions = new List<QuestionAnswers>
               { new QuestionAnswers
               {
                   Question = new()
                   {
                       Id = 1,
                       Title = "Test",
                   },
                   Answers = new IReadOnlyList<SkillsCentral.Api.Domain.Models.Answer>
                   {
                       
                   }
               }
               }
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
