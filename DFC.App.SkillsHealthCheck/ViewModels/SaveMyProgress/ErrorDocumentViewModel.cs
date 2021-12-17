using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class ErrorDocumentViewModel
    {
        public HeadViewModel? HeadViewModel { get; set; }

        public BreadcrumbViewModel? BreadcrumbViewModel { get; set; }

        public ErrorViewModel? BodyViewModel { get; set; }
    }
}
