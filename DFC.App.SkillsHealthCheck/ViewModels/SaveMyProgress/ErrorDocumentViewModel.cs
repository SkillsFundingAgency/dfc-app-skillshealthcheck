using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class ErrorDocumentViewModel
    {
        public HtmlHeadViewModel? HtmlHeadViewModel { get; set; }

        public BreadcrumbViewModel? BreadcrumbViewModel { get; set; }

        public ErrorViewModel? BodyViewModel { get; set; }
    }
}
