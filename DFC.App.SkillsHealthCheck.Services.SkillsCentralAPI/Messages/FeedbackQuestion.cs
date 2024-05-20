using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using System.Collections.Generic;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class FeedbackQuestion
    {
        public string? Question { get; set; }

        public string DocValueTitle { get; set; }

        public List<FeedbackAnswer>? FeedbackAnswers { get; set; }

        public string? Answer { get; set; }

        /// <summary>
        /// Gets or sets the type of the assessment.
        /// </summary>
        /// <value>
        /// The type of the assessment.
        /// </value>
        public AssessmentType AssessmentType { get; set; }

        public Level Level { get; set; }
        public Accessibility Accessibility { get; set; }
    }
}
