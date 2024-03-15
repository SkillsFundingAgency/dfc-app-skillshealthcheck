// -----------------------------------------------------------------------
// <copyright file="NumericResult.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;

    /// <summary>
    /// Numeric Result - Entity to store final result for numeric result 
    /// </summary>
    public class NumericResult : SHCResultBase
    {
        #region | Constructor |
        /// <summary>
        /// Initializes a new instance of the NumericResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="ease">string holding ease</param>
        /// <param name="timing">string holding timing</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public NumericResult(string qualificationLevel, string ease, string timing, string type, string answers, string complete) :
            base(SHCReportSection.Numbers.ToString(), qualificationLevel, type, answers, complete)
        {
            if (!string.IsNullOrEmpty(ease))
            {
                this.Ease = Convert.ToInt32(ease);
            }

            if (!string.IsNullOrEmpty(timing))
            {
                this.Timing = Convert.ToInt32(timing);
            }
        }

        /// <summary>
        /// Initializes a new instance of the NumericResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="ease">string holding ease</param>
        /// <param name="timing">string holding timing</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public NumericResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Numbers.ToString(), qualificationLevel, type, answers, complete)
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
            else if (questionsAttempted > 6 && questionsAttempted <= 8)
            {
                style = 2;
            }
            else if (questionsAttempted > 8)
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
            else if (rawScore >= 0.3 && rawScore <= 0.689)
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
        /// Computes overall potential based on number of questions answered correct
        /// </summary>
        /// <param name="questionsCorrect">int holding the number of of questions answered correct</param>
        /// <returns>int holding the overall potential</returns>
        private int GetOverallPotential(int questionsCorrect)
        {
            int overallPotential = 1;

            if (questionsCorrect <= 2)
            {
                overallPotential = 1;
            }
            else if (questionsCorrect > 2 && questionsCorrect <= 6)
            {
                overallPotential = 2;
            }
            else if (questionsCorrect > 6)
            {
                overallPotential = 3;
            }

            return overallPotential;
        }

        #endregion

        #region | Public Methods |

        /// <summary>
        /// Runs numerical calculation
        /// </summary>
        /// <returns>Returns xml string holding the numerical result set</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return this.GetXML(null, Constant.XmlNumericalRootElement);
            }
            else
            {                
                string correctAnswers = null;
                if (this.Type.ToLower().StartsWith(Constant.NumericalQuestionTypeName))
                {
                    correctAnswers = this.Resource.GetString(Constant.NumericalQuestionSetB);
                }
                else
                {
                    correctAnswers = this.Resource.GetString(Constant.NumericalQuestionSetA);
                }

                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);

                int style = GetStyle(questionsAttempted);

                double rawscore = (double)questionsCorrect / questionsAttempted;

                int accuracy = GetAccuracy(rawscore);

                int overallPotential = GetOverallPotential(questionsCorrect);

                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add(Constant.XmlEaseElement, this.Ease.ToString());
                items.Add(Constant.XmlTimingElement, this.Timing.ToString());
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());
                items.Add(Constant.XmlStyleElement, style.ToString());
                items.Add(Constant.XmlAccuracyElement, accuracy.ToString());
                items.Add(Constant.XmlOverallPotentialElement, overallPotential.ToString());

                return this.GetXML(items, Constant.XmlNumericalRootElement);
            }
        }

        /// <summary>
        /// Runs numerical calculation
        /// </summary>
        /// <returns>Returns xml string holding the numerical result set</returns>
        public override string GetSummaryResult()
        {
            if (!this.IsComplete)
            {
                return this.GetXML(null, Constant.XmlNumericalRootElement);
            }
            else
            {
                string correctAnswers = null;
                if (this.Type.ToLower().StartsWith(Constant.NumericalQuestionTypeName))
                {
                    correctAnswers = this.Resource.GetString(Constant.NumericalQuestionSetB);
                }
                else
                {
                    correctAnswers = this.Resource.GetString(Constant.NumericalQuestionSetA);
                }

                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);
                
                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());      

                return this.GetXML(items, Constant.XmlNumericalRootElement);
            }
        }
        #endregion        
    }
}
