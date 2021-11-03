using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class GetCodeViewModel
    {
        public HtmlHeadViewModel? HtmlHeadViewModel { get; set; }

        public BreadcrumbViewModel? BreadcrumbViewModel { get; set; }

        public ReferenceNumberViewModel? BodyViewModel { get; set; }
    }
}
