using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;


namespace DFC.SkillsCentral.Api.Application.Interfaces.Services
{
    public interface IAssessmentsService
    {



        //Saves user's given answer to the current question
        Task SaveSkillsDocument(SkillsDocument skillsDocument);

        //Gets all questions in a given assessment
        Task<AssessmentQuestions> GetAssessmentQuestions(string assessmentType);

    }
}
