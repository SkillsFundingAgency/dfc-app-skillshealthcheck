using System;
using System.Collections.Generic;
using System.Linq;
namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    internal class CheckingResult : SHCResultBase
    {
        public CheckingResult(string qualificationLevel, string ease, string timing, string type, string answers, string complete, string enjoyment) :
            base(SHCReportSection.Checking.ToString(), qualificationLevel, type, answers, complete)
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

        public CheckingResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Checking.ToString(), qualificationLevel, type, answers, complete)
        {            
        }

        public int Ease { get; set; }

        public int Timing { get; set; }

        public int Enjoyment { get; set; }

        public int HighScoreType { get; set; }
        
        public int LowScoreType { get; set; }

        private int GetQuestionCorrectBand(int questionsCorrect, int totalQuestions)
        {
            int questionsCorrectBand = 0;

            if (questionsCorrect > 0 && questionsCorrect <= 23)
            {
                questionsCorrectBand = 1;
            }
            else if (questionsCorrect > 23 && questionsCorrect <= 33)
            {
                questionsCorrectBand = 2;
            }
            else if (questionsCorrect > 33 && questionsCorrect <= totalQuestions)
            {
                questionsCorrectBand = 3;
            }

            return questionsCorrectBand;
        }

        private int GetRangeCorrectAnswerCount(string checkingType, string correctAnswers)
        {
            int correctAnswer = 0;
            string[] userAnswers = this.Answers.Split(Constant.AnswerSeparator);
            string[] correctAnswersSplit = correctAnswers.Split(Constant.AnswerSeparator);

            int[] correct = correctAnswersSplit.Select(x => int.Parse(x)).ToArray();
            int[] answers = userAnswers.Select(x => int.Parse(x)).ToArray();
            int[] results = new int[userAnswers.Length];

            for (int i = 0; i < answers.Length; i++)
            {
                if (answers[i].ToString() == correct[i].ToString())
                {
                    results[i] = answers[i] & correct[i];

                    if ((results[i] & Convert.ToInt16(checkingType)) == Convert.ToInt16(checkingType))
                    {
                        correctAnswer++;
                    }
                }
            }

            return correctAnswer;
        }

        private void SetCheckingCondType(double simpleNumbersPercent, double financialFiguresPercent, double abstractCodesPercent, double differentFormatsPercent)
        {
            Dictionary<string, double> values = new Dictionary<string, double>();
            values.Add("SimpleNumbers", simpleNumbersPercent);
            values.Add("FinancialFigures", financialFiguresPercent);
            values.Add("AbstractCodes", abstractCodesPercent);
            values.Add("DifferentFormats", differentFormatsPercent);
            double maxValue = values.Max(kvp => kvp.Value);
            if (maxValue > 0.7)
            {               
                int maxScoreCount = values.Where(kvp => kvp.Value == maxValue).Count();

                if (maxScoreCount == 1)
                {
                    if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers"))
                    {
                        HighScoreType = (int)CheckingCondType.Type1;     
                    }
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures"))
                    {
                        HighScoreType = (int)CheckingCondType.Type2;
                    }
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes"))
                    {
                        HighScoreType = (int)CheckingCondType.Type3;
                    }
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        HighScoreType = (int)CheckingCondType.Type4;
                    }
                }                
                else if (maxScoreCount == 2)
                {
                    if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers") &&
                        values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures"))
                    {
                        HighScoreType = (int)CheckingCondType.Type5;
                    }                    
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers") &&
                         values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes"))
                    {
                        HighScoreType = (int)CheckingCondType.Type6;
                    }                    
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers") &&
                       values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        HighScoreType = (int)CheckingCondType.Type7;
                    }                    
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures") &&
                       values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes"))
                    {
                        HighScoreType = (int)CheckingCondType.Type8;
                    }                    
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures") &&
                       values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        HighScoreType = (int)CheckingCondType.Type9;
                    }                   
                    else if (values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes") &&
                       values.Where(kvp => kvp.Value == maxValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        HighScoreType = (int)CheckingCondType.Type10;
                    }
                }               
                else if (maxScoreCount > 2)
                {
                    HighScoreType = (int)CheckingCondType.Type11;
                }

                double minValue = values.Min(kvp => kvp.Value);
                int minScoreCount = values.Where(kvp => kvp.Value == minValue).Count();

                if (minScoreCount == 1)
                {
                    if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers"))
                    {
                        LowScoreType = (int)CheckingCondType.Type1;  
                    }
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures"))
                    {
                        LowScoreType = (int)CheckingCondType.Type2;
                    }
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes"))
                    {
                        LowScoreType = (int)CheckingCondType.Type3;
                    }
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        LowScoreType = (int)CheckingCondType.Type4;
                    }
                }              
                else if (minScoreCount == 2)
                {
                    if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers") &&
                       values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures"))
                    {
                        LowScoreType = (int)CheckingCondType.Type5;
                    }                    
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers") &&
                        values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes"))
                    {
                        LowScoreType = (int)CheckingCondType.Type6;
                    }                    
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("SimpleNumbers") &&
                       values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        LowScoreType = (int)CheckingCondType.Type7;
                    }                   
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures") &&
                       values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes"))
                    {
                        LowScoreType = (int)CheckingCondType.Type8;
                    }                   
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("FinancialFigures") &&
                       values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        LowScoreType = (int)CheckingCondType.Type9;
                    }                    
                    else if (values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("AbstractCodes") &&
                       values.Where(kvp => kvp.Value == minValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ContainsKey("DifferentFormats"))
                    {
                        LowScoreType = (int)CheckingCondType.Type10;
                    }
                }                
            }
        }
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlChkRootElement);
            }
            else
            {
                string correctAnswers = this.Resource.GetString(Constant.ChkQuestionSetA);
                int questionsCorrect = this.GetNumberOfQuestionsCorrect(correctAnswers);
                int questionsAttempted = this.GetNumberOfQuestionsAttempted();
                int totalQuestions = this.GetQuestionCount(correctAnswers);
                int questionsCorrectBand = GetQuestionCorrectBand(questionsCorrect, totalQuestions);

                int numbersCnt = GetRangeCorrectAnswerCount(((int)CheckingType.SimpleNumbers).ToString(), correctAnswers);
                int financialCnt = GetRangeCorrectAnswerCount(((int)CheckingType.FinancialFigures).ToString(), correctAnswers);
                int abstractCnt = GetRangeCorrectAnswerCount(((int)CheckingType.AbstractCodes).ToString(), correctAnswers);
                int formatsCnt = GetRangeCorrectAnswerCount(((int)CheckingType.DifferentFormats).ToString(), correctAnswers);

                double simplenumbersPercent = (double)numbersCnt / 10;
                double financialPercent = (double)financialCnt / 13;
                double abstractPercent = (double)abstractCnt / 13;
                double formatsPercent = (double)formatsCnt / 9;
                SetCheckingCondType(simplenumbersPercent, financialPercent, abstractPercent, formatsPercent);

                double rawscore = (double)questionsCorrect / questionsAttempted;

                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add(Constant.XmlChkTimingElement, this.Timing.ToString());
                items.Add(Constant.XmlChkQuestionsCorrectElement, questionsCorrect.ToString());
                items.Add(Constant.XmlChkBandElement, questionsCorrectBand.ToString());
                items.Add(Constant.XmlHSTElement, HighScoreType.ToString());
                items.Add(Constant.XmlLSTElement, LowScoreType.ToString());
                items.Add(Constant.XmlChkEaseElement, this.Ease.ToString());
                items.Add(Constant.XmlChkEnjoymentElement, this.Enjoyment.ToString());

                return GetXML(items, Constant.XmlChkRootElement);
            }
        }

    }
}
