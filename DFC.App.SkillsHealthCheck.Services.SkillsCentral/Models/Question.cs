using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    public class Question
    {
        /// <summary>
        /// Gets or sets the assessment title.
        /// </summary>
        /// <value>
        /// The assessment title.
        /// </value>
        public string AssessmentTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the assessment.
        /// </summary>
        /// <value>
        /// The type of the assessment.
        /// </value>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets the type of the question alignment.
        /// </summary>
        /// <value>
        /// The type of the question alignment.
        /// </value>
        public AnswerButtonGroupType AnswerButtonGroupType { get; set; }

        /// <summary>
        /// Gets or sets the accessibility.
        /// </summary>
        /// <value>
        /// The accessibility.
        /// </value>
        public Accessibility Accessibility { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public Level Level { get; set; }

        /// <summary>
        /// Gets or sets the question title.
        /// </summary>
        /// <value>
        /// The question title.
        /// </value>
        public string QuestionTitle { get; set; }

        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        /// <value>
        /// The question text.
        /// </value>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the question number.
        /// </summary>
        /// <value>
        /// The question number.
        /// </value>
        public int QuestionNumber { get; set; }

        /// <summary>
        /// Gets or sets the total question number.
        /// </summary>
        /// <value>
        /// The total question number.
        /// </value>
        public int TotalQuestionNumber { get; set; }

        /// <summary>
        /// Gets or sets the next question number.
        /// </summary>
        /// <value>
        /// The next question number.
        /// </value>
        public int? NextQuestionNumber { get; set; }

        /// <summary>
        /// Gets a value indicating whether [valid next question number].
        /// </summary>
        /// <value>
        /// <c>true</c> if [valid next question number]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidNextQuestionNumber => NextQuestionNumber.HasValue && NextQuestionNumber.Value != -1;

        /// <summary>
        /// Gets or sets the answer headings.
        /// </summary>
        /// <value>
        /// The answer headings.
        /// </value>
        public List<string> AnswerHeadings { get; set; }

        /// <summary>
        /// Gets or sets the possible responses.
        /// </summary>
        /// <value>
        /// The possible responses.
        /// </value>
        public List<Answer> PossibleResponses { get; set; }

        /// <summary>
        /// Gets or sets the report identifier.
        /// </summary>
        /// <value>
        /// The report identifier.
        /// </value>
        public long ReportId { get; set; }

        /// <summary>
        /// Gets or sets the image title.
        /// </summary>
        /// <value>
        /// The image title.
        /// </value>
        public string ImageTitle { get; set; }

        /// <summary>
        /// Gets or sets the image caption.
        /// </summary>
        /// <value>
        /// The image caption.
        /// </value>
        public string ImageCaption { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the question data.
        /// </summary>
        /// <value>
        /// The question data.
        /// </value>
        public string QuestionData { get; set; }

        public string SubTitle { get; set; }
        public string IntroductionText { get; set; }
    }
}
