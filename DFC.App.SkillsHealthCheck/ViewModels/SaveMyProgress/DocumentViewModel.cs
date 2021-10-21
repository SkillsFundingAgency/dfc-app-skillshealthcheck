using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class DocumentViewModel
    {
        public HtmlHeadViewModel? HtmlHeadViewModel { get; internal set; }

        public BreadcrumbViewModel? BreadcrumbViewModel { get; internal set; }

        public SaveMyProgressViewModel? SaveMyProgressViewModel { get; internal set; }
    }
}
