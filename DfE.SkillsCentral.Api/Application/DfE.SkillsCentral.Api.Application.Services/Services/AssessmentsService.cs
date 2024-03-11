using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfE.SkillsCentral.Api.Application.Services.Services
{
    public class AssessmentsService : IAssessmentsService
    {
        private readonly IAssessmentsRepository _assessmentsRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly ISkillsDocumentsRepository _skillsDocumentsRepository;


        public AssessmentsService(IAssessmentsRepository assessmentsRepository, IQuestionsRepository questionsRepository, IAnswersRepository answersRepository, ISkillsDocumentsRepository skillsDocumentsRepository)
        {
            _assessmentsRepository = assessmentsRepository;
            _questionsRepository = questionsRepository;
            _answersRepository = answersRepository;
            _skillsDocumentsRepository = skillsDocumentsRepository;
        }

        public async Task<AssessmentQuestions?> GetAssessmentQuestions(string assessmentType)
        {
            var assessmentQuestions = new AssessmentQuestions();

            assessmentQuestions.Assessment = await _assessmentsRepository.GetByTypeAsync(assessmentType);
            if (assessmentQuestions.Assessment == null)
                return default;
            var questions = await _questionsRepository.GetAllByAssessmentIdAsync(assessmentQuestions.Assessment.Id);
            foreach ( var question in questions)
            {
                var answers = await _answersRepository.GetAllByQuestionIdAsync(question.Id);
                var questionAnswers = new QuestionAnswers()
                {
                    Question = question,
                    Answers = answers    
                };

                assessmentQuestions.Questions.Add(questionAnswers);
            }

            return assessmentQuestions;
        }
    }

    
}
