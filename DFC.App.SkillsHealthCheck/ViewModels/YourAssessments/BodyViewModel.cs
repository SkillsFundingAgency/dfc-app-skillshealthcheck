using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DFC.App.SkillsHealthCheck.Models;

namespace DFC.App.SkillsHealthCheck.ViewModels.YourAssessments
{
    [ExcludeFromCodeCoverage]
    public class BodyViewModel
    {
        public DateTime DateAssessmentsCreated { get; set; }

        public IList<AssessmentOverview> AssessmentsActivity { get; set; }

        public IList<AssessmentOverview> AssessmentsPersonal { get; set; }

        public IList<AssessmentOverview> AssessmentsCompleted { get; set; }

        public RightBarViewModel RightBarViewModel { get; set; }
    }
}
