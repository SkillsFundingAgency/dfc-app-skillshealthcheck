﻿namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    public class SkillAreasResult : SHCResultBase
    {       
        public enum Skills
        {
            CT1 = 1,

            CT2,

            CT3,

            CT4,

            CT5,

            CT6,

            CT7,

            CT8,

            CT9
        }

        public SkillAreasResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.SkillAreas.ToString(), qualificationLevel, type, answers, complete)
        {           
        }

        public List<SkillsCategory> RankedSkillCategories { get; set; } 
        private int AnswerToScore(int answer)
        {
            switch (answer)
            {
                case 1:
                    return 2;
                case 2:
                    return 1;
                case 3:
                    return 0;
                default:
                    return 0;
            }
        }
       
        private List<SkillsCategory> GetRankedSkillsCategory()
        {
            int scoreCT1 = 0, scoreCT2 = 0, scoreCT3 = 0, scoreCT4 = 0, scoreCT5 = 0, scoreCT6 = 0, scoreCT7 = 0, scoreCT8 = 0, scoreCT9 = 0;

            int rank1CT1 = 0;
            int rank1CT2 = 0;
            int rank1CT3 = 0;
            int rank1CT4 = 0;
            int rank1CT5 = 0;
            int rank1CT6 = 0;
            int rank1CT7 = 0;
            int rank1CT8 = 0;
            int rank1CT9 = 0;

            string[] categoriesArray = this.Resource.GetString(Constant.SkillsAreaCategories).Split(Constant.AnswerSeparator);
            string[] answersArray = this.Answers.Split(Constant.AnswerSeparator);
            int[] answers = answersArray.Select(x => int.Parse(x)).ToArray();

            if (categoriesArray.Length != answersArray.Length)
            {
                throw new ArgumentException(string.Format(Error_UserAnswerCorrectAnswerMismatch,
                                                this.ReportName, answersArray.Length, categoriesArray.Length));
            }

            for (int i = 0; i < answers.Length; i++)
            {
                answers[i] += 1;

                if (categoriesArray[i] == Skills.CT1.ToString())
                {
                    scoreCT1 = scoreCT1 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT1++;
                    }
                }

                if (categoriesArray[i] == Skills.CT2.ToString())
                {
                    scoreCT2 = scoreCT2 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT2++;
                    }
                }

                if (categoriesArray[i] == Skills.CT3.ToString())
                {
                    scoreCT3 = scoreCT3 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT3++;
                    }
                }

                if (categoriesArray[i] == Skills.CT4.ToString())
                {
                    scoreCT4 = scoreCT4 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT4++;
                    }
                }

                if (categoriesArray[i] == Skills.CT5.ToString())
                {
                    scoreCT5 = scoreCT5 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT5++;
                    }
                }

                if (categoriesArray[i] == Skills.CT6.ToString())
                {
                    scoreCT6 = scoreCT6 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT6++;
                    }
                }

                if (categoriesArray[i] == Skills.CT7.ToString())
                {
                    scoreCT7 = scoreCT7 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT7++;
                    }
                }

                if (categoriesArray[i] == Skills.CT8.ToString())
                {
                    scoreCT8 = scoreCT8 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT8++;
                    }
                }

                if (categoriesArray[i] == Skills.CT9.ToString())
                {
                    scoreCT9 = scoreCT9 + AnswerToScore(answers[i]);
                    if (answers[i] == 1)
                    {
                        rank1CT9++;
                    }
                }
            }

            List<SkillsCategory> items = new List<SkillsCategory>();
            items.Add(new SkillsCategory(Skills.CT1.ToString(), scoreCT1 / 18.0d, rank1CT1 / 9.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT2.ToString(), scoreCT2 / 22.0d, rank1CT2 / 11.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT3.ToString(), scoreCT3 / 20.0d, rank1CT3 / 10.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT4.ToString(), scoreCT4 / 16.0d, rank1CT4 / 8.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT5.ToString(), scoreCT5 / 18.0d, rank1CT5 / 9.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT6.ToString(), scoreCT6 / 20.0d, rank1CT6 / 10.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT7.ToString(), scoreCT7 / 18.0d, rank1CT7 / 9.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT8.ToString(), scoreCT8 / 20.0d, rank1CT8 / 10.0d, this.Resource));
            items.Add(new SkillsCategory(Skills.CT9.ToString(), scoreCT9 / 16.0d, rank1CT9 / 8.0d, this.Resource));

            return items.OrderByDescending(o1 => o1.Score).ThenByDescending(o2 => o2.RankOneResponses).ToList(); 
        }

        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlSkillsRootElement);
            }
            else
            {
                this.RankedSkillCategories  = GetRankedSkillsCategory();
                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlSkillsRootElement, this.Complete);
                        
                        xml.WriteStartElement(Constant.XmlSkillsRootElement);
                        xml.WriteElementString(Constant.XmlQualificationLevelElement, this.QualificationLevel);
                        foreach (var item in this.RankedSkillCategories)
                        {
                            xml.WriteStartElement(Constant.XmlSkillsCategoryElement);

                            xml.WriteStartElement(Constant.XmlNameElement);
                            xml.WriteCData(item.Name);
                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlTitleElement);
                            xml.WriteCData(item.Title);
                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlDescriptionElement);
                            xml.WriteCData(item.Description);
                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlDefinitionElement);
                            xml.WriteCData(item.Definition);
                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlDevTip1Element);
                            xml.WriteCData(item.DevTip1);
                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlDevTip2Element);
                            xml.WriteCData(item.DevTip2);
                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlStengthElement);
                            xml.WriteCData(item.Strength);
                            xml.WriteEndElement();

                            xml.WriteElementString(Constant.XmlRankOnResponseElement, item.RankOneResponses.ToString());
                            xml.WriteElementString(Constant.XmlScoreElement, item.Score.ToString());
                            xml.WriteElementString(Constant.XmlScoreScaleElement, item.ScoreScale.ToString());                            
                            xml.WriteEndElement();
                        }

                        xml.WriteEndElement();
                        returnXML = sw.ToString();
                    }

                    return returnXML;
                }
            }
        }        

    }
}
