using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DfE.SkillsCentral.Api.Domain.Models
{
    public class Assessment
    {
        public List<string>? Answers { get; set; }
        public bool? Complete { get; set; }
    }

    public class AssessmentWithEaseTiming : Assessment
    {
        public int? Ease { get; set; }
        public int? Timing { get; set; }
    }

    public class AssessmentWithEaseTimingAndEnjoyment : AssessmentWithEaseTiming
    {
        public int? Enjoyment { get; set; }

    }

    public class SkillAreas : Assessment
    {
        public List<string>? ExcludedJobFamilies { get; set; }

    }

    public class DataValues
    {
        public SkillAreas? SkillAreas { get; set; }
        public Assessment? Interest { get; set; }
        public Assessment? Personal { get; set; }
        public Assessment? Motivation { get; set; }
        public AssessmentWithEaseTiming? Numeric { get; set; }
        public AssessmentWithEaseTiming? Verbal { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment? Checking { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment? Mechanical { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment? Spatial { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment? Abstract { get; set; }


       // public static SkillAreas ParseSkillAreas(JToken token)
       // {
       //     if (token == null)
       //         return null;
       //     return new SkillAreas
       //     {
       //         Answers = token["Answers"].ToObject<List<string>>(),
       //         Complete = token["Complete"].ToObject<bool?>(),
       //         ExcludedJobFamilies = token["ExcludedJobFamilies"].ToObject<List<string>>()
       //     };
       // }

       // public static Assessment ParseAssessment(JToken token)
       // {
       //     if (token == null)
       //         return null;
       //     return new Assessment
       //     {
       //         Answers = token["Answers"].ToObject<List<string>>(),
       //         Complete = token["Complete"].ToObject<bool?>()
       //     };
       // }

       //public static AssessmentWithEaseTiming ParseAssessmentWithEaseTiming(JToken token)
       // {
       //     if (token == null)
       //         return null;
       //     return new AssessmentWithEaseTiming
       //     {
       //         Answers = token["Answers"].ToObject<List<string>>(),
       //         Complete = token["Complete"].ToObject<bool?>(),
       //         Ease = token["Ease"].ToObject<int?>(),
       //         Timing = token["Timing"].ToObject<int?>()
       //     };
       // }

       // public static AssessmentWithEaseTimingAndEnjoyment ParseAssessmentWithEaseTimingAndEnjoyment(JToken token)
       // {
       //     if (token == null)
       //         return null;
       //     return new AssessmentWithEaseTimingAndEnjoyment
       //     {
       //         Answers = token["Answers"].ToObject<List<string>>(),
       //         Complete = token["Complete"].ToObject<bool?>(),
       //         Ease = token["Ease"].ToObject<int?>(),
       //         Timing = token["Timing"].ToObject<int?>(),
       //         Enjoyment = token["Enjoyment"].ToObject<int?>()
       //     };
       // }
    }


}






   

