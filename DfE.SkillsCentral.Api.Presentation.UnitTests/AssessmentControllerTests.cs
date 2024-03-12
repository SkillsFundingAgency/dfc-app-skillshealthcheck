using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Services.Services;
using DfE.SkillsCentral.Api.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DfE.SkillsCentral.Api.Presentation.UnitTests
{
    public class AssessmentControllerTests
    {
        private readonly ILogger<AssessmentController> logger;

        [Fact]
        public async Task GetAssessmentQuestions_NoIdProvided()
        {
            //Arrange
            Mock<IAssessmentsRepository> _assessmentsRepository = new Mock<IAssessmentsRepository>();
            Mock<IQuestionsRepository> _questionsRepository = new Mock<IQuestionsRepository>();
            Mock<IAnswersRepository> _answersRepository = new Mock<IAnswersRepository>();
            Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();

            var validAssessment = "test_assessment";
            Assessment assessment = new Assessment();
            IReadOnlyList<Question> questions = new List<Question>() { new Question(), new Question() };
            IReadOnlyList<Answer> answers1 = new List<Answer>() { new Answer(), new Answer() };
            IReadOnlyList<Answer> answers2 = new List<Answer>() { new Answer(), new Answer() };

            _questionsRepository.Setup(x => x.GetAllByAssessmentIdAsync(assessment.Id)).ReturnsAsync(questions);
            _assessmentsRepository.Setup(x => x.GetByTypeAsync(validAssessment)).ReturnsAsync(assessment);
            var _assessmentsService = new AssessmentsService(_assessmentsRepository.Object, _questionsRepository.Object, _answersRepository.Object, _skillsDocumentsRepository.Object);
            var _skillsDocumentService = new SkillsDocumentsService(_skillsDocumentsRepository.Object);

            //Act
            var sut = new AssessmentController(_skillsDocumentService, _assessmentsService, logger);
            var result = await sut.GetAssessment(null);
            var response = result.Result as ObjectResult;

            //Assert
            Assert.Equal(500, response?.StatusCode);
        }

        [Fact]
        public async Task GetAssessmentQuestions_WithIdProvided()
        {
            //Arrange
            Mock<IAssessmentsRepository> _assessmentsRepository = new Mock<IAssessmentsRepository>();
            Mock<IQuestionsRepository> _questionsRepository = new Mock<IQuestionsRepository>();
            Mock<IAnswersRepository> _answersRepository = new Mock<IAnswersRepository>();
            Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();

            var validAssessment = "test_assessment";
            Assessment assessment = new Assessment();
            IReadOnlyList<Question> questions = new List<Question>() { new Question(), new Question() };
            IReadOnlyList<Answer> answers1 = new List<Answer>() { new Answer(), new Answer() };
            IReadOnlyList<Answer> answers2 = new List<Answer>() { new Answer(), new Answer() };

            _questionsRepository.Setup(x => x.GetAllByAssessmentIdAsync(assessment.Id)).ReturnsAsync(questions);
            _assessmentsRepository.Setup(x => x.GetByTypeAsync(validAssessment)).ReturnsAsync(assessment);
            var _assessmentsService = new AssessmentsService(_assessmentsRepository.Object, _questionsRepository.Object, _answersRepository.Object, _skillsDocumentsRepository.Object);
            var _skillsDocumentService = new SkillsDocumentsService(_skillsDocumentsRepository.Object);

            //Act
            var sut = new AssessmentController(_skillsDocumentService, _assessmentsService, logger);
            var result = await sut.GetAssessment("spatial");

            //Assert
            var response = result.Result as ObjectResult;
            Assert.Equal(200, response?.StatusCode);
        }
    }
}