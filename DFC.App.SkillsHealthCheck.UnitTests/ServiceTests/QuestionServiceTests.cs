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
using SkillsDocument = DFC.SkillsCentral.Api.Domain.Models.SkillsDocument;

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

        //[Fact]
        //public async Task GetSkillsDocumentSuccess()
        //{

        //    // Arrange
        //    var skillsDoc = new SkillsDocument
        //    {
        //        Id = 1,
        //    };

        //    var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(A<int>.Ignored))
        //    .Returns(skillsDoc);

        //    // Act
        //    var response = A.CallTo(() => questionService.GetSkillsDocument(A<int>.Ignored))
        //        .Returns(skillsDoc);

        //    // Assert
        //    Assert.Equal(aCallToSHCServiceGetSkillsDocument, response);
        //}

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
                   Answers = new List<SkillsCentral.Api.Domain.Models.Answer>
                   {
                       new SkillsCentral.Api.Domain.Models.Answer
                       {
                           Id = 1,
                           QuestionID = 1,
                           Value = "Test",
                       }
                   }
               }
               }
            });
            var skillsDocument = new SkillsCentral.Api.Domain.Models.SkillsDocument
            {
                Id = 123,
            };
            var assessmentQuestionOverview = new AssessmentQuestionsOverView
            {
                TotalQuestionsNumberPlusFeedback = 1,
            };

            // Act
            var viewModel = questionService.GetAssessmentQuestionViewModel(Services.SkillsCentral.Enums.AssessmentType.Personal, skillsDocument, assessmentQuestionOverview, 0);

            // Assert
            Assert.Equal(1, viewModel.Id);
        }
    }
}
