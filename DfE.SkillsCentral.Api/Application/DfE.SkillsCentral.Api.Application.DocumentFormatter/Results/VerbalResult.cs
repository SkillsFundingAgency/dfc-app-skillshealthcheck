﻿using System;
using System.Collections.Generic;
namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    public class VerbalResult : SHCResultBase
    {
        public VerbalResult(string qualificationLevel, string ease, string timing, string type, string answers, string complete) :
            base(SHCReportSection.Verbal.ToString(), qualificationLevel, type, answers, complete)
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

        public VerbalResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Verbal.ToString(), qualificationLevel, type, answers, complete)
        {
        }
        public int Ease { get; set; }

        public int Timing { get; set; }

        private int GetStyle(int questionsAttempted)
        {
            int style = 1;
            if (questionsAttempted <= 14)
            {
                style = 1;
            }
            else if (questionsAttempted > 14 && questionsAttempted <= 18)
            {
                style = 2;
            }
            else if (questionsAttempted > 18)
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

        private int GetOverallPotential(int questionsCorrect)
        {
            int overallPotential = 1;
            if (questionsCorrect <= 7)
            {
                overallPotential = 1;
            }
            else if (questionsCorrect > 7 && questionsCorrect <= 13)
            {
                overallPotential = 2;
            }
            else if (questionsCorrect > 13)
            {
                overallPotential = 3;
            }

            return overallPotential;
        }

        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return this.GetXML(null, Constant.XmlVerbalRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.VerbalCorrectAnswers);

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

                return this.GetXML(items, Constant.XmlVerbalRootElement);
            }
        }


    }
}
