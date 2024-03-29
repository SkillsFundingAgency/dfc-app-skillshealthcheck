﻿using System.Diagnostics.CodeAnalysis;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;

namespace DFC.App.SkillsHealthCheck.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class RightBarViewModel
    {
        public string AssessmentType { get; set; }

        public SharedContentItemModel SpeakToAnAdviser { get; set; }

        public ReturnToAssessmentViewModel ReturnToAssessmentViewModel { get; set; }
    }
}
