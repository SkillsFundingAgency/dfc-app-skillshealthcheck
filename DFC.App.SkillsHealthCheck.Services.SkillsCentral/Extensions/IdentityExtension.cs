//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
//using SFA.Careers.Citizen.Data.IdentityModel;

//namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Extensions
//{
//    public static class IdentityExtension
//    {
//        internal static CitizenIdentity GetCitizenIdentity(this Identity identity)
//        {
//            var citizenIdentity = new CitizenIdentity
//            {
//                PersonalDetails = new Models.PersonalDetails
//                {
//                    GivenName = identity.PersonalDetails.GivenName,
//                    FamilyName = identity.PersonalDetails.FamilyName,
//                    AddressLine1 = identity.PersonalDetails.AddressLine1,
//                    AddressLine2 = identity.PersonalDetails.AddressLine2,
//                    AddressLine3 = identity.PersonalDetails.AddressLine3,
//                    AddressLine4 = identity.PersonalDetails.AddressLine4,
//                    AddressLine5 = identity.PersonalDetails.AddressLine5,
//                    AlternativePostCode = identity.PersonalDetails.AlternativePostCode,
//                    HomePostCode = identity.PersonalDetails.HomePostCode,
//                    Town = identity.PersonalDetails.Town,
//                    DateOfBirth = identity.PersonalDetails.DateOfBirth,
//                    Gender = identity.PersonalDetails.Gender.GetModelGender(),
//                    Title = identity.PersonalDetails.Title.GetModelTitle()
//                },
//                ContactDetails = new Models.ContactDetails
//                {
//                    ContactEmail = identity.ContactDetails.ContactEmail,
//                    ContactPreference = identity.ContactDetails.ContactPreference.GetModelChannel(),
//                    TelephoneNumber = identity.ContactDetails.TelephoneNumber,
//                    TelephoneNumberAlternative = identity.ContactDetails.TelephoneNumberAlternative
//                },
//                MarketingPreferences = new Models.MarketingPreferences
//                {
//                    OptOutOfMarketResearch = identity.MarketingPreferences.OptOutOfMarketing,
//                    OptOutOfMarketing = identity.MarketingPreferences.OptOutOfMarketResearch
//                }
//            };

//            return citizenIdentity;
//        }

//        /// <summary>
//        /// Gets the model gender.
//        /// </summary>
//        /// <param name="gender">The gender.</param>
//        /// <returns></returns>
//        private static Gender GetModelGender(this SFA.Careers.Citizen.Data.IdentityModel.Gender gender)
//        {
//            switch (gender)
//            {
//                case SFA.Careers.Citizen.Data.IdentityModel.Gender.NotKnown:
//                    return Gender.NotKnown;
//                case SFA.Careers.Citizen.Data.IdentityModel.Gender.NotApplicable:
//                    return Gender.NotApplicable;
//                case SFA.Careers.Citizen.Data.IdentityModel.Gender.Male:
//                    return Gender.Male;
//                case SFA.Careers.Citizen.Data.IdentityModel.Gender.Female:
//                    return Gender.Female;
//                default:
//                    return Gender.NotKnown;
//            }
//        }

//        /// <summary>
//        /// Gets the model title.
//        /// </summary>
//        /// <param name="title">The title.</param>
//        /// <returns></returns>
//        private static Title GetModelTitle(this SFA.Careers.Citizen.Data.IdentityModel.Title title)
//        {
//            switch (title)
//            {
//                case SFA.Careers.Citizen.Data.IdentityModel.Title.NotKnown:
//                    return Title.NotKnown;
//                case SFA.Careers.Citizen.Data.IdentityModel.Title.Dr:
//                    return Title.Dr;
//                case SFA.Careers.Citizen.Data.IdentityModel.Title.Miss:
//                    return Title.Miss;
//                case SFA.Careers.Citizen.Data.IdentityModel.Title.Mr:
//                    return Title.Mr;
//                case SFA.Careers.Citizen.Data.IdentityModel.Title.Mrs:
//                    return Title.Mrs;
//                case SFA.Careers.Citizen.Data.IdentityModel.Title.Ms:
//                    return Title.Ms;
//                default:
//                    return Title.NotKnown;
//            }
//        }

//        /// <summary>
//        /// Gets the model channel.
//        /// </summary>
//        /// <param name="channel">The channel.</param>
//        /// <returns></returns>
//        private static Enums.Channel GetModelChannel(this Channel channel)
//        {
//            switch (channel)
//            {
//                case Channel.Email:
//                    return Enums.Channel.Email;
//                case Channel.Text:
//                    return Enums.Channel.Text;
//                case Channel.Phone:
//                    return Enums.Channel.Phone;
//                default:
//                    return Enums.Channel.Email;
//            }
//        }

//    }
//}
