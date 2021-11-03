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
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", ErrorMessage = "Enter a mobile phone number, like 07700 900 982.")]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }
    }
}
