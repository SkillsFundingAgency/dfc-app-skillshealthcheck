
using System;
using System.Collections.Generic;
namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
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

        public enum AbstractType
        {
            NoType = 0,

            AllHigh = 1,

            RotationHigh = 2,

            ReflectionHigh = 3,

            MovementHigh = 4,

            RepetitionHigh = 5,

            RotationMovementHigh = 6,

            MovementRepetitionHigh = 7,

            MovementReflectionHigh = 8,

            RepetitionRefletionHigh = 9,

            ReflectionRotationHigh = 10,

            RepetitionRotationHigh = 11,

            MovementRepetitionRotationHigh = 12,

            MovementRepetitionReflectionHigh = 13,

            MovementRefletionRotationHigh = 14,

            RotationRepetitionReflectionHigh = 15
        }
        public int Ease { get; set; }

        public int Timing { get; set; }

        public int Enjoyment { get; set; }

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

        private string GetAbstractTypeID(double reflection, double rotation, double movement, double repetition)
        {
            AbstractType result = AbstractType.NoType;

            if (reflection > 0.6 && rotation > 0.6 && movement > 0.6 && repetition > 0.6)
            {
                result = AbstractType.AllHigh;
            }
            else
            {
                if (rotation > reflection && rotation > movement && rotation > repetition)
                {
                    result = AbstractType.RotationHigh;    
                }
                else if (reflection > rotation && reflection > movement && reflection > repetition)
                {
                    result = AbstractType.ReflectionHigh;    
                }
                else if (movement > rotation && movement > reflection && movement > repetition)
                {
                    result = AbstractType.MovementHigh;    
                }
                else if (repetition > rotation && repetition > movement && repetition > reflection)
                {
                    result = AbstractType.RepetitionHigh;   
                }                
                else if (movement > reflection && movement > repetition && movement == rotation)
                {
                    result = AbstractType.RotationMovementHigh;     
                }
                else if (movement > reflection && movement > rotation && movement == repetition)
                {
                    result = AbstractType.MovementRepetitionHigh;    
                }
                else if (movement > rotation && movement > repetition && movement == reflection)
                {
                    result = AbstractType.MovementReflectionHigh;     
                }
                else if (repetition > movement && repetition > rotation && repetition == reflection)
                {
                    result = AbstractType.RepetitionRefletionHigh;     
                }
                else if (reflection > movement && reflection > repetition && reflection == rotation)
                {
                    result = AbstractType.ReflectionRotationHigh;     
                }
                else if (repetition > movement && repetition > reflection && repetition == rotation)
                {
                    result = AbstractType.RepetitionRotationHigh;      
                }                
                else if (movement > reflection && movement == repetition && movement == rotation)
                {
                    result = AbstractType.MovementRepetitionRotationHigh;        
                }
                else if (movement > rotation && movement == repetition && movement == reflection)
                {
                    result = AbstractType.MovementRepetitionReflectionHigh;       
                }
                else if (movement > repetition && movement == reflection && movement == rotation)
                {
                    result = AbstractType.MovementRefletionRotationHigh;       
                }
                else if (rotation > movement && rotation == repetition && rotation == reflection)
                {
                    result = AbstractType.RotationRepetitionReflectionHigh;       
                }
            }

            return ((int)result).ToString();
        }
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

    }
}
