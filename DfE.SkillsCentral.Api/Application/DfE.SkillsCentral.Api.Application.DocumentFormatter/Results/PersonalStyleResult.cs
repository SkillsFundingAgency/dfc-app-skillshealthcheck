namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    public class PersonalStyleResult : SHCResultBase
    {
        public PersonalStyleResult(string qualificationLevel, string type, string answers, string complete) :
            base(SHCReportSection.PersonalStyle.ToString(), qualificationLevel, type, answers, complete)
        {
        }

        private Dictionary<string, int> CalculateCategoryScore()
        {
            string[] categoriesArray = this.Resource.GetString(Constant.PersonalCategories).Split(Constant.AnswerSeparator);
            string[] answersArray = this.Answers.Split(Constant.AnswerSeparator);
            int[] answers = answersArray.Select(x => int.Parse(x)).ToArray();

            if (categoriesArray.Length != answersArray.Length)
            {
                throw new ArgumentException(
                                            string.Format(Error_UserAnswerCorrectAnswerMismatch,
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

        private List<PersonalStyle> BuildList()
        {
            Dictionary<string, int> calculatedScores = CalculateCategoryScore();
            List<PersonalStyle> pesrsonalStyles = new List<PersonalStyle>();
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

                    if (ps.Category == "D1")
                    {
                        score = TransformScore(score);
                    }

                    ps.Score = score;
                    pesrsonalStyles.Add(ps);

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

        private int TransformScore(int score)
        {
            string[] relaxedTenseScore = this.Resource.GetString(Constant.PersonalRelaxedTenseScore).Split(Constant.AnswerSeparator);
            string[] transformedRelaxedTenseScore = this.Resource.GetString(Constant.PersonalTransformedRelaxedTenseScore).Split(Constant.AnswerSeparator);

            int index = Array.IndexOf(relaxedTenseScore, score.ToString());
            score = Convert.ToInt32(transformedRelaxedTenseScore[index]);

            return score;
        }

        private void SortPersonalStyleCareers(List<PersonalStyle> pesrsonalStyles, bool sortDescending)
        {
            if (sortDescending)
            {
                pesrsonalStyles.Sort(delegate(PersonalStyle ps1, PersonalStyle ps2)
                {
                    int comparisonResult = Comparer<int>.Default.Compare(ps2.Score, ps1.Score);

                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }                    
                    else
                    {
                        int tetraryComparisonResult = Comparer<string>.Default.Compare(ps1.RightName, ps2.RightName);
                        return tetraryComparisonResult;
                    }
                });
            }
            else
            {
                pesrsonalStyles.Sort(delegate(PersonalStyle ps1, PersonalStyle ps2)
                {
                    int comparisonResult = Comparer<int>.Default.Compare(ps1.Score, ps2.Score);

                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }                   
                    else
                    {
                        int tetraryComparisonResult = Comparer<string>.Default.Compare(ps1.RightName, ps2.RightName);
                        return tetraryComparisonResult;
                    }
                });
            }
        }
    }
}
