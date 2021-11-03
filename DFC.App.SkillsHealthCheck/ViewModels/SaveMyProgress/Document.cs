using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class Document
    {
        public DateTime DateAssessmentsCreated { get; set; }

        public string? Code { get; set; }
    }
}
