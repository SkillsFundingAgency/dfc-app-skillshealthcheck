using System;
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

    [ExcludeFromCodeCoverage]
    public class EmailViewModel
    {
        public string? ReturnLink { get; set; }

        public string? ReturnLinkText { get; set; }
    }

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
        public string PhoneNumber { get; set; }
    }

    public class Document
    {
        public DateTime DateAssessmentsCreated { get; set; }

        public string? Code { get; set; }
    }

}
