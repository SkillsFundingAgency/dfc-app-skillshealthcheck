using System;
using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    /// <summary>
    /// Assessment Question Overview Data
    /// </summary>
    [Serializable]
    public class AssessmentQuestionsOverView
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentQuestionsOverView"/> class.
        /// </summary>
        public AssessmentQuestionsOverView()
        {
            QuestionOverViewList = new List<QuestionOverView>();
        }
        /// <summary>
        /// Gets or sets the type of the assessment.
        /// </summary>
        /// <value>
        /// The type of the assessment.
        /// </value>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets the question over view list.
        /// </summary>
        /// <value>
        /// The question over view list.
        /// </value>
        public List<QuestionOverView> QuestionOverViewList { get; set; }

        /// <summary>
        /// Gets or sets the total questions number.
        /// </summary>
        /// <value>
        /// The total questions number.
        /// </value>
        public int TotalQuestionsNumber { get; set; }

        /// <summary>
        /// Gets or sets the total questions number plus feedback.
        /// </summary>
        /// <value>
        /// The total questions number plus feedback.
        /// </value>
        public int TotalQuestionsNumberPlusFeedback { get; set; }

        /// <summary>
        /// Gets or sets the actual questions number.
        /// </summary>
        /// <value>
        /// The actual questions number.
        /// </value>
        public int ActualQuestionsNumber { get; set; }

        /// <summary>
        /// Gets or sets the actual questions number plus feedback.
        /// </summary>
        /// <value>
        /// The actual questions number plus feedback.
        /// </value>
        public int ActualQuestionsNumberPlusFeedback { get; set; }

        public string AssessmentTitle { get; set; }
        public string SubTitle { get; set; }

        public string IntroductionText { get; set; }
    }
}
