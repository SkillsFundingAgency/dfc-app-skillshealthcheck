using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.Interfaces
{
    public interface IYourAssessmentsService
    {
        BodyViewModel GetAssessmentListViewModel(long documentId);

        DocumentFormatter GetFormatter(DownloadType downloadType);

        DownloadDocumentResponse GetDownloadDocument(SessionDataModel sessionDataModel, DocumentFormatter formatter, out string documentTitle);
    }
}
