using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Application.Interfaces.Services
{
    public interface IAssessmentsService
    {



        //Saves user's given answer to the current question
        public void SaveQuestionAnswer(string questionId, string usersAnswerId);

        //Gets all questions in a given assessment
        public Task<IReadOnlyList<Question>> GetAssessmentQuestions(string assessmentId);

    }
}
