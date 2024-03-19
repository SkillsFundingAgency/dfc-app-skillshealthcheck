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
        SkillsDocument GetSkillsDocument(GetSkillsDocumentRequest getSkillsDocumentRequest);

        AssessmentQuestionViewModel GetAssessmentQuestionViewModel(AssessmentType assessmentType, SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview);

        AssessmentQuestionsOverView GetAssessmentQuestionsOverview(SessionDataModel sessionDataModel, AssessmentType assessmentType, SkillsDocument activeSkillsDocument);

        Task<SaveQuestionAnswerResponse> SubmitAnswer(SessionDataModel sessionDataModel, AssessmentQuestionViewModel model);
    }
}
