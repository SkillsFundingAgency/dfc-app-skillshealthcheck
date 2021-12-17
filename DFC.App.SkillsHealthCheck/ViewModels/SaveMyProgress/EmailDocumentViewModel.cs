using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class EmailDocumentViewModel
    {
        public HeadViewModel? HeadViewModel { get; set; }

        public BreadcrumbViewModel? BreadcrumbViewModel { get; set; }

        public EmailViewModel? BodyViewModel { get; set; }
    }
}
