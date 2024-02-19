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
    public class AsessmentsService : IAssessmentsService
    {
        private readonly IQuestionsRepository questionRepository;

        public AsessmentsService(IQuestionsRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public Task<IReadOnlyList<Question>> GetAssessmentQuestions(string assessmentId)
        {
            if (assessmentId == null) { throw new ArgumentNullException(nameof(assessmentId)); }
             
            return this.questionRepository.GetAllByAssessmentIdAsync(assessmentId);
        }

        public void SaveQuestionAnswer(string questionId, string usersAnswerId)
        {
            if (questionId == null) { throw new ArgumentNullException(nameof(questionId)); }
            if (usersAnswerId == null) { throw new ArgumentNullException(nameof(usersAnswerId)); }
            
            //nothing in repository to call?
            throw new NotImplementedException();
        }
    }
}
