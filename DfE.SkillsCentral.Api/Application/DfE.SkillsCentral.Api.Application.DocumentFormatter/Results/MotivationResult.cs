// -----------------------------------------------------------------------
// <copyright file="MotivationResult.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Resources;

    /// <summary>
    /// MechanicalResult- Entity to store final result for motivation assessment
    /// </summary>
    public class MotivationResult : SHCResultBase
    {
        #region | Constructor |

        /// <summary>
        /// Initializes a new instance of the MotivationResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public MotivationResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Motivation.ToString(), qualificationLevel, type, answers, complete)
        {
        }

        #endregion

        #region | Private Methods |
        /// <summary>
        /// Computes category score for motivation
        /// </summary>
        /// <returns>return the dictionary of category and score.</returns>
        private Dictionary<string, int> CalculateCategoryScore()
        {
            string[] categoriesArray = this.Resource.GetString(Constant.MotivationCategories).Split(Constant.AnswerSeparator);
            string[] answersArray = this.Answers.Split(Constant.AnswerSeparator);

            //answer validation
            if (categoriesArray.Length != answersArray.Length)
            {
                throw new ArgumentException(
                                            string.Format(Error_UserAnswerCorrectAnswerMismatch,
                                            this.ReportName, 
                                            answersArray.Length, 
                                            categoriesArray.Length));
            }

            int[] answers = answersArray.Select(x => int.Parse(x)).ToArray();

            Dictionary<string, int> resultByCategory = new Dictionary<string, int>();
            string[] uniqueCategories = this.Resource.GetString(Constant.MotivationUniqueCategories).Split(Constant.AnswerSeparator);

            foreach (string category in uniqueCategories)
            {
                resultByCategory.Add(category, 0);
            }

            for (int i = 0; i < answers.Length; i++)
            {
                resultByCategory[categoriesArray[i]] = resultByCategory[categoriesArray[i]] + answers[i];
            }

            return resultByCategory;
        }
        #endregion

        #region | Public Methods |
        /// <summary>
        /// Runs motivation calculation
        /// </summary>
        /// <returns>Returns xml string holding the motivation result set</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlMotivationRootElement);
            }
            else
            {
                Dictionary<string, int> resultByCategory = CalculateCategoryScore();

                var allCategories = (from item in resultByCategory
                                     select new
                                     {
                                         CategoryName = item.Key,
                                         Score = item.Value,
                                         ScaleScore = (double)item.Value / 42 * 9.0d //TODO: Check why the denominator is multiplied by 9.0
                                     }).OrderByDescending(ctr => ctr.ScaleScore).ToList();

                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlMotivationRootElement, this.Complete);

                        xml.WriteStartElement(Constant.XmlMotivationRootElement);
                        foreach (var category in allCategories)
                        {
                            xml.WriteStartElement(Constant.XmlMotivationCategoryElement);
                            xml.WriteElementString(Constant.XmlCategoryElement, category.CategoryName);
                            xml.WriteElementString(Constant.XmlScoreElement, category.Score.ToString());
                            xml.WriteElementString(Constant.XmlScaleScoreElement, category.ScaleScore.ToString());
                            xml.WriteStartElement(Constant.XmlNameElement);
                            xml.WriteCData(this.Resource.GetString(string.Format(Constant.MotivationName, category.CategoryName)));
                            xml.WriteEndElement();
                            xml.WriteStartElement(Constant.XmlDefinitionElement);
                            xml.WriteCData(this.Resource.GetString(string.Format(Constant.MotivationDefintion, category.CategoryName)));
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                        }

                        xml.WriteEndElement();
                    }

                    returnXML = sw.ToString();
                }

                return returnXML;
            }
        }

        /// <summary>
        /// Runs motivation calculation
        /// </summary>
        /// <returns>Returns xml string holding the motivation result set</returns>
        public override string GetSummaryResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlMotivationRootElement);
            }
            else
            {
                Dictionary<string, int> resultByCategory = CalculateCategoryScore();

                var allCategories = (from item in resultByCategory
                                     select new
                                     {
                                         CategoryName = item.Key,
                                         Score = item.Value,
                                         ScaleScore = (double)item.Value / 42 * 9.0d //TODO: Check why the denominator is multiplied by 9.0
                                     }).OrderByDescending(ctr => ctr.ScaleScore).ToList();

                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlMotivationRootElement, this.Complete);

                        xml.WriteStartElement(Constant.XmlMotivationRootElement);
                        int i;
                        for (i = 0; i < allCategories.Count; i++)
                        {
                            xml.WriteStartElement(Constant.XmlMotivationCategoryElement + (i + 1).ToString());
                            xml.WriteCData(this.Resource.GetString(string.Format(Constant.MotivationName, allCategories[i].CategoryName)));
                            xml.WriteEndElement();
                        }

                        xml.WriteEndElement();
                    }

                    returnXML = sw.ToString();
                }

                return returnXML;
            }
        }

        #endregion
    }
}
