namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Xml;
    public abstract class SHCResultBase
    {
       internal const string Error_ParameterEmpty = "[{0}] - {1} paramters does not have a value specified.";
       internal const string Error_UserAnswerCorrectAnswerMismatch = "[{0}] - The number of user answers ({1}) does not match the number of correct answers ({2}) stored in system";

        public SHCResultBase(string reportName, string qualificationLevel, string type, string answers, string complete)
        {
            this.QualificationLevel = qualificationLevel;
            this.Type = type;
            this.Answers = answers;
            this.Complete = complete;
            this.ReportName = reportName;
        }
        public string ReportName { get; set; }

        public string QualificationLevel { get; set; }

        public string Type { get; set; }

        public string Answers { get; set; }

        public string Complete { get; set; }
       
        public ResourceManager Resource
        {
            get
            {                
                ResourceManager resource = null;
                int qualification;
                int.TryParse(this.QualificationLevel, out qualification);
                if (qualification <= 1)
                {
                    resource = Level1.ResourceManager;
                }
                
                else
                {
                    resource = Level1.ResourceManager;
                }

                return resource;
            }
        }

        public bool IsComplete
        {
            get
            {
                bool retvalue = false;
                bool.TryParse(this.Complete, out retvalue);
                return retvalue;
            }
        }

        public abstract string GetResult();

         
        protected virtual int GetNumberOfQuestionsCorrect(string correctAnswers)
        {
            int score = 0;
            if (!string.IsNullOrEmpty(this.Answers) && !string.IsNullOrEmpty(correctAnswers))
            {
                string[] correctAnswersArray = correctAnswers.Split(Constant.AnswerSeparator);
                string[] userAnswersArray = this.Answers.Split(Constant.AnswerSeparator);

                if (userAnswersArray.Length == correctAnswersArray.Length)
                {
                    for (int i = 0; i < correctAnswersArray.Length; i++)
                    {
                        if (string.Compare(correctAnswersArray[i], userAnswersArray[i]) == 0)
                        {
                            score++;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException(
                                                string.Format(Error_UserAnswerCorrectAnswerMismatch,
                                                this.ReportName, 
                                                userAnswersArray.Length, 
                                                correctAnswersArray.Length));
                }
            }
            else
            {
                throw new ArgumentException(
                                            string.Format(
                                                           Error_ParameterEmpty,
                                                           this.ReportName, 
                                                           (string.IsNullOrEmpty(this.Answers) ? "User answer" : "Correct answer stored in system")));
            }

            return score;
        }

        protected virtual int GetNumberOfQuestionsAttempted()
        {
            return this.Answers.Split(Constant.AnswerSeparator).Where(x => x != Constant.AnswerSkippedMarker).Count();
        }

        protected virtual int GetQuestionCount(string correctAnswers)
        {
            return correctAnswers.Split(Constant.AnswerSeparator).Length;
        }

        protected virtual double GetRangeCorrectAnswerPercent(string answerPositions, string correctAnswers)
        {
            int correctAnswer = 0;
            string[] userAnswers = this.Answers.Split(Constant.AnswerSeparator);
            string[] correctAnswersSplit = correctAnswers.Split(Constant.AnswerSeparator);
            string[] answerPostitionSplit = answerPositions.Split(Constant.AnswerSeparator);

            foreach (string item in answerPostitionSplit)
            {
                int idx = Convert.ToInt32(item) - 1;
                if (userAnswers[idx] == correctAnswersSplit[idx])
                {
                    correctAnswer++;
                }
            }

            return (double)correctAnswer / answerPostitionSplit.Length;
        }

        protected virtual string GetXML(Dictionary<string, string> items, string rootTag)
        {
            string returnXML = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter xml = new XmlTextWriter(sw))
                {
                    xml.WriteElementString(Constant.ShowTagPrefix + rootTag, this.IsComplete.ToString());
                    if (this.IsComplete)
                    {
                        xml.WriteStartElement(rootTag);
                        foreach (KeyValuePair<string, string> item in items)
                        {
                            xml.WriteElementString(item.Key, item.Value);
                        }
                    }
                }

                returnXML = sw.ToString();
            }

            return returnXML;
        }
   
    }
}
