using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Home
{
    [ExcludeFromCodeCoverage]
    public class DocumentViewModel
    {
        public HtmlHeadViewModel HtmlHeadViewModel { get; set; }
        public BodyViewModel HomeBodyViewModel { get; set; }
        public BreadcrumbViewModel BreadcrumbViewModel { get; set; }
    }
}
