using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Question;

namespace DFC.App.SkillsHealthCheck.Services.Interfaces
{
    public interface IQuestionService
    {
        AssessmentQuestionViewModel GetAssessmentQuestionViewModel(ISkillsHealthCheckService skillsHealthCheckService, Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview);

        AssessmentQuestionsOverView GetAssessmentQuestionsOverview(ISkillsHealthCheckService skillsHealthCheckService, Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument activeSkillsDocument);
    }
}
