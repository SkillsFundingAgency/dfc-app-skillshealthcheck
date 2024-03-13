
using System;
using System.Collections.Generic;
//using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    /// <summary>
    ///  AbstractResult- Entity to calculate & store final result for solving abstract problems
    /// </summary>
    public class AbstractResult : SHCResultBase 
    {
      
        public AbstractResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Abstract.ToString(), qualificationLevel, type, answers, complete)
        {            
        }

      
        public AbstractResult(string qualificationLevel, string ease, string timing, string type, string answers, string complete, string enjoyment) :
            base(SHCReportSection.Abstract.ToString(), qualificationLevel, type, answers, complete)
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
        /// Defines abstract type
        /// </summary>
        public enum AbstractType
        {
            /// <summary>
            /// default value
            /// </summary>
            NoType = 0,

            /// <summary>
            /// Reflection Percent, Rotation Percent, Movement Percent and Repetition Percent are all over 0.6 or over 
            /// </summary>
            AllHigh = 1,

            /// <summary>
            /// Rotation is higher than reflection, movement, repetition
            /// </summary>
            RotationHigh = 2,

            /// <summary>
            /// Refletion is higher than rotation, movement and repetition
            /// </summary>
            ReflectionHigh = 3,

            /// <summary>
            /// Movement is higher than rotation, reflection and repetition
            /// </summary>
            MovementHigh = 4,

            /// <summary>
            /// Repetition is higher than rotation, reflection and movement
            /// </summary>
            RepetitionHigh = 5,

            /// <summary>
            /// Rotation and movement higher
            /// </summary>
            RotationMovementHigh = 6,

            /// <summary>
            /// Movement and repetition higer
            /// </summary>
            MovementRepetitionHigh = 7,

            /// <summary>
            /// Movement and reflection higher
            /// </summary>
            MovementReflectionHigh = 8,

            /// <summary>
            /// Repetition and reflection higher
            /// </summary>
            RepetitionRefletionHigh = 9,

            /// <summary>
            /// Reflection and rotation higer
            /// </summary>
            ReflectionRotationHigh = 10,

            /// <summary>
            /// Repetition and rotation higher
            /// </summary>
            RepetitionRotationHigh = 11,

            /// <summary>
            /// Movement, repetition and rotation higher
            /// </summary>
            MovementRepetitionRotationHigh = 12,

            /// <summary>
            /// Movement, repetition and reflection higher
            /// </summary>
            MovementRepetitionReflectionHigh = 13,

            /// <summary>
            /// Movement, reflection and rotation higher
            /// </summary>
            MovementRefletionRotationHigh = 14,

            /// <summary>
            /// Rotation, repetition and reflection higher
            /// </summary>
            RotationRepetitionReflectionHigh = 15
        }
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
        /// <returns>return the band</returns>
        private int GetQuestionCorrectBand(int questionsCorrect, int totalQuestions)
        {
            int questionCorrectBand = 0;

            if (questionsCorrect > 0 && questionsCorrect <= 5)
            {
                questionCorrectBand = 1;
            }
            else if (questionsCorrect > 5 && questionsCorrect <= 10)
            {
                questionCorrectBand = 2;
            }
            else if (questionsCorrect > 10 && questionsCorrect <= totalQuestions)
            {
                questionCorrectBand = 3;
            }

            return questionCorrectBand;
        }

        /// <summary>
        /// Returns  abstract type id 
        /// </summary>
        /// <param name="reflection">double holding the reflection percentage</param>
        /// <param name="rotation">double holding the rotation percentage</param>
        /// <param name="movement">double holding the movement percentage</param>
        /// <param name="repetition">double holding the repetition percentage</param>
        /// <returns>return the type of enum to be used to construct report section</returns>
        private string GetAbstractTypeID(double reflection, double rotation, double movement, double repetition)
        {
            AbstractType result = AbstractType.NoType;

            // All are highest
            if (reflection > 0.6 && rotation > 0.6 && movement > 0.6 && repetition > 0.6)
            {
                result = AbstractType.AllHigh;
            }
            else
            {
                //if one is higher
                if (rotation > reflection && rotation > movement && rotation > repetition)
                {
                    result = AbstractType.RotationHigh; // rotation is higher
                }
                else if (reflection > rotation && reflection > movement && reflection > repetition)
                {
                    result = AbstractType.ReflectionHigh; // reflection is higher
                }
                else if (movement > rotation && movement > reflection && movement > repetition)
                {
                    result = AbstractType.MovementHigh; // movement is higher
                }
                else if (repetition > rotation && repetition > movement && repetition > reflection)
                {
                    result = AbstractType.RepetitionHigh; //repetition is higer
                }                
                else if (movement > reflection && movement > repetition && movement == rotation)
                {
                    result = AbstractType.RotationMovementHigh; //rotation & movement  higher
                }
                else if (movement > reflection && movement > rotation && movement == repetition)
                {
                    result = AbstractType.MovementRepetitionHigh; //movment and repetition higher
                }
                else if (movement > rotation && movement > repetition && movement == reflection)
                {
                    result = AbstractType.MovementReflectionHigh; // movment and reflection higher
                }
                else if (repetition > movement && repetition > rotation && repetition == reflection)
                {
                    result = AbstractType.RepetitionRefletionHigh; // repetition and reflection higer
                }
                else if (reflection > movement && reflection > repetition && reflection == rotation)
                {
                    result = AbstractType.ReflectionRotationHigh; // reflection and rotation higer
                }
                else if (repetition > movement && repetition > reflection && repetition == rotation)
                {
                    result = AbstractType.RepetitionRotationHigh;  // repetition and rotation higher
                }                
                else if (movement > reflection && movement == repetition && movement == rotation)
                {
                    result = AbstractType.MovementRepetitionRotationHigh; // movment , repetition and rotation is higher
                }
                else if (movement > rotation && movement == repetition && movement == reflection)
                {
                    result = AbstractType.MovementRepetitionReflectionHigh; // movement, repetition and reflection is higher
                }
                else if (movement > repetition && movement == reflection && movement == rotation)
                {
                    result = AbstractType.MovementRefletionRotationHigh; // movement, relfletion and rotation is higher
                }
                else if (rotation > movement && rotation == repetition && rotation == reflection)
                {
                    result = AbstractType.RotationRepetitionReflectionHigh; // rotation, repetition and refletion is higher
                }
            }

            return ((int)result).ToString();
        }
        /// <summary>
        /// Runs abstract calculation
        /// </summary>
        /// <returns>Returns xml string holding the abstract result set</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlAbstractRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.AbstractCorrectAnswer);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);
                int style = GetStyle(questionsAttempted);
                int questionsCorrectBand = GetQuestionCorrectBand(questionsCorrect, totalQuestions);
                double rawscore = (double)questionsCorrect / questionsAttempted;
                int accuracy = GetAccuracy(rawscore);
                double reflectionPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.AbstractReflectionQuestions), correctAnswers);
                double rotationPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.AbstractRotationQuestions), correctAnswers);
                double movementPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.AbstractMovementQuestions), correctAnswers);
                double repetitionPercent = this.GetRangeCorrectAnswerPercent(this.Resource.GetString(Constant.AbstractRepetitionQuestions), correctAnswers);
                string abstractType = GetAbstractTypeID(reflectionPercent, rotationPercent, movementPercent, repetitionPercent);
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
                items.Add(Constant.XmlRelectionPercentElement, reflectionPercent.ToString());
                items.Add(Constant.XmlRotationPercentElement, rotationPercent.ToString());
                items.Add(Constant.XmlMovementPercentElement, movementPercent.ToString());
                items.Add(Constant.XmlRepetitionPercentElement, repetitionPercent.ToString());
                items.Add(Constant.XmlAbstractTypeIdElement, abstractType);  
                return GetXML(items, Constant.XmlAbstractRootElement);
            }
        }

        /// <summary>
        /// Runs abstract calculation
        /// </summary>
        /// <returns>Returns xml string holding the abstract result set</returns>
        public override string GetSummaryResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlAbstractRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.AbstractCorrectAnswer);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);
                
                Dictionary<string, string> items = new Dictionary<string, string>();                
                items.Add(Constant.XmlQuestionsAttemptedElement, questionsAttempted.ToString());
                items.Add(Constant.XmlTotalQuestionsElement, totalQuestions.ToString());
                items.Add(Constant.XmlQuestionsCorrectElement, questionsCorrect.ToString());
                
                return GetXML(items, Constant.XmlAbstractRootElement);
            }
        }
    }
}
