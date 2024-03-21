using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Question;

namespace DFC.App.SkillsHealthCheck.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<DFC.SkillsCentral.Api.Domain.Models.SkillsDocument> GetSkillsDocument(int documentId);

        Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview);

        Task<AssessmentQuestionsOverView> GetAssessmentQuestionsOverview(SessionDataModel sessionDataModel, AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument activeSkillsDocument);

        Task<DFC.SkillsCentral.Api.Domain.Models.SkillsDocument> SubmitAnswer(SessionDataModel sessionDataModel, AssessmentQuestionViewModel model);
    }
}
