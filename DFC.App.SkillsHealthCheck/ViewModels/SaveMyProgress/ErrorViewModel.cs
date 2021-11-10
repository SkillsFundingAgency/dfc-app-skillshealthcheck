using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class ErrorViewModel
    {
        public string? ReturnLink { get; set; }

        public string? ReturnLinkText { get; set; }

        public string? SendTo { get; set; }

        public string? Code { get; set; }
    }
}
