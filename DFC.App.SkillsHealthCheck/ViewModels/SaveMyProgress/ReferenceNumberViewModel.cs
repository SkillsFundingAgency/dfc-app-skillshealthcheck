using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class ReferenceNumberViewModel
    {
        public string? ReturnLink { get; set; }

        public string? ReturnLinkText { get; set; }

        public Document? Document { get; set; }

        [Required(ErrorMessage = "Enter a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(07[\d]{3}|\+447[\d]{3})[ ]?[\d]{3}[ ]?[\d]{3}$", ErrorMessage = "Enter a mobile phone number, like 07700 900 982.")]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }
    }
}