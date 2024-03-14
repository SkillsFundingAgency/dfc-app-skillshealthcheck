// -----------------------------------------------------------------------
// <copyright file="SHCResultBase.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Xml;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Resources;   

    /// <summary>
    /// SHCResultBase - base class provides common functionality for different SHC assessement
    /// </summary>
    public abstract class SHCResultBase
    {
       internal const string Error_ParameterEmpty = "[{0}] - {1} paramters does not have a value specified.";
       internal const string Error_UserAnswerCorrectAnswerMismatch = "[{0}] - The number of user answers ({1}) does not match the number of correct answers ({2}) stored in system";

        /// <summary>
        /// Initializes a new instance of the SHCResultBase class
        /// </summary>
        /// <param name="qualificationLevel"></param>
        /// <param name="type"></param>
        /// <param name="answers"></param>
        /// <param name="complete"></param>
        public SHCResultBase(string reportName, string qualificationLevel, string type, string answers, string complete)
        {
            this.QualificationLevel = qualificationLevel;
            this.Type = type;
            this.Answers = answers;
            this.Complete = complete;
            this.ReportName = reportName;
        }
        /// <summary>
        /// Get / Set report name
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// Get / Set candidate qualification leve
        /// </summary>
        public string QualificationLevel { get; set; }

        /// <summary>
        /// Get / Set question set type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Get / Set answers
        /// </summary>
        public string Answers { get; set; }

        /// <summary>
        /// Get / Set complete flag
        /// </summary>
        public string Complete { get; set; }
       
        /// <summary>
        /// Returns the associated resource manager for the given qualification
        /// </summary>
        /// <param name="qualificationLevel">string holding the qualification level</param>
        /// <returns></returns>
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
                    //default to level 1                    
                    resource = Level1.ResourceManager;
                }

                return resource;
            }
        }

        /// <summary>
        /// Returns boolean flag to check question set is complete
        /// </summary>
        public bool IsComplete
        {
            get
            {
                bool retvalue = false;
                bool.TryParse(this.Complete, out retvalue);
                return retvalue;
            }
        }

        /// <summary>
        /// Implements the specific calculation for the questions set and returns the result
        /// </summary>
        /// <returns>Returns xml string</returns>
        public abstract string GetResult();

        /// <summary>
        /// Implements the specific calculation for the questions set and returns the result for Summary Report
        /// </summary>
        /// <returns>Returns xml string</returns>
        public abstract string GetSummaryResult();
         
        /// <summary>
        /// Calculates the number of correct answers
        /// </summary>
        /// <param name="userAnswers">The answers provided by the user</param>
        /// <param name="correctAnswers">Correct answers</param>
        /// <returns>Number of correct answers</returns>
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

        /// <summary>
        /// Returns number of questions attempted by the user
        /// </summary>
        /// <param name="userAnswers">A coma spearated list of answers provided by the user</param>
        /// <returns>Number of questions attempted</returns>
        /// <remarks>Not attempted questions are marked with 'X'</remarks>
        protected virtual int GetNumberOfQuestionsAttempted()
        {
            return this.Answers.Split(Constant.AnswerSeparator).Where(x => x != Constant.AnswerSkippedMarker).Count();
        }

        /// <summary>
        /// Returns the number of questions in a question set
        /// </summary>
        /// <param name="correctAnswers">A coma separated list of answers</param>
        /// <returns>Number of questions</returns>
        protected virtual int GetQuestionCount(string correctAnswers)
        {
            return correctAnswers.Split(Constant.AnswerSeparator).Length;
        }

        /// <summary>
        /// Returns correct answer count for a given set of question numbers
        /// </summary>
        /// <param name="answerPositions">string holding multiple question number index separated by comma (,)</param>
        /// <param name="correctAnswers">string holding the correct answers</param>
        /// <returns>int holding the correct answer count</returns>
        protected virtual double GetRangeCorrectAnswerPercent(string answerPositions, string correctAnswers)
        {
            int correctAnswer = 0;
            string[] userAnswers = this.Answers.Split(Constant.AnswerSeparator);
            string[] correctAnswersSplit = correctAnswers.Split(Constant.AnswerSeparator);
            string[] answerPostitionSplit = answerPositions.Split(Constant.AnswerSeparator);

            foreach (string item in answerPostitionSplit)
            {
                //reduce by - 1 as the index number starts from 0 but resource file having question numbers that starts from 1
                int idx = Convert.ToInt32(item) - 1;
                if (userAnswers[idx] == correctAnswersSplit[idx])
                {
                    correctAnswer++;
                }
            }

            return (double)correctAnswer / answerPostitionSplit.Length;
        }

        /// <summary>
        /// Build the result set in xml  eg., (<ShowNumerical>True</ShowNumerical><Numerical>....</Numerical>)
        /// </summary>
        /// <param name="items">dictionary collection holding xml element name and value</param>
        /// <param name="rootTag">string holding the root tag name</param>
        /// <returns>string holding the xml </returns>
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
