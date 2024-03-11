using DfE.SkillsCentral.Api.Application.Interfaces.Models;

namespace DFC.SkillsCentral.Api.Application.Interfaces.Services
{
    public interface IAssessmentsService
    {
        Task<AssessmentQuestions?> GetAssessmentQuestions(string assessmentType);
    }
}