using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using System.Collections.Generic;

namespace DFC.App.SkillsHealthCheck.Services.Interfaces
{
    public interface IYourAssessmentsService
    {
        BodyViewModel GetAssessmentListViewModel(long documentId, IEnumerable<string> selectedJobs = null);
    }
}
