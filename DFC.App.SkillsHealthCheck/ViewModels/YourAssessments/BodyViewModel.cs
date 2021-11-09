using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;

namespace DFC.App.SkillsHealthCheck.ViewModels.YourAssessments
{
    [ExcludeFromCodeCoverage]
    public class BodyViewModel : IBodyPostback
    {
        public bool? InValidDocumentId { get; set; }

        public bool? IsAPiError { get; set; }

        public string? ApiErrorMessage { get; set; }

        public DateTime? DateAssessmentsCreated { get; set; }

        public IList<AssessmentOverview>? AssessmentsActivity { get; set; }

        public IList<AssessmentOverview>? AssessmentsPersonal { get; set; }

        public IList<AssessmentOverview>? AssessmentsStarted { get; set; }

        public IList<AssessmentOverview>? AssessmentsCompleted { get; set; }

        public DownloadType DownloadType { get; set; }

        public bool? SkillsAssessmentComplete { get; set; }

        public JobFamilyList? JobFamilyList { get; set; }

        public RightBarViewModel? RightBarViewModel { get; set; }
    }
}
