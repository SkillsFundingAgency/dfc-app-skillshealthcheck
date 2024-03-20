using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Services.Interfaces
{
    public interface IYourAssessmentsService
    {
        Task<BodyViewModel> GetAssessmentListViewModel(long documentId, IEnumerable<string> selectedJobs);

        DocumentFormatter GetFormatter(DownloadType downloadType);

        Task<DownloadDocumentResponse> GetDownloadDocumentAsync(SessionDataModel sessionDataModel, DocumentFormatter formatter, List<string> selectedJobs);

        Task<bool> GetSkillsDocumentIDByReferenceAndStore(SessionDataModel sessionDataModel, string referenceId);
    }
}
