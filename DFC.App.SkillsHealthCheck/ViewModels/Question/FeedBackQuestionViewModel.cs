using System.Diagnostics.CodeAnalysis;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    /// <summary>
    /// Feedback Question Model
    /// </summary>
    /// <seealso cref="AssessmentQuestionViewModel" />
    [ExcludeFromCodeCoverage]
    public class FeedBackQuestionViewModel : AssessmentQuestionViewModel
    {
        /// <summary>
        /// Gets or sets the feedback question.
        /// </summary>
        /// <value>
        /// The feedback question.
        /// </value>
        public FeedbackQuestion FeedbackQuestion { get; set; }
    }
}
