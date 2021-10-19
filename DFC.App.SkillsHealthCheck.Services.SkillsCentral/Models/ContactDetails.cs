﻿using System.ComponentModel.DataAnnotations;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.CustomAttributes;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    public class ContactDetails
    {
        [StringLength(100, ErrorMessage = "Email address is too long (max. 100)")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [Required(ErrorMessage = "Enter email address")]
        [Display(Name = "Email address", Order = 17)]
        public string ContactEmail { get; set; }

        [MobilePhone(
            DependsOn = "ContactPreference",
            ErrorMessage = "You have selected a contact preference which requires a mobile number")]
        [TelephoneNumber(
            DependsOn = "ContactPreference",
            FirstRegex = ServiceCommon.RegexPatterns.PhoneNumber.ContactPhone,
            SecondRegex = ServiceCommon.RegexPatterns.PhoneNumber.ContactMobilePhone,
            IsAndOperator = false,
            ErrorMessage = "Enter a valid phone number")]
        [Display(Name = "Phone number", Order = 18)]
        public string TelephoneNumber { get; set; }

        [MobilePhone(
            DependsOn = "ContactPreference",
            ErrorMessage = "You have selected a contact preference which requires a mobile number")]
        [DoubleRegex(
            FirstRegex = ServiceCommon.RegexPatterns.PhoneNumber.ContactPhone,
            SecondRegex = ServiceCommon.RegexPatterns.PhoneNumber.ContactMobilePhone,
            IsAndOperator = false,
            IsRequired = false,
            ErrorMessage = "Enter a valid phone number")]
        [Display(Name = "Alternative phone number", Order = 19)]
        public string TelephoneNumberAlternative { get; set; }

        [Display(Name = "Contact by", Order = 20)]
        public Channel ContactPreference { get; set; }
    }
}