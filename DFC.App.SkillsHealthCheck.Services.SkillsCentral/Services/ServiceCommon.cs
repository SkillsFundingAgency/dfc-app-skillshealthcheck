using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

using Microsoft.AspNetCore.Routing;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services
{
    public static class ServiceCommon
    {
        public static class RegexPatterns
        {
            internal static RegexOptions commonRegexOptions = RegexOptions.Compiled | RegexOptions.CultureInvariant;
            public static class PostCode
            {
                public const string Postcode = @"^[A-Za-z0-9-.\(\)\/\\\s]*$";
                public const string UKPostCode = @"^([bB][fF][pP][oO]\s{0,1}[0-9]{1,4}|[gG][iI][rR]\s{0,1}0[aA][aA]|[a-pr-uwyzA-PR-UWYZ]([0-9]{1,2}|([a-hk-yA-HK-Y][0-9]|[a-hk-yA-HK-Y][0-9]([0-9]|[abehmnprv-yABEHMNPRV-Y]))|[0-9][a-hjkps-uwA-HJKPS-UW])\s{0,1}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2})$";
                public const string EnglishOrBFPOPostCode = @"^(?!ab|bt|cf|ch5|ch6|ch7|ch8|dd|dg|eh|fk|g[0-9]|gy|hs|im|iv|je|ka|kw|ky|ld|ll|ml|np|pa|ph|sa|sy|td|ze)+.*$";
                public const string BfpoPostCode = @"^([bB][fF][pP][oO]\s{0,1}[0-9]{1,4})$";
            }
            public static class PhoneNumber
            {
                public const string ContactPhone = @"^[+0]{1}[0-9( )-]{10,19}$";
                public const string ContactMobilePhone = @"^((\+44)|(0044)|(0))7([1-5]|[7-9])[0-9]{8}$";
            }
            public static class Other
            {
                public const string Name = @"^[A-Za-z'-. `]*$";
                public const string Email = @"^[A-Za-z0-9_\-\+/{\t\n\r}#$%^\\&\[\]*=()|?'~`! :]+([.][A-Za-z0-9_\-\+/{\t\n\r}#$%^\\&\[\]*=()|?'~`! :]+)*@[A-Za-z0-9\[\]:]+([.-][A-Za-z0-9\[\]:]+)*\.[A-Za-z0-9\[\]:]+([-.][A-Za-z0-9\[\]:]+)*$";
                public const string AddressString = @"^[A-Za-z0-9'-. `_,:&*#\""\(\)\/\\]*$";
                public const string Day = @"^(0[1-9]|[1-9]|[1-2][0-9]|3[0-1])$";
                public const string Month = @"^(0[1-9]|[1-9]|1[0-2])$";
                public const string Numeric = @"^[0-9]*$";
            }

            /// <summary>
            /// Course directory Regex patterns
            /// </summary>
            public static class CourseDirectory
            {
                /// <summary>
                /// The no invalid characters
                /// </summary>
                public const string NoInvalidCharacters = @"^[a-zA-Z0-9& \(\)\+:'’,\./]*$";
            }

            /// <summary>
            /// Job Profile Regex patterns
            /// </summary>
            public static class JobProfile
            {
                /// <summary>
                /// The no invalid characters
                /// </summary>
                public const string NoInvalidCharacters = @"^[a-zA-Z0-9& \(\)\+:'’,\./]*$";
            }
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            return
                !string.IsNullOrWhiteSpace(emailAddress) &&
                Regex.IsMatch(emailAddress, RegexPatterns.Other.Email, RegexPatterns.commonRegexOptions);
        }

        public static bool IsValidUkPostcode(string postalCode)
        {
            return
                !string.IsNullOrWhiteSpace(postalCode) &&
                Regex.IsMatch(postalCode, RegexPatterns.PostCode.UKPostCode, RegexPatterns.commonRegexOptions);
        }

        public static bool IsValidBfpoPostcode(string postalCode)
        {
            return
                !string.IsNullOrWhiteSpace(postalCode) &&
                Regex.IsMatch(postalCode, RegexPatterns.PostCode.UKPostCode, RegexPatterns.commonRegexOptions) &&
                Regex.IsMatch(postalCode, RegexPatterns.PostCode.BfpoPostCode, RegexPatterns.commonRegexOptions);
        }

        public static bool IsValidUKEnglandOrBfpoPostcode(string postalCode)
        {
            return
                !string.IsNullOrWhiteSpace(postalCode) &&
                Regex.IsMatch(postalCode, RegexPatterns.PostCode.UKPostCode, RegexPatterns.commonRegexOptions) &&
                Regex.IsMatch(postalCode, RegexPatterns.PostCode.EnglishOrBFPOPostCode, RegexPatterns.commonRegexOptions);
        }
        public static bool IsValidContactPhoneOrMobileNumber(string contactNumber)
        {
            return !string.IsNullOrWhiteSpace(contactNumber) &&
                (
                    Regex.IsMatch(contactNumber, RegexPatterns.PhoneNumber.ContactPhone, RegexPatterns.commonRegexOptions) ||
                    Regex.IsMatch(contactNumber, RegexPatterns.PhoneNumber.ContactMobilePhone, RegexPatterns.commonRegexOptions)
                );
        }
        public static bool IsValidContactPhoneNumber(string contactNumber)
        {
            return !string.IsNullOrWhiteSpace(contactNumber) &&
                Regex.IsMatch(contactNumber, RegexPatterns.PhoneNumber.ContactPhone, RegexPatterns.commonRegexOptions);
        }
        public static bool IsValidContactMobilePhoneNumber(string contactNumber)
        {
            return !string.IsNullOrWhiteSpace(contactNumber) &&
                Regex.IsMatch(contactNumber, RegexPatterns.PhoneNumber.ContactMobilePhone, RegexPatterns.commonRegexOptions);
        }
        public static bool ActionPlanActionStatusSelectorIsEnabled(string currentStatus)
        {
            if (!string.IsNullOrWhiteSpace(currentStatus) &&
                (
                    currentStatus.Equals("", StringComparison.OrdinalIgnoreCase) ||
                    currentStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase)
                )
              )
            {
                return false; //@"disabled= ""disabled""";
            }
            return true;
        }

        public static IDictionary<string, string> GetActionPlanStatusOptions(string currentStatus)
        {
            if (string.IsNullOrWhiteSpace(currentStatus) == true)
            {
                return ACTION_PLAN_ACTION_STATUSES;
            }
            else
            {
                return ACTION_PLAN_ACTION_STATUSES.Where((x, i) => i >= ACTION_PLAN_ACTION_STATUSES.Values.ToList().IndexOf(currentStatus)).ToDictionary(x => x.Key, x => x.Value);
            }
        }

        private static readonly IDictionary<string, string> ACTION_PLAN_ACTION_STATUSES =
            new Dictionary<string, string>() {
                { "", "" },
                { "Not Applicable", "NoLongerApplicable" },
                { "Not Started", "PendingNotYetStarted" },
                { "In Progress", "InProgress" },
                { "Completed", "Completed" }
            };

        //this can be moved to configuration
        public static readonly string SERVICE_NAME = "san.id";
        //this can be moved to configuration - there is no web service call I am aware of, which can return following information.
        internal static readonly List<DocumentType> DOCUMENT_TYPES =
            new List<DocumentType> {
                new DocumentType {
                    Title = "Adviser Created Action Plan",
                    TypeCode = DocumentTypeCode.ACAP,
                    DocumentTypes = new List<string> {
                        "ap.advised.1"
                    },
                    Formatters = new List<DocumentFormatter>()
                },
                new DocumentType {
                    Title = "CV",
                    TypeCode =DocumentTypeCode.CV,
                    DocumentTypes = new List<string> {
                        "cv.1.0"
                    },
                    Formatters = new List<DocumentFormatter>(){
                        {
                            new DocumentFormatter() {
                                Title = "Pdf",
                                FileExtension = DocumentFileExtensions.Pdf,
                                ContentType = DocumentContentTypes.Pdf,
                                FormatterName = "CVPDFFormatter"
                            }
                        },
                        {
                            new DocumentFormatter() {
                                Title = "Word",
                                FileExtension = DocumentFileExtensions.Docx,
                                ContentType = DocumentContentTypes.Docx,
                                FormatterName = "CVDocxFormatter"
                            }
                        }
                    }
                },
                new DocumentType {
                    Title = "Skills Health Check",
                    TypeCode = DocumentTypeCode.SHC,
                    DocumentTypes = new List<string> {
                        "sdt.dr.6"
                    },
                    Formatters = new List<DocumentFormatter>{
                        new DocumentFormatter
                        {
                            Title = "Full pdf",
                            FileExtension = DocumentFileExtensions.Pdf,
                            ContentType = DocumentContentTypes.Pdf,
                            FormatterName = "SHCFullPdfFormatter"
                        },
                        new DocumentFormatter
                        {
                            Title = "Full word",
                            FileExtension = DocumentFileExtensions.Docx,
                            ContentType = DocumentContentTypes.Docx,
                            FormatterName = "SHCFullDocxFormatter"
                        }
                    }
                },
                new DocumentType {
                    Title = "Personal Action Plan",
                    TypeCode =DocumentTypeCode.PAP,
                    DocumentTypes = new List<string>
                    {
                        "ap.personal.7"
                    },
                    Formatters = new List<DocumentFormatter>()
                    {
                        {
                            new DocumentFormatter() {
                                Title = "Pdf",
                                FileExtension = DocumentFileExtensions.Pdf,
                                ContentType = DocumentContentTypes.Pdf,
                                FormatterName = "ActionPlanPdfFormatter"
                            }
                        },
                        {
                            new DocumentFormatter() {
                                Title = "Word",
                                FileExtension = DocumentFileExtensions.Docx,
                                ContentType = DocumentContentTypes.Docx,
                                FormatterName = "ActionPlanDocxFormatter"
                            }
                        }
                    }
                }
            };

        public static DocumentFormatter GetDocumentFormatter(DocumentTypeCode skillsDocumentType, string targetFormat)
        {
            return DOCUMENT_TYPES.
                FirstOrDefault(x => x.TypeCode.Equals(skillsDocumentType)).
                Formatters.
                FirstOrDefault(x => x.Title.Equals(targetFormat, StringComparison.OrdinalIgnoreCase));
        }

        public static DocumentType GetDocumentType(this DocumentTypeCode skillsDocumentType)
        {
            return DOCUMENT_TYPES.FirstOrDefault(x => x.TypeCode.Equals(skillsDocumentType));
        }

        public static DocumentType GetDocumentType(this string skillsDocumentType)
        {
            return DOCUMENT_TYPES.FirstOrDefault(x => x.DocumentTypes.Contains(skillsDocumentType));
        }

        public static IEnumerable<dynamic> JsonArrayToExpando(string item)
        {
            var converter = new ExpandoObjectConverter();
            return JsonConvert.DeserializeObject<ExpandoObject[]>(item, converter);
        }

        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = anonymousObject.AnonymousObjectToHtmlAttributes();
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
            {
                expando.Add(item);
            }
            return (ExpandoObject)expando;
        }

        internal static RouteValueDictionary AnonymousObjectToHtmlAttributes(this object htmlAttributes)
        {
            RouteValueDictionary routeValueDictionaries = new RouteValueDictionary();
            if (htmlAttributes != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(htmlAttributes))
                {
                    routeValueDictionaries.Add(property.Name.Replace('\u005F', '-'), property.GetValue(htmlAttributes));
                }
            }
            return routeValueDictionaries;
        }

        public static string GetDisplayName(this Enum value)
        {
            try
            {
                DisplayAttribute customAttribute = value.GetType().GetMember(value.ToString()).FirstOrDefault().GetCustomAttribute<DisplayAttribute>();
                return customAttribute != null ? customAttribute.Name : value.ToString();
            }
            catch
            {
                return value.ToString();
            }
        }
    }
}

