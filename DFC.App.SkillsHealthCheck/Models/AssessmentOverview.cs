using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;

namespace DFC.App.SkillsHealthCheck.Models
{
    public class AssessmentOverview
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the name of the assessment.
        /// </summary>
        /// <value>
        /// The name of the assessment.
        /// </value>
        public string AssessmentName { get; set; }
        /// <summary>
        /// Gets or sets the assessment category.
        /// </summary>
        /// <value>
        /// The assessment category.
        /// </value>
        public string AssessmentCategory { get; set; }

        /// <summary>
        /// Gets or sets the duration of the assessment.
        /// </summary>
        /// <value>
        /// The duration of the assessment.
        /// </value>
        public string AssessmentDuration { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the type of the assessment.
        /// </summary>
        /// <value>
        /// The type of the assessment.
        /// </value>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [personal assessment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [personal assessment]; otherwise, <c>false</c>.
        /// </value>
        public bool PersonalAssessment { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [activity assessment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [activity assessment]; otherwise, <c>false</c>.
        /// </value>
        public bool ActivityAssessment { get; set; }
    }
}
