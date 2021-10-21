using System.Collections.Generic;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    public class TabularAnswerQuestionViewModel : AssessmentQuestionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabularAnswerQuestionViewModel"/> class.
        /// </summary>
        public TabularAnswerQuestionViewModel()
        {
            CurrentQuestion = 1;
        }
        /// <summary>
        /// Gets or sets the current question.
        /// </summary>
        /// <value>
        /// The current question.
        /// </value>
        public int CurrentQuestion { get; set; }

        /// <summary>
        /// Gets or sets the sub questions.
        /// </summary>
        /// <value>
        /// The sub questions.
        /// </value>
        public int SubQuestions { get; set; }

        /// <summary>
        /// Gets or sets the answer selection.
        /// </summary>
        /// <value>
        /// The answer selection.
        /// </value>
        public IEnumerable<string> AnswerSelection { get; set; }
    }
}
