﻿using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    /// <summary>
    /// Feedback Question Model
    /// </summary>
    /// <seealso cref="AssessmentQuestionViewModel" />
    public class FeedBackQuestionViewModel : AssessmentQuestionViewModel
    {
        /// <summary>
        /// Gets or sets the feedback question.
        /// </summary>
        /// <value>
        /// The feedback question.
        /// </value>
        public FeedbackQuestion FeedbackQuestion { get; set; }
        /// <summary>
        /// Gets or sets the assessment title.
        /// </summary>
        /// <value>
        /// The assessment title.
        /// </value>
        public string AssessmentTitle { get; set; }

    }
}