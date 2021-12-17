using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class GetCodeViewModel
    {
        public HeadViewModel? HeadViewModel { get; set; }

        public BreadcrumbViewModel? BreadcrumbViewModel { get; set; }

        public ReferenceNumberViewModel? BodyViewModel { get; set; }
    }
}
