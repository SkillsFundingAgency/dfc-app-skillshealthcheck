// -----------------------------------------------------------------------
// <copyright file="PersonalStyleResult.cs" company="tesl.com">
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
    using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;
    using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Resources;

    /// <summary>
    /// PersonalStyle - Entity to calucate and store the personal style.
    /// </summary>
    public class PersonalStyleResult : SHCResultBase
    {
        #region | Constructor |

        /// <summary>
        /// Initializes a new instance of the PersonalStyleResult class
        /// </summary>
        /// <param name="qualificationLevel">stirng holding qualification level</param>
        /// <param name="type">string holding type</param>
        /// <param name="answers">string holding answers</param>
        /// <param name="complete">string holding complete flag</param>
        public PersonalStyleResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.PersonalStyle.ToString(), qualificationLevel, type, answers, complete)
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
            string[] categoriesArray = this.Resource.GetString(Constant.PersonalCategories).Split(Constant.AnswerSeparator);
            string[] answersArray = this.Answers.Split(Constant.AnswerSeparator);
            int[] answers = answersArray.Select(x => int.Parse(x)).ToArray();

            //answer validation
            if (categoriesArray.Length != answersArray.Length)
            {
                throw new ArgumentException(
                                            string.Format(General.Error_UserAnswerCorrectAnswerMismatch,
                                            this.ReportName,
                                            answersArray.Length, 
                                            categoriesArray.Length));
            }

            Dictionary<string, int> resultByCategory = new Dictionary<string, int>();
            string[] uniqueCategories = this.Resource.GetString(Constant.PersonalUniqueCategories).Split(Constant.AnswerSeparator);

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
        /// Runs personal style assement and returns result set for summary report
        /// </summary>
        /// <returns>return the xml string with all the value to generate report</returns>
        public override string GetSummaryResult()
        {
            //return the normal report output for summary result
            return this.GetResult();
        }

        /// <summary>
        /// Runs personal stryle assesment and returns result set for SHC full report
        /// </summary>
        /// <returns>return the xml string with all the value to generate report</returns>
        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlPersonalRootElement);
            }
            else
            {
                List<PersonalStyle> allPersonalStyles = BuildList();
                int i = Convert.ToInt32(this.Resource.GetString(Constant.PersonalNoOfStrengthsToDisplay));
                SortPersonalStyleCareers(allPersonalStyles, true);

                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlPersonalRootElement, this.Complete);
                        xml.WriteStartElement(Constant.XmlPersonalRootElement);

                        xml.WriteStartElement(Constant.XmlPersonalListOfStrengthElement);
                        foreach (var category in allPersonalStyles)
                        {
                            xml.WriteStartElement(Constant.XmlPersonalStrengthElement);
                            string strength = string.Empty;
                            if (category.Score < 15)
                            {
                                strength = category.StrengthLeft;
                            }
                            else if (category.Score == 15)
                            {
                                strength = category.StrengthMid;
                            }
                            else if (category.Score > 15)
                            {
                                strength = category.StrengthRight;
                            }

                            xml.WriteStartElement(Constant.XmlPersonalItem);
                            xml.WriteCData(strength);
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                            i--;
                            if (i == 0)
                            {
                                break;
                            }
                        }

                        xml.WriteEndElement();

                        SortPersonalStyleCareers(allPersonalStyles, false);
                        i = Convert.ToInt32(this.Resource.GetString(Constant.PersonalNoOfChallengesToDisplay));
                        xml.WriteStartElement(Constant.XmlPersonalListOfChallengeElement);
                        foreach (var category in allPersonalStyles)
                        {
                            if (category.Category.Contains("A2_"))
                            {
                                category.Score = TransformScore(category.Score);
                            }

                            xml.WriteStartElement(Constant.XmlPersonalChallengeElement);
                            string challenge = string.Empty;
                            string tip = string.Empty;
                            if (category.Score < 15)
                            {
                                if (!string.IsNullOrEmpty(category.ChallengeLeft) && !string.IsNullOrEmpty(category.DevelopmentLeft))
                                {
                                    challenge = category.ChallengeLeft;
                                    tip = category.DevelopmentLeft;
                                }
                            }
                            else if (category.Score == 15)
                            {
                                challenge = category.ChallengeMid;
                            }
                            else if (category.Score > 15)
                            {
                                if (!string.IsNullOrEmpty(category.ChallengeRight) && !string.IsNullOrEmpty(category.DevelopmentRight))
                                {
                                    challenge = category.ChallengeRight;
                                    tip = category.DevelopmentRight;
                                }
                            }

                            xml.WriteStartElement(Constant.XmlPersonalItem);
                            xml.WriteCData(challenge);
                            xml.WriteEndElement();
                            xml.WriteStartElement(Constant.XmlPersonalTip);
                            xml.WriteCData(tip);
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                            i--;
                            if (i == 0)
                            {
                                break;
                            }
                        }

                        xml.WriteEndElement();
                        xml.WriteEndElement();
                    }

                    returnXML = sw.ToString();
                }

                return returnXML;
            }
        }

        #endregion

        #region | Private Methods |

        /// <summary>
        /// Build personal style items
        /// </summary>
        /// <returns>list of personal style</returns>
        private List<PersonalStyle> BuildList()
        {
            Dictionary<string, int> calculatedScores = CalculateCategoryScore();
            List<PersonalStyle> pesrsonalStyles = new List<Entity.PersonalStyle>();
            string[] categories = this.Resource.GetString(Constant.PersonalUniqueCategories).Split(Constant.AnswerSeparator);
            string[] rightNames = this.Resource.GetString(Constant.PersonalRightNames).Split(Constant.AnswerSeparator);

            string[] sepeartor = new string[] { ";#" };
            string[] strengthLefts = this.Resource.GetString(Constant.PersonalStrengthLeft).Split(sepeartor, StringSplitOptions.None);
            string[] strengthMids = this.Resource.GetString(Constant.PersonalStrengthMid).Split(sepeartor, StringSplitOptions.None);
            string[] strengthRights = this.Resource.GetString(Constant.PersonalStrengthRight).Split(sepeartor, StringSplitOptions.None);

            string[] challengeLefts = this.Resource.GetString(Constant.PersonalChallengeLeft).Split(sepeartor, StringSplitOptions.None);
            string[] challengeMids = this.Resource.GetString(Constant.PersonalChallengeMid).Split(sepeartor, StringSplitOptions.None);
            string[] challengeRights = this.Resource.GetString(Constant.PersonalChallengeRight).Split(sepeartor, StringSplitOptions.None);

            string[] developmentLefts = this.Resource.GetString(Constant.PersonalDevelopmentLeft).Split(sepeartor, StringSplitOptions.None);
            string[] developmentRights = this.Resource.GetString(Constant.PersonalDevelopmentRight).Split(sepeartor, StringSplitOptions.None);

            for (int i = 0; i < categories.Length; i++)
            {
                // Task Focused - Caring  (A3) and Non Practical-Practical (B1) are not included at all, as are less directly relevant in a job search context
                if (categories[i] != "A3" && categories[i] != "B1")
                {
                    PersonalStyle ps = new PersonalStyle();
                    ps.Category = categories[i];
                    ps.RightName = rightNames[i];
                    ps.StrengthLeft = strengthLefts[i];
                    ps.StrengthMid = strengthMids[i];
                    ps.StrengthRight = strengthRights[i];
                    ps.ChallengeLeft = challengeLefts[i];
                    ps.ChallengeMid = challengeMids[i];
                    ps.ChallengeRight = challengeRights[i];
                    ps.DevelopmentLeft = developmentLefts[i];
                    ps.DevelopmentRight = developmentRights[i];
                    int score = (int)calculatedScores[categories[i]];

                    // Transform the score on Relaxed-Tense
                    if (ps.Category == "D1")
                    {
                        score = TransformScore(score);
                    }

                    ps.Score = score;
                    pesrsonalStyles.Add(ps);

                    // Reserved-Sociable category.  Here a “Right” preference is both a strength and a challenge. A “Left” Preference is a challenge.
                    if (ps.Category == "A2")
                    {
                        PersonalStyle reversedPS = new PersonalStyle();
                        reversedPS.Category = categories[i] + "_2";
                        reversedPS.RightName = rightNames[i];
                        reversedPS.StrengthLeft = strengthLefts[i];
                        reversedPS.StrengthMid = strengthMids[i];
                        reversedPS.StrengthRight = strengthRights[i];
                        reversedPS.ChallengeLeft = challengeLefts[i];
                        reversedPS.ChallengeMid = challengeMids[i];
                        reversedPS.ChallengeRight = challengeRights[i];
                        reversedPS.DevelopmentLeft = developmentLefts[i];
                        reversedPS.DevelopmentRight = developmentRights[i];
                        score = TransformScore(score);
                        reversedPS.Score = score;
                        pesrsonalStyles.Add(reversedPS);
                    }
                }
            }

            return pesrsonalStyles;
        }

        /// <summary>
        /// Transforms the score according to personal style specification
        /// </summary>
        /// <param name="score">int holding the score</param>
        /// <returns>returns transformed score as integer</returns>
        private int TransformScore(int score)
        {
            string[] relaxedTenseScore = this.Resource.GetString(Constant.PersonalRelaxedTenseScore).Split(Constant.AnswerSeparator);
            string[] transformedRelaxedTenseScore = this.Resource.GetString(Constant.PersonalTransformedRelaxedTenseScore).Split(Constant.AnswerSeparator);

            int index = Array.IndexOf(relaxedTenseScore, score.ToString());
            score = Convert.ToInt32(transformedRelaxedTenseScore[index]);

            return score;
        }

        /// <summary>
        /// Orders personal style itmes 
        /// </summary>
        /// <param name="pesrsonalStyles"></param>
        /// <param name="sortDescending"></param>
        private void SortPersonalStyleCareers(List<PersonalStyle> pesrsonalStyles, bool sortDescending)
        {
            if (sortDescending)
            {
                // sort so as to have the highest score at top
                pesrsonalStyles.Sort(delegate(PersonalStyle ps1, PersonalStyle ps2)
                {
                    //The comparison logic provided by the Skills Funding Agency
                    int comparisonResult = Comparer<int>.Default.Compare(ps2.Score, ps1.Score);

                    // if this can be resolved exclusivly on the basis of the value simply reorder
                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }                    
                    else
                    {
                        // If we have a tie
                        int tetraryComparisonResult = Comparer<string>.Default.Compare(ps1.RightName, ps2.RightName);
                        return tetraryComparisonResult;
                    }
                });
            }
            else
            {
                // sort so as to have the lowest score at top
                pesrsonalStyles.Sort(delegate(PersonalStyle ps1, PersonalStyle ps2)
                {
                    //The comparison logic provided by the Skills Funding Agency
                    int comparisonResult = Comparer<int>.Default.Compare(ps1.Score, ps2.Score);

                    // if this can be resolved exclusivly on the basis of the value simply reorder
                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }                   
                    else
                    {
                        // If we have a tie
                        int tetraryComparisonResult = Comparer<string>.Default.Compare(ps1.RightName, ps2.RightName);
                        return tetraryComparisonResult;
                    }
                });
            }
        }
        #endregion
    }
}
