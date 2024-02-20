using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfE.SkillsCentral.Api.Domain.Models
{
    public class Assessment
    {
        public string Type { get; set; }
        public string Answers { get; set; }
        public bool Complete { get; set; }
    }

    public class AssessmentWithEaseTiming : Assessment
    {
        public int Ease { get; set; }
        public int Timing { get; set; }
    }

    public class AssessmentWithEaseTimingAndEnjoyment : AssessmentWithEaseTiming
    {
        public int Enjoyment { get; set; }

    }

    public class SkillAreas : Assessment
    {
        public List<string> ExcludedJobFamilies { get; set; }

    }

    public class DataValues
    {
        public AssessmentWithEaseTiming Numeric { get; set; }
        public AssessmentWithEaseTiming Verbal { get; set; }
        public Assessment Motivation { get; set; }
        public Assessment Personal { get; set; }
        public SkillAreas SkillAreas { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment Mechanical { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment Checking { get; set; }
        public Assessment Interest { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment Spatial { get; set; }
        public AssessmentWithEaseTimingAndEnjoyment Abstract { get; set; }
    }
}
