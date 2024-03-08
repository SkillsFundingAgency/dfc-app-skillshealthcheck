using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using DfE.SkillsCentral.Api.Application.Services.Services;
using Moq;
using System.Data.Common;

namespace DfE.SkillsCentral.Api.Application.Services.UnitTests
{
    public class AssessmentsServiceTests
    {
        [Fact]
        public async Task GetAssessmentQuestions_ShouldReturnAllQuestionsAndAnswers_WhenValidAssessmentTypeProvided()
        {
            //Arrange
            Mock<IAssessmentsRepository> _assessmentsRepository = new Mock<IAssessmentsRepository>();
            Mock<IQuestionsRepository> _questionsRepository = new Mock<IQuestionsRepository>();
            Mock<IAnswersRepository> _answersRepository = new Mock<IAnswersRepository>();
            Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();

            var validAssessment = "test1";
            Assessment assessment = new Assessment();
            IReadOnlyList<Question> questions = new List<Question>() { new Question(), new Question() };
            IReadOnlyList<Answer> answers1 = new List<Answer>() { new Answer(), new Answer() };
            IReadOnlyList<Answer> answers2 = new List<Answer>() { new Answer(), new Answer() };

            _questionsRepository.Setup(x => x.GetAllByAssessmentIdAsync(assessment.Id)).ReturnsAsync(questions);
            _assessmentsRepository.Setup(x => x.GetByTypeAsync(validAssessment)).ReturnsAsync(assessment);
            var _assessmentsService = new AssessmentsService(_assessmentsRepository.Object, _questionsRepository.Object, _answersRepository.Object, _skillsDocumentsRepository.Object);

            //Act
            var assessmentQuestions = await _assessmentsService.GetAssessmentQuestions(validAssessment);
            var questionAnswers1 = new QuestionAnswers() { Question = questions[0], Answers = answers1 };
            var questionAnswers2 = new QuestionAnswers() { Question = questions[1], Answers = answers2 };
            var assessmentQuestionsResult = new AssessmentQuestions() { Assessment = new Assessment(), Questions = { questionAnswers1, questionAnswers2 } };

            //Assert
            Assert.Equal(assessmentQuestions.Questions.Count, assessmentQuestionsResult.Questions.Count);
        }

        [Fact]
        public async Task GetAssessmentQuestions_ShouldReturnDefault_WhenInvalidAssessmentTypeProvided()
        {
            //Arrange
            Mock<IAssessmentsRepository> _assessmentsRepository = new Mock<IAssessmentsRepository>();
            Mock<IQuestionsRepository> _questionsRepository = new Mock<IQuestionsRepository>();
            Mock<IAnswersRepository> _answersRepository = new Mock<IAnswersRepository>();
            Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();

            var invalidAssessment = "test2";
            Assessment assessment = new Assessment();
            IReadOnlyList<Question> questions = new List<Question>() { new Question(), new Question() };
            IReadOnlyList<Answer> answers1 = new List<Answer>() { new Answer(), new Answer() };
            IReadOnlyList<Answer> answers2 = new List<Answer>() { new Answer(), new Answer() };

            _questionsRepository.Setup(x => x.GetAllByAssessmentIdAsync(assessment.Id)).ReturnsAsync(questions);
            _assessmentsRepository.Setup(x => x.GetByTypeAsync(invalidAssessment)).ReturnsAsync(default(Assessment));
            var _assessmentsService = new AssessmentsService(_assessmentsRepository.Object, _questionsRepository.Object, _answersRepository.Object, _skillsDocumentsRepository.Object);

            //Act
            var assessmentQuestions = await _assessmentsService.GetAssessmentQuestions(invalidAssessment);

            //Assert
            Assert.Null(assessmentQuestions);
        }

        [Fact]
        public async Task SaveSkillsDocument_ShouldUpdateSkillsDocument_WhenGivenValidDocument()
        {
            //Arrange
            Mock<IAssessmentsRepository> _assessmentsRepository = new Mock<IAssessmentsRepository>();
            Mock<IQuestionsRepository> _questionsRepository = new Mock<IQuestionsRepository>();
            Mock<IAnswersRepository> _answersRepository = new Mock<IAnswersRepository>();
            Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();

            SkillsDocument skillsDocument = new SkillsDocument();
            Assessment assessment = new Assessment();
            IReadOnlyList<Question> questions = new List<Question>() { new Question(), new Question() };

            _skillsDocumentsRepository.Setup(x => x.UpdateAsync(skillsDocument)).Returns(Task.CompletedTask).Verifiable();
            _questionsRepository.Setup(x => x.GetAllByAssessmentIdAsync(assessment.Id)).ReturnsAsync(questions);
            var _assessmentsService = new AssessmentsService(_assessmentsRepository.Object, _questionsRepository.Object, _answersRepository.Object, _skillsDocumentsRepository.Object);

            //Act
            await _assessmentsService.SaveSkillsDocument(skillsDocument);

            //Assert
            _skillsDocumentsRepository.Verify(x => x.UpdateAsync(skillsDocument), Times.Once);
        }
    }
}