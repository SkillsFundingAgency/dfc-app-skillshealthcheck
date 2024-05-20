// -----------------------------------------------------------------------
// <copyright file="JobFamilyCconstant.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    /// <summary>
    /// JobFamilyCconstant - Holds constant for job family xml & configuration items
    /// </summary>
    public class JobFamilyConstant
    {
        #region | Config Key |
        public const string JOBFAMILYSOURCEFILE = "JobFamilySourceFile";
        #endregion

        #region | Xml Elements |
        public const string JOBFAMILY = "JobFamily";
        public const string JOBFAMILYTITLE = "JobFamilyTitle";
        public const string ABSTRACT = "Abstract";
        public const string ACHIEVINGRESULTS = "AchievingResults";
        public const string CHECKING = "Checking";
        public const string CREATIONANDINNOVATION = "CreationAndInnovation";
        public const string HANDLINGCHANGEANDPRESSURE = "HandlingChangeAndPressure";
        public const string EXCLUDEQUALIFICATIONLEVEL = "ExcludeQualificationLevel";
        public const string INTERESTAREAS = "InterestAreas";
        public const string JOBFAMILYDETAILID = "JobFamilyDetailId";
        public const string JOBFAMILYID = "JobFamilyId";
        public const string JOBFAMILYKEYSKILLSSTATEMENT1 = "JobFamilyKeySkillsStatement1";
        public const string JOBFAMILYKEYSKILLSSTATEMENT2 = "JobFamilyKeySkillsStatement2";
        public const string JOBFAMILYKEYSKILLSSTATEMENT3 = "JobFamilyKeySkillsStatement3";
        public const string JOBFAMILYURL = "JobFamilyURL";
        public const string LEARNINGANDTECHNOLOGY = "LearningAndTechnology";
        public const string MECHANICAL = "Mechanical";
        public const string NUMERICAL = "Numerical";
        public const string PERSUADINGANDSPEAKING = "PersuadingAndSpeaking";
        public const string PLANNINGANDORGANISING = "PlanningAndOrganising";
        public const string RELEVANTTASKSCOMPLETEDTEXT = "RelevantTasksCompletedText";
        public const string RELEVANTTASKSNOTCOMPLETEDTEXT = "RelevantTasksNotCompletedText";
        public const string SPATIAL = "Spatial";
        public const string TAKINGRESPONSIBILITY = "TakingResponsibility";
        public const string THINKINGCRITICALLY = "ThinkingCritically";
        public const string VERBAL = "Verbal";
        public const string WORKINGWITHOTHERS = "WorkingWithOthers";
        #endregion

        #region Azure

        /// <summary>
        /// Returns bin folder location in azure
        /// </summary>
        public static string AzureBinFolder
        {
            get
            {
                return Environment.GetEnvironmentVariable("RoleRoot") + "\\approot\\bin\\";
            }
        }

        #endregion
    }
}
