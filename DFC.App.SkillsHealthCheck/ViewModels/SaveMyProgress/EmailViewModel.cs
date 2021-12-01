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

        [Display(Name = "Email", Description = "We will only use this to send you a link to return to your skills health check")]
        [Required(ErrorMessage = "Enter an email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(RegExForEmailAddress, ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
        public string? EmailAddress { get; set; }

        public Document? Document { get; set; }
    }
}
