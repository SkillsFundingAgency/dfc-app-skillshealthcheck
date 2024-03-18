namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    public class MechanicalResult : SHCResultBase
    {
        public enum MechanicalType
        {
            NoType = 0,

            PPHigh = 1,

            MOHigh = 2,

            SWHigh = 3,

            PPMOHigh = 4,

            SWPPHigh = 5,

            SWMOHigh = 6,

            AllEqual = 7,
        }
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

        public MechanicalResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Mechanical.ToString(), qualificationLevel, type, answers, complete)
        {
        }
        public int Ease { get; set; }

        public int Timing { get; set; }

        public int Enjoyment { get; set; }

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

        private string GetMechanicalTypeId(double physicalPrinciplesPercent, double movementOfObjectsPercent, double structureAndWeightsPercent)
        {
            MechanicalType result = MechanicalType.NoType;

            if (physicalPrinciplesPercent > 0.4 || movementOfObjectsPercent > 0.4 || structureAndWeightsPercent > 0.4)
            {
                if (physicalPrinciplesPercent > movementOfObjectsPercent && physicalPrinciplesPercent > structureAndWeightsPercent)
                {
                    result = MechanicalType.PPHigh;         
                }
                else if (movementOfObjectsPercent > physicalPrinciplesPercent && movementOfObjectsPercent > structureAndWeightsPercent)
                {
                    result = MechanicalType.MOHigh;           
                }
                else if (structureAndWeightsPercent > physicalPrinciplesPercent && structureAndWeightsPercent > movementOfObjectsPercent)
                {
                    result = MechanicalType.SWHigh;          
                }               
                else if (movementOfObjectsPercent > structureAndWeightsPercent && physicalPrinciplesPercent == movementOfObjectsPercent)
                {
                    result = MechanicalType.PPMOHigh;               
                }
                else if (structureAndWeightsPercent > movementOfObjectsPercent && structureAndWeightsPercent == physicalPrinciplesPercent)
                {
                    result = MechanicalType.SWPPHigh;               
                }            
                else if (structureAndWeightsPercent > physicalPrinciplesPercent && structureAndWeightsPercent == movementOfObjectsPercent)
                {
                    result = MechanicalType.SWMOHigh;                
                }                
                else if (structureAndWeightsPercent == physicalPrinciplesPercent && physicalPrinciplesPercent == movementOfObjectsPercent)
                {
                    result = MechanicalType.AllEqual;    
                }
            }

            return ((int)result).ToString();
        }
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

    }
}
