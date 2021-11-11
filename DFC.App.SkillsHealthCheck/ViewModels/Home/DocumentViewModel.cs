using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Home
{
    [ExcludeFromCodeCoverage]
    public class DocumentViewModel : IDocumentPostback
    {
        public HtmlHeadViewModel HtmlHeadViewModel { get; set; }

        public BodyViewModel BodyViewModel { get; set; }

        public BreadcrumbViewModel BreadcrumbViewModel { get; set; }
    }
}
