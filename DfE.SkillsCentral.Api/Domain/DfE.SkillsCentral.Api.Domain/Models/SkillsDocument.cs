using DfE.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{

    public class SkillsDocument
    {
        public int? Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public Dictionary<string,string>? DataValueKeys { get; set; } = new()
        {
          {"SkillAreas.Answers", ""},
          {"SkillAreas.Complete", ""},
          {"SkillAreas.ExcludedJobFamilies1", ""},
          {"SkillAreas.ExcludedJobFamilies2", ""},
          {"SkillAreas.ExcludedJobFamilies3", ""},
          {"Interests.Answers", ""},
          {"Interests.Complete", ""},
          {"Personal.Answers", ""},
          {"Personal.Complete", ""},
          {"Motivation.Answers", ""},
          {"Motivation.Complete", ""},
          {"Numerical.Answers", ""},
          {"Numerical.Complete", ""},
          {"Numerical.Ease", ""},
          {"Numerical.Timing", ""},
          {"Verbal.Answers", ""},
          {"Verbal.Complete", ""},
          {"Verbal.Ease", ""},
          {"Verbal.Timing", ""},
          {"Checking.Answers", ""},
          {"Checking.Complete", ""},
          {"Checking.Ease", ""},
          {"Checking.Timing", ""},
          {"Checking.Enjoyment", ""},
          {"Mechanical.Answers", ""},
          {"Mechanical.Complete", ""},
          {"Mechanical.Ease", ""},
          {"Mechanical.Timing", ""},
          {"Mechanical.Enjoyment", ""},
          {"Spatial.Answers", ""},
          {"Spatial.Complete", ""},
          {"Spatial.Ease", ""},
          {"Spatial.Timing", ""},
          {"Spatial.Enjoyment", ""},
          {"Abstract.Answers", ""},
          {"Abstract.Complete", ""},
          {"Abstract.Ease", ""},
          {"Abstract.Timing", ""},
          {"Abstract.Enjoyment", ""}
        };

        public string? ReferenceCode { get; set; }
    }

    
}
