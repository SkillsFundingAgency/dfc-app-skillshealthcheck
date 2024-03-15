// -----------------------------------------------------------------------
// <copyright file="SkillsCategory.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Resources;
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;

    /// <summary>
    ///  SkillsCategory - Entity to store personal interest category information
    /// </summary>
    public class SkillsCategory
    {
        #region | Enum |
        /// <summary>
        /// Defines skills category scale 
        /// </summary>
        public enum Scale
        {
            /// <summary>
            /// Category Higher
            /// </summary>
            Higher,

            /// <summary>
            /// Category Medium
            /// </summary>
            Medium, 

            /// <summary>
            /// Category Lower
            /// </summary>
            Lower
        }
        #endregion
        
        #region | Constructor |
        /// <summary>
        /// Initializes a new instance of the SkillsCategory class
        /// </summary>
        /// <param name="title">string holding the title</param>
        /// <param name="score">double holiding the score</param>
        /// <param name="rankOneCount">double holding the rank count</param>
        /// <param name="resources">Reference type holding the correct resource file</param>
        public SkillsCategory(string title, double score, double rankOneCount, ResourceManager resources)
        {
            this.Title = title;
            this.Score = score;
            this.RankOneResponses = rankOneCount;
            this.ScoreScale = GetScale(score);
            SetResourceValues(resources);
        }
        #endregion

        #region | Properties |

        /// <summary>
        /// Get / Set rank on responses
        /// </summary>
        public double RankOneResponses { get; set; }

        /// <summary>
        /// Get / Set title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get / Set score
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Get / Set score scale
        /// </summary>
        public Scale ScoreScale { get; set; }

        /// <summary>
        /// Get / Set skill name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get / Set skill definition
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Get / Set skill description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get / Set development tip 1
        /// </summary>
        public string DevTip1 { get; set; }

        /// <summary>
        /// Get / Set development tip 2
        /// </summary>
        public string DevTip2 { get; set; }

        /// <summary>
        /// Get / Set strength
        /// </summary>
        public string Strength { get; set; }

        #endregion

        #region | Private Methods |

        /// <summary>
        /// Returns score scale
        /// </summary>
        /// <param name="score">int holding the score</param>
        /// <returns>return an objcect of type scale</returns>
        private Scale GetScale(double score)
        {
            Scale scale = scale = Scale.Higher;

            if (Math.Round(score, 5) <= 0.33333)
            {
                scale = Scale.Lower;
            }
            else if (Math.Round(score, 5) < 0.66665)
            {
                scale = Scale.Medium;
            }
            else
            {
                scale = Scale.Higher;
            }

            return scale;
        }

        /// <summary>
        /// Initialise property values from the resource file
        /// </summary>
        /// <param name="res"></param>
        private void SetResourceValues(ResourceManager res)
        {
            this.Name = res.GetString(string.Format(Constant.SkillsAreaName, this.Title.Substring(2, 1)));
            this.Definition = res.GetString(string.Format(Constant.SkillsAreaDefinition, this.Title.Substring(2, 1)));
            this.Description = res.GetString(string.Format(Constant.SkillsAreaDescription, this.Title.Substring(2, 1)));
            this.DevTip1 = res.GetString(string.Format(Constant.SkillsAreaDevelopmentTip1, this.Title.Substring(2, 1)));
            this.DevTip2 = res.GetString(string.Format(Constant.SkillsAreaDevelopmentTip2, this.Title.Substring(2, 1)));
            this.Strength = res.GetString(string.Format(Constant.SkillsAreaStrength, this.Title.Substring(2, 1)));
        }

        #endregion
    }
}
