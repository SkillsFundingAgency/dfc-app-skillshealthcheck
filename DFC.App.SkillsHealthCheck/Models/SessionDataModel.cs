using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Models
{
    public class SessionDataModel
    {
        public long DocumentId { get; set; }

        public Dictionary<string, AssessmentQuestionsOverView> AssessmentQuestionsOverViews { get; set; }

        public string? AssessmentType { get; set; }
    }
}
