// -----------------------------------------------------------------------
// <copyright file="InterestResult.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Resources;

    ///// <summary>
    /// InterestResult- Entity to store final result for personal interest assessment
    /// </summary>
    public class InterestResult : SHCResultBase
    {
        /// <summary>
        /// Initializes a new instance of the InterestResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public InterestResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.Interests.ToString(), qualificationLevel, type, answers, complete)
        {
        }

        /// <summary>
        /// Get / Set interest categories 
        /// </summary>
        /// <remarks>This property is used in the job family to retrive the interest category result set for processing within JobSuggessionResult</remarks>
        public List<InterestCategory> InterestCategories { get; set; }
        /// <summary>
        /// Returns list of available interest categories
        /// </summary>
        /// <returns>list of interestCategories</returns>
        private List<InterestCategory> GetAvailableInterstCategories()
        {
            //setup categories
            //Not at all interested,A little interested,Moderately interested,Very interested,Extremely interested
            string[] names = this.Resource.GetString(Constant.InterestBandNames).Split(Constant.AnswerSeparator);

            //None,A little,Moderate,Very High,Extremely High
            string[] displayNames = this.Resource.GetString(Constant.InterestBandDisplayNames).Split(Constant.AnswerSeparator);

            //13,19,27,34,40
            string[] maxScores = this.Resource.GetString(Constant.InterestResultScoring).Split(Constant.AnswerSeparator);

            //Interest_StrengthOfInterestColors
            string[] colors = this.Resource.GetString(Constant.InterestStrengthOfInterestColors).Split(Constant.AnswerSeparator);

            List<InterestCategory> interestCategories = new List<InterestCategory>();
            for (int i = 0; i < names.Length; i++)
            {
                InterestCategory interestCategory = new InterestCategory
                {
                    Name = names[i],
                    DisplayName = displayNames[i],
                    MaxScore = int.Parse(maxScores[i]),
                    Color = colors[i],
                    Interests = new List<Interest>()
                };
                interestCategories.Add(interestCategory);
            }

            // sort so as to have the highest score categories at the first
            return interestCategories;
        }

        /// <summary>
        /// Calculates personal interest score
        /// </summary>
        /// <returns>list of interst category</returns>
        private List<InterestCategory> CalculatePersonalInterestScore()
        {
            List<InterestCategory> interestCategories = GetAvailableInterstCategories();    
            int catA = 0, catB = 0, catC = 0, catD = 0, catE = 0, catF = 0, catG = 0, catH = 0, catI = 0, catJ = 0, catK = 0;
            int maxA = 0, maxB = 0, maxC = 0, maxD = 0, maxE = 0, maxF = 0, maxG = 0, maxH = 0, maxI = 0, maxJ = 0, maxK = 0;

            int maxScore = int.Parse(this.Resource.GetString(Constant.InterestMaximumScore));
            string[] categories = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
            int[] categoryScore = { catA, catB, catC, catD, catE, catF, catG, catH, catI, catJ, catK };
            int[] categoryMaxScoreCount = { maxA, maxB, maxC, maxD, maxE, maxF, maxG, maxH, maxI, maxJ, maxK };

            string[] categoriesArray = this.Resource.GetString(Constant.InterestAnswerClasification).Split(Constant.AnswerSeparator);
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

            for (int i = 0; i < answers.Length; i++)
            {
                for (int j = 0; j < categories.Length; j++)
                {
                    if (categoriesArray[i] == categories[j])
                    {
                        categoryScore[j] = categoryScore[j] + answers[i];
                        if (answers[i] == maxScore)
                        {
                            categoryMaxScoreCount[j]++;
                        }
                    }
                }
            }

            // Following the specification multiply the class by 2
            categoryScore[1] = (int)categoryScore[1] * 2;

            string[] interestNames = this.Resource.GetString(Constant.InterestNames).Split(Constant.AnswerSeparator);
            string[] internalNames = this.Resource.GetString(Constant.InterestInternalNames).Split(Constant.AnswerSeparator);
            string[] families = this.Resource.GetString(Constant.InterestRelatedJobFamilies).Split(new string[] { Constant.InterestJobFamilesSeparator }, StringSplitOptions.None);
            string[] involves = this.Resource.GetString(Constant.InterestWhatThisInvolves).Split(new string[] { Constant.InterestJobFamilesSeparator }, StringSplitOptions.None);

            for (int i = 0; i < interestNames.Length; i++)
            {
                Interest interest = new Interest()
                                            {
                                                Name = interestNames[i],
                                                InternalName = internalNames[i],
                                                Category = categories[i],
                                                Score = categoryScore[i],
                                                MaxScoreCount = categoryMaxScoreCount[i],
                                                RelatedJobFamilies = families[i],
                                                WhatItInvolves = involves[i]
                                            };

                foreach (InterestCategory category in interestCategories)
                {
                    //TODO: check the logic
                    if (categoryScore[i] != 0 && categoryScore[i] <= category.MaxScore)
                    {
                        category.Interests.Add(interest);
                        break;
                    }
                }
            }

            // sorting the interest inside the category list
            foreach (InterestCategory category in interestCategories)
            {
                category.Interests.Sort(delegate(Interest interest1, Interest interest2)
                {
                    //return interest1.Score.CompareTo(interest1.Score);

                    //The comparison logic provided by the Skills Funding Agency
                    int comparisonResult = Comparer<int>.Default.Compare(interest2.Score, interest1.Score);

                    // if this can be resolved exclusivly on the basis of the value simply reorder
                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }                    
                    else
                    {
                        // If we have a tie
                        // compare the number of maximal scorses
                        int secondaryComparisonResult = Comparer<int>.Default.Compare(interest2.MaxScoreCount, interest1.MaxScoreCount);

                        // if we have a winner
                        if (secondaryComparisonResult != 0)
                        {
                            return secondaryComparisonResult;
                        }                       
                        else
                        {
                            // If not compare basing on the name of the class
                            int tetraryComparisonResult = Comparer<string>.Default.Compare(interest1.Name, interest2.Name);
                            return tetraryComparisonResult;
                        }
                    }
                });
            }

            return interestCategories.OrderByDescending(ctr => ctr.MaxScore).ToList();
        }
        /// <summary>
        /// Runs interest assessment
        /// </summary>
        /// <returns>Returns xml string holding the interest result set</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlInterestRootElement);
            }
            else
            {
                this.InterestCategories = CalculatePersonalInterestScore(); //returns categories (not at all intrested etc) with sorted interest (Caring, taking charge etc) inside
                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlInterestRootElement, this.Complete);

                        xml.WriteStartElement(Constant.XmlInterestRootElement);
                        xml.WriteElementString(Constant.XmlQualificationLevelElement, this.QualificationLevel);
                        foreach (var category in this.InterestCategories)
                        {
                            if (category.Interests.Count > 0)
                            {
                                xml.WriteStartElement(Constant.XmlInterestCategoryElement);
                                xml.WriteElementString(Constant.XmlNameElement, category.Name);
                                xml.WriteElementString(Constant.XmlDisplayNameElement, category.DisplayName);
                                xml.WriteElementString(Constant.XmlColorElement, category.Color);
                                foreach (Interest item in category.Interests)
                                {
                                    xml.WriteStartElement(Constant.XmlInterestItemElement);

                                    xml.WriteStartElement(Constant.XmlNameElement);
                                    xml.WriteCData(item.Name);
                                    xml.WriteEndElement();

                                    xml.WriteStartElement(Constant.XmlInternalNameElement);
                                    xml.WriteCData(item.InternalName);
                                    xml.WriteEndElement();

                                    xml.WriteStartElement(Constant.XmlScoreElement);
                                    xml.WriteCData(item.Score.ToString());
                                    xml.WriteEndElement();

                                    xml.WriteStartElement(Constant.XmlMaxScoreCountElement);
                                    xml.WriteCData(item.MaxScoreCount.ToString());
                                    xml.WriteEndElement();

                                    xml.WriteStartElement(Constant.XmlRelatedJobFamiliesElement);

                                    //split the job families into multiple node to force new line characters in xsl when displayed in word
                                    string[] jobFamilies = item.GetIndividualJobs();
                                    foreach (string job in jobFamilies)
                                    {
                                        xml.WriteStartElement(Constant.XmlJobName);
                                        xml.WriteCData(job);
                                        xml.WriteEndElement();
                                    }

                                    xml.WriteEndElement();
                                    xml.WriteStartElement(Constant.XmlWhatItInvolvesElement);
                                    xml.WriteCData(item.WhatItInvolves);
                                    xml.WriteEndElement();
                                    xml.WriteEndElement();
                                }

                                xml.WriteEndElement();
                            }
                        }

                        xml.WriteEndElement();
                    }

                    returnXML = sw.ToString();
                }

                return returnXML;
            }
        }

        /// <summary>
        /// Runs interest assessment
        /// </summary>
        /// <returns>Returns xml string holding the interest result set</returns>
        public override string GetSummaryResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlInterestRootElement);
            }
            else
            {
                this.InterestCategories = CalculatePersonalInterestScore();
                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlInterestRootElement, this.Complete);

                        xml.WriteStartElement(Constant.XmlInterestRootElement);
                        foreach (var category in this.InterestCategories)
                        {
                            if (category.Interests.Count > 0)
                            {
                                xml.WriteStartElement(category.Name.Replace(" ", string.Empty));
                                StringBuilder interestBuilder = new StringBuilder();
                                foreach (Interest item in category.Interests)
                                {
                                    if (interestBuilder.Length == 0)
                                    {
                                        interestBuilder.Append(item.Name);
                                    }
                                    else
                                    {
                                        interestBuilder.Append(" / ");
                                        interestBuilder.Append(item.Name);
                                    }
                                }

                                xml.WriteStartElement(Constant.XmlNameElement);
                                xml.WriteCData(interestBuilder.ToString());
                                xml.WriteEndElement();
                                xml.WriteEndElement();
                            }
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
