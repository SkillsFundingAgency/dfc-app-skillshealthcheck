using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress
{
    [ExcludeFromCodeCoverage]
    public class EmailViewModel
    {
        private const string RegExForEmailAddress = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

        public string? ReturnLink { get; set; }

        public string? ReturnLinkText { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter an email address")]
        [RegularExpression(RegExForEmailAddress, ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }
    }
}
