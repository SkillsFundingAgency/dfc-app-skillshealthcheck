// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;

    /// <summary>
    ///  MechanicalResult- Entity to store final result for mechanical assessment
    /// </summary>
    public class MechanicalResult : SHCResultBase
    {
        #region | Enums |
        /// <summary>
        /// Defines mechanical type
        /// </summary>
        public enum MechanicalType
        {
            /// <summary>
            /// Default value
            /// </summary>
            NoType = 0,

            /// <summary>
            /// Physical priciple is high
            /// </summary>
            PPHigh = 1,

            /// <summary>
            /// movement of object is high
            /// </summary>
            MOHigh = 2,

            /// <summary>
            /// structure and weight is high
            /// </summary>
            SWHigh = 3,

            /// <summary>
            /// physical principle and movement of objects are equal and high
            /// </summary>
            PPMOHigh = 4,

            /// <summary>
            /// structure and weight and physical principle are equal and high
            /// </summary>
            SWPPHigh = 5,

            /// <summary>
            /// structure and weight and movement of objects are equal and high
            /// </summary>
            SWMOHigh = 6,

            /// <summary>
            /// all are high
            /// </summary>
            AllEqual = 7,
        }
        #endregion

        #region | Constructor |

        /// <summary>
        /// Initializes a new instance of the MechanicalResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="ease">string holding ease</param>
        /// <param name="timing">string holding timing</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        /// <param name="enjoyment">string holding enjoyment flag</param>
        public MechanicalResult(string qualificationLevel, string ease, string timing, string type, string answers, string complete, string enjoyment) :
            base(SHCReportSection.Mechanical.ToString(), qualificationLevel, type, answers, complete)
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
        /// Initializes a new instance of the MechanicalResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public MechanicalResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Mechanical.ToString(), qualificationLevel, type, answers, complete)
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

            if (questionsAttempted <= 5)
            {
                style = 1;
            }
            else if (questionsAttempted > 5 && questionsAttempted <= 8)
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
        /// <returns>question correct band</returns>
        private int GetQuestionCorrectBand(int questionsCorrect, int totalQuestions)
        {
            int questionCorrectBand = 0;

            if (questionsCorrect > 0 && questionsCorrect <= 3)
            {
                questionCorrectBand = 1;
            }
            else if (questionsCorrect > 3 && questionsCorrect <= 6)
            {
                questionCorrectBand = 2;
            }
            else if (questionsCorrect > 6 && questionsCorrect <= totalQuestions)
            {
                questionCorrectBand = 3;
            }

            return questionCorrectBand;
        }

        /// <summary>
        /// Returns  mechanical type id 
        /// </summary>
        /// <param name="physicalPrinciplesPercent">double holding physical pricipal percentage</param>
        /// <param name="movementOfObjectsPercent">double holding move of objectpercentage</param>
        /// <param name="structureAndWeightsPercent">double holding structure and weight percentage</param>
        /// <returns>enum to construct the report section</returns>
        private string GetMechanicalTypeId(double physicalPrinciplesPercent, double movementOfObjectsPercent, double structureAndWeightsPercent)
        {
            MechanicalType result = MechanicalType.NoType;

            if (physicalPrinciplesPercent > 0.4 || movementOfObjectsPercent > 0.4 || structureAndWeightsPercent > 0.4)
            {
                //if one is higher
                if (physicalPrinciplesPercent > movementOfObjectsPercent && physicalPrinciplesPercent > structureAndWeightsPercent)
                {
                    result = MechanicalType.PPHigh; // physical principle is higher than the other two
                }
                else if (movementOfObjectsPercent > physicalPrinciplesPercent && movementOfObjectsPercent > structureAndWeightsPercent)
                {
                    result = MechanicalType.MOHigh;  // movement of object is higher than the other two
                }
                else if (structureAndWeightsPercent > physicalPrinciplesPercent && structureAndWeightsPercent > movementOfObjectsPercent)
                {
                    result = MechanicalType.SWHigh; // structure and weight is higher than the other two
                }               
                else if (movementOfObjectsPercent > structureAndWeightsPercent && physicalPrinciplesPercent == movementOfObjectsPercent)
                {
                    result = MechanicalType.PPMOHigh; // physical principle and movement of objects are equal and greater than structure and weight
                }
                else if (structureAndWeightsPercent > movementOfObjectsPercent && structureAndWeightsPercent == physicalPrinciplesPercent)
                {
                    result = MechanicalType.SWPPHigh; // structure and weight and physical principle are equal and greater than movement of objects
                }            
                else if (structureAndWeightsPercent > physicalPrinciplesPercent && structureAndWeightsPercent == movementOfObjectsPercent)
                {
                    result = MechanicalType.SWMOHigh; // structure and weight and movement of objects are equal and greater than physical principle 
                }                
                else if (structureAndWeightsPercent == physicalPrinciplesPercent && physicalPrinciplesPercent == movementOfObjectsPercent)
                {
                    result = MechanicalType.AllEqual; // all are high
                }
            }

            return ((int)result).ToString();
        }
        #endregion

        #region | Public Methods |
        /// <summary>
        /// Runs mechanical calculation
        /// </summary>
        /// <returns>Returns xml string holding the mechanical result set</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlMechanicalRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.MechanicalCorrectAnswer);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);
                int style = GetStyle(questionsAttempted);
                int questionsCorrectBand = GetQuestionCorrectBand(questionsCorrect, totalQuestions);
                double physicalPrinciplesPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.MechanicalPhysicalPrincpleQuestionToCheck), correctAnswers);
                double movementOfObjectsPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.MechanicalMovementOfObjectsQuestionToCheck), correctAnswers);
                double structureAndWeightsPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.MechanicalStructureAndWeightQuestionToCheck), correctAnswers);
                double rawscore = (double)questionsCorrect / questionsAttempted;
                int accuracy = GetAccuracy(rawscore);

                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add(Constant.XmlTimingElement, this.Timing.ToString());
                items.Add(Constant.XmlEaseElement, this.Ease.ToString());
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());
                items.Add(Constant.XmlQuestionsCorrectBandElement, questionsCorrectBand.ToString());
                items.Add(Constant.XmlStyleElement, style.ToString());
                items.Add(Constant.XmlAccuracyElement, accuracy.ToString());
                items.Add(Constant.XmlEnjoymentElement, this.Enjoyment.ToString());
                items.Add(Constant.XmlPhysicalPrinciplesPercentElement, physicalPrinciplesPercent.ToString());
                items.Add(Constant.XmlMovementOfObjectsPercentElement, movementOfObjectsPercent.ToString());
                items.Add(Constant.XmlStructureAndWeightsPercentElement, structureAndWeightsPercent.ToString());
                items.Add(Constant.XmlMechanicalTypeIdElement, GetMechanicalTypeId(physicalPrinciplesPercent, movementOfObjectsPercent, structureAndWeightsPercent));   
                return GetXML(items, Constant.XmlMechanicalRootElement);
            }
        }

        /// <summary>
        /// Runs mechanical calculation
        /// </summary>
        /// <returns>Returns xml string holding the mechanical result set</returns>
        public override string GetSummaryResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlMechanicalRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.MechanicalCorrectAnswer);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);                

                Dictionary<string, string> items = new Dictionary<string, string>();                
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());    
            
                return GetXML(items, Constant.XmlMechanicalRootElement);
            }
        }
        #endregion
    }
}
