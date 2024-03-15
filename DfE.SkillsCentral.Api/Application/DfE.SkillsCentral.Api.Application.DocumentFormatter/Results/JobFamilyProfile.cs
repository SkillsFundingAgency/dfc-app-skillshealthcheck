// -----------------------------------------------------------------------
// <copyright file="JobFamilyProfile.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    /// <summary>
    /// JobFamilyProfile - Entity to store job family profile details
    /// </summary>
    public class JobFamilyProfile
    {
        #region | Constructor |
        /// <summary>
        /// Initializes a new instance of the JobFamilyProfile class
        /// </summary>
        public JobFamilyProfile()
        {
        }
        #endregion

        #region | Properties |

        /// <summary>
        /// Job Profile Family title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Datasource unique name for Job Family format: F######
        /// </summary>
        public string JobFamilyDetailID { get; set; }

        /// <summary>
        /// Job Family legacy primary key
        /// </summary>
        public int JobFamilyID { get; set; }

        /// <summary>
        /// Job Family Key Skills Statement 1
        /// </summary>
        public string JobFamilyKeySkillsStatement1 { get; set; }

        /// <summary>
        /// Job Family Key Skills Statement 2
        /// </summary>
        public string JobFamilyKeySkillsStatement2 { get; set; }

        /// <summary>
        /// Job Family Key Skills Statement 3
        /// </summary>
        public string JobFamilyKeySkillsStatement3 { get; set; }

        /// <summary>
        /// Calibration value for Taking Responsibility skill level
        /// </summary>
        public double? TakingResponsibility { get; set; }

        /// <summary>
        /// Calibration value for Working With Others skill level
        /// </summary>
        public double? WorkingWithOthers { get; set; }

        /// <summary>
        /// Calibration value for Persuading And Speaking skill level
        /// </summary>
        public double? PersuadingAndSpeaking { get; set; }

        /// <summary>
        /// Calibration value for Thinking Critically skill level
        /// </summary>
        public double? ThinkingCritically { get; set; }

        /// <summary>
        /// Calibration value for Creation And Innovation skill level
        /// </summary>
        public double? CreationAndInnovation { get; set; }

        /// <summary>
        /// Calibration value for Planning And Organising skill level
        /// </summary>
        public double? PlanningAndOrganising { get; set; }

        /// <summary>
        /// Calibration value for Handling Change And Pressures skill level
        /// </summary>
        public double? HandlingChangeAndPressure { get; set; }

        /// <summary>
        /// Calibration value for Achieving Results skill level
        /// </summary>
        public double? AchievingResults { get; set; }

        /// <summary>
        /// Calibration value for Learning And Technology skill level
        /// </summary>
        public double? LearningAndTechnology { get; set; }

        /// <summary>
        /// Verbal skill
        /// </summary>
        public bool? Verbal { get; set; }

        /// <summary>
        /// Numerical skill
        /// </summary>
        public bool? Numerical { get; set; }

        /// <summary>
        /// Checking skill
        /// </summary>
        public bool? Checking { get; set; }

        /// <summary>
        /// Mechanical skill
        /// </summary>
        public bool? Mechanical { get; set; }

        /// <summary>
        /// Spatial skill
        /// </summary>
        public bool? Spatial { get; set; }

        /// <summary>
        /// Abstract skill
        /// </summary>
        public bool? Abstract { get; set; }

        /// <summary>
        /// Relevant Tasks Completed Text
        /// </summary>
        public string RelevantTasksCompletedText { get; set; }

        /// <summary>
        /// Relevant Tasks Not Completed Text
        /// </summary>
        public string RelevantTasksNotCompletedText { get; set; }
        
        /// <summary>
        /// Job Family Url
        /// </summary>
        public string JobFamilyUrl { get; set; }

        /// <summary>
        /// Collection of Interest Areas
        /// </summary>
        public string[] InterestAreas { get; set; }

        /// <summary>
        /// Qualification Level to exclude
        /// </summary>
        public string[] ExcludeQualificationLevel { get; set; }

        /// <summary>
        /// Banding of Interest Levels
        /// </summary>
        public string[] InterestBands { get; set; }
        
        /// <summary>
        /// If a skills jobfamily is from a interest area.
        /// Integer value of Interest Level
        /// </summary>
        public int? InterestLevel { get; set; }

        /// <summary>
        /// Calculated Job Family Fit Score
        /// </summary>
        public double? JobFamilyFitScore
        {
            get;
            set;
        }

        /// <summary>
        /// If jobfamily is from interest area  and used up in skills area.
        /// </summary>
        public bool InterestAreaUsedInSklills { get; set; }

        /// <summary>
        /// if familiy is from skills
        /// </summary>
        public bool IsFromSkills { get; set; }

        /// <summary>
        /// if familiy is from Interest area
        /// </summary>
        public bool IsFromInterests { get; set; }
        #endregion
    }
}
