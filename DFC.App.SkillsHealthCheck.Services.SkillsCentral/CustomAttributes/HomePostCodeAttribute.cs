using System;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.CustomAttributes
{
    public class HomePostCodeAttribute : Attribute
    {
        public string UKPostCodeRegex { get; set; }
        public string EnglishOrBFPOPostCodeRegex { get; set; }
        public string BfpoPostCodeRegex { get; set; }

        public HomePostCodeAttribute() { }

        public HomePostCodeAttribute(string ukPostCodeRegex, string englishOrBFPOPostCodeRegex, string bfpoPostCodeRegex)
        {
            UKPostCodeRegex = ukPostCodeRegex;
            EnglishOrBFPOPostCodeRegex = englishOrBFPOPostCodeRegex;
            BfpoPostCodeRegex = bfpoPostCodeRegex;
        }
    }
}
