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
        private readonly Mock<IAssessmentsRepository> _assessmentsRepository = new Mock<IAssessmentsRepository>();
        private readonly Mock<IQuestionsRepository> _questionsRepository = new Mock<IQuestionsRepository>();
        private readonly Mock<IAnswersRepository> _answersRepository = new Mock<IAnswersRepository>();
        private readonly Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();
        private readonly AssessmentsService _assessmentsService;

        readonly string validAssessment = "test1";
        readonly string invalidAssessment = "test2";
        IReadOnlyList<Question> questions = new List<Question>() { new Question(), new Question()};
        IReadOnlyList<Answer> answers1 = new List<Answer>() { new Answer(), new Answer() };
        IReadOnlyList<Answer> answers2 = new List<Answer>() { new Answer(), new Answer() };
        SkillsDocument skillsDocument = new SkillsDocument();
        Task task = Task.CompletedTask;

        public AssessmentsServiceTests() 
        {


            Assessment assessment = new Assessment();

            //_assessmentsRepository.Setup(x => x.GetByTypeAsync(validAssessment)).ReturnsAsync(assessment);
            //_assessmentsRepository.Setup(x => x.GetByTypeAsync(invalidAssessment)).ReturnsAsync(default(Assessment));
            _questionsRepository.Setup(x => x.GetAllByAssessmentIdAsync(assessment.Id)).ReturnsAsync(questions);
            _answersRepository.Setup(x => x.GetAllByQuestionIdAsync(questions[0].Id)).ReturnsAsync(answers1);
            _answersRepository.Setup(x => x.GetAllByQuestionIdAsync(questions[1].Id)).ReturnsAsync(answers2);


            _assessmentsService = new AssessmentsService(_assessmentsRepository.Object, _questionsRepository.Object, _answersRepository.Object, _skillsDocumentsRepository.Object);
        }
        [Fact]
        public async Task GetAssessmentQuestions_ShouldReturnAllQuestionsAndAnswers_WhenValidAssessmentTypeProvided()
        {
         
            var assessmentQuestions = await _assessmentsService.GetAssessmentQuestions(validAssessment);

            var questionAnswers1 = new QuestionAnswers() { Question = questions[0], Answers = answers1 };
            var questionAnswers2 = new QuestionAnswers() { Question = questions[1], Answers = answers2 };
            var assessmentQuestionsResult = new AssessmentQuestions() { Assessment = new Assessment(), Questions = {questionAnswers1,questionAnswers2}};
            
            Assert.Equal(assessmentQuestions.Questions.Count, assessmentQuestionsResult.Questions.Count);

        }


        [Fact]
        public async Task GetAssessmentQuestions_ShouldReturnDefault_WhenInvalidAssessmentTypeProvided()
        {

            var assessmentQuestions = await _assessmentsService.GetAssessmentQuestions(invalidAssessment);

            Assert.Null(assessmentQuestions);
        }


        [Fact]
        public async Task SaveSkillsDocument_ShouldUpdateSkillsDocument_WhenGivenValidDocument()
        {
            _skillsDocumentsRepository.Setup(x => x.UpdateAsync(skillsDocument)).Returns(Task.CompletedTask).Verifiable();

            await _assessmentsService.SaveSkillsDocument(skillsDocument);

            _skillsDocumentsRepository.Verify(x=>x.UpdateAsync(skillsDocument), Times.Once);
        }

    }
}