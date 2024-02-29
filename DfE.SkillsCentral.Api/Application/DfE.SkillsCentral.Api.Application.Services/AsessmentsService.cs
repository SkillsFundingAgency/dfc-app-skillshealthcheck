using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfE.SkillsCentral.Api.Application.Services
{
    public class AsessmentsService
    {
        private readonly IAssessmentsRepository assessmentsRepository;
        private readonly IQuestionsRepository questionsRepository;
        private readonly IAnswersRepository answersRepository;
        private readonly ISkillsDocumentsRepository skillsDocumentsRepository;

        public AsessmentsService(IAssessmentsRepository assessmentsRepository, IQuestionsRepository questionsRepository, 
            IAnswersRepository answersRepository, ISkillsDocumentsRepository skillsDocumentsRepository)
        {
            this.assessmentsRepository = assessmentsRepository;
            this.questionsRepository = questionsRepository;
            this.answersRepository = answersRepository;
            this.skillsDocumentsRepository = skillsDocumentsRepository;
        }

        //Reading Assessments:

        public Task<IReadOnlyList<Assessment>> GetAllAssessments()
        {
            return this.assessmentsRepository.GetAllAsync();
        }

        public Task<Assessment> GetAssessmentById(int assessmentId)
        {
            return this.assessmentsRepository.GetByIdAsync(assessmentId);
        }

        //Reading Questions:

        public Task<IReadOnlyList<Question>> GetAllQuestions()
        {
            return this.questionsRepository.GetAllAsync();
        }

        public Task<IReadOnlyList<Question>> GetQuestionsForAssessmentById(int assessmentId)
        {    
            return this.questionsRepository.GetAllByAssessmentIdAsync(assessmentId);
        }

        public Task<Question> GetQuestionById(int questionId)
        {
            return this.questionsRepository.GetByIdAsync(questionId);
        }

        //Reading Answers:

        public Task<IReadOnlyList<Answer>> GetAnswersForAssessmentById(int assessmentId)
        {
            return this.answersRepository.GetAllByAssessmentIdAsync(assessmentId);
        }

        public Task<IReadOnlyList<Answer>> GetAnswersForQuestionById(int questionId)
        {
            return this.answersRepository.GetAllByQuestionIdAsync(questionId);
        }

        public Task<Answer> GetAnswerById(int answerId)
        {
            return this.answersRepository.GetByIdAsync(answerId);
        }

        //Writing Answers: 

        public void SaveQuestionAnswer(int skillsDocumentId, int assessmentId, int questionId, string answerValue)
        {
            if (answerValue == null) { throw new ArgumentNullException(nameof(answerValue)); }
            
            //nothing in repository to call?
            throw new NotImplementedException();
        }
    }
}
