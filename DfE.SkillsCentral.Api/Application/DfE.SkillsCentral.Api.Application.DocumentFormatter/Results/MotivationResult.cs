namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    public class MotivationResult : SHCResultBase
    {
        public MotivationResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Motivation.ToString(), qualificationLevel, type, answers, complete)
        {
        }

        private Dictionary<string, int> CalculateCategoryScore()
        {
            string[] categoriesArray = this.Resource.GetString(Constant.MotivationCategories).Split(Constant.AnswerSeparator);
            string[] answersArray = this.Answers.Split(Constant.AnswerSeparator);

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
                                         ScaleScore = (double)item.Value / 42 * 9.0d         
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


    }
}
