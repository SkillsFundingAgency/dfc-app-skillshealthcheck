// -----------------------------------------------------------------------
// <copyright file="ShapesResult.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;

    /// <summary>
    /// ShapesResult - Entity to store final result for working with shapes
    /// </summary>
    public class ShapesResult : SHCResultBase 
    {
        #region | Constructor |
        /// <summary>
        /// Initializes a new instance of the ShapesResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="ease">string holding ease</param>
        /// <param name="timing">string holding timing</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        /// <param name="enjoyment">string holding enjoyment</param>
        public ShapesResult(string qualificationLevel, string ease, string timing, string type, string answers, string complete, string enjoyment) :
            base(SHCReportSection.Shapes.ToString(), qualificationLevel, type, answers, complete)
        {
            if (!string.IsNullOrEmpty(ease))
            {
                this.Ease = Convert.ToInt32(ease);
            }

            if (!string.IsNullOrEmpty(timing))
            {
                this.Timing = Convert.ToInt32(timing);
            }

            if (!string.IsNullOrEmpty(enjoyment))
            {
                this.Enjoyment = Convert.ToInt32(enjoyment);
            }
        }

        /// <summary>
        /// Initializes a new instance of the ShapesResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public ShapesResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Shapes.ToString(), qualificationLevel, type, answers, complete)
        {
        }
        #endregion

        #region | Properties |

        /// <summary>
        /// Get / Set ease
        /// </summary>
        public int Ease { get; set; }

        /// <summary>
        /// Get / Set timing
        /// </summary>
        public int Timing { get; set; }

        /// <summary>
        /// Get / Set enjoyment
        /// </summary>
        public int Enjoyment { get; set; }

        #endregion

        #region | Private Methods |

        /// <summary>
        /// Computes style based on questions attempted
        /// </summary>
        /// <param name="questionsAttempted">int holding the number of questions attempted</param>
        /// <returns>int holding the style</returns>
        private int GetStyle(int questionsAttempted) 
        {
            int style = 1;

            if (questionsAttempted <= 6)
            {
                style = 1;
            }
            else if (questionsAttempted > 6 && questionsAttempted <= 10)
            {
                style = 2;
            }
            else if (questionsAttempted > 10)
            {
                style = 3;
            }

            return style;
        }

        /// <summary>
        /// Computes accuracy based on raw score
        /// </summary>
        /// <param name="rawScore">int holding the raw score</param>
        /// <returns>int holding the accuracy</returns>
        private int GetAccuracy(double rawScore)
        {
            int accuracy = 1;

            if (rawScore >= 0 && rawScore <= 0.2999)
            {
                accuracy = 1;
            }
            else if (rawScore >= 0.3 && rawScore <= 0.6899)
            {
                accuracy = 2;
            }
            else if (rawScore >= 0.69 && rawScore <= 0.8999)
            {
                accuracy = 3;
            }
            else if (rawScore >= 0.9 && rawScore <= 1.0)
            {
                accuracy = 4;
            }

            return accuracy;
        }

        /// <summary>
        /// Computes question correct band
        /// </summary>
        /// <param name="questionsCorrect">int holding total number of correct question</param>
        /// <param name="totalQuestions">int holding total number of questions</param>
        /// <returns>return question correct band as integer</returns>
        private int GetQuestionCorrectBand(int questionsCorrect, int totalQuestions)
        {
            int questionCorrectBand = 0;

            if (questionsCorrect > 0 && questionsCorrect <= 4)
            {
                questionCorrectBand = 1;
            }
            else if (questionsCorrect > 4 && questionsCorrect <= 9)
            {
                questionCorrectBand = 2;
            }
            else if (questionsCorrect > 9 && questionsCorrect <= totalQuestions)
            {
                questionCorrectBand = 3;
            }

            return questionCorrectBand;
        }
    
        #endregion

        #region | Public Methods |
        /// <summary>
        /// Runs Shapes calculation
        /// </summary>
        /// <returns>Returns xml string holding the mechanical result set</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlShapesRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.ShapesCorrectAnswer);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);

                int questionsAttempted = this.GetNumberOfQuestionsAttempted();

                int totalQuestions = this.GetQuestionCount(correctAnswers);

                int style = GetStyle(questionsAttempted);

                int questionBand = GetQuestionCorrectBand(questionsCorrect, totalQuestions);

                double rawscore = (double)questionsCorrect / questionsAttempted;

                int accuracy = GetAccuracy(rawscore);

                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add(Constant.XmlTimingElement, this.Timing.ToString());
                items.Add(Constant.XmlEaseElement, this.Ease.ToString());
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());
                items.Add(Constant.XmlQuestionsCorrectBandElement, questionBand.ToString());
                items.Add(Constant.XmlStyleElement, style.ToString());
                items.Add(Constant.XmlAccuracyElement, accuracy.ToString());
                items.Add(Constant.XmlEnjoymentElement, this.Enjoyment.ToString());
                return GetXML(items, Constant.XmlShapesRootElement);
            }
        }

        /// <summary>
        /// Runs Shapes calculation
        /// </summary>
        /// <returns>Returns xml string holding the mechanical result set</returns>
        public override string GetSummaryResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlShapesRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.ShapesCorrectAnswer);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);
               
                Dictionary<string, string> items = new Dictionary<string, string>();               
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());

                return GetXML(items, Constant.XmlShapesRootElement);
            }
        }
        #endregion       
    }
}
