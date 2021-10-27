using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using DFC.App.SkillsHealthCheck.Enums;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class SaveMyProgressViewModel
    {
        public const string SelectedOptionValidationError = "Choose how you would like to return to your skills health check";

        [Required(ErrorMessage = SelectedOptionValidationError)]
        [Range((int)SaveMyProgressOption.Email, (int)SaveMyProgressOption.ReferenceCode, ErrorMessage = SelectedOptionValidationError)]
        [EnumDataType(typeof(SaveMyProgressOption))]
        public SaveMyProgressOption? SelectedOption { get; set; }

        public string? ReturnLink { get; set; }

        public string? ReturnLinkText { get; set; }
    }
}
