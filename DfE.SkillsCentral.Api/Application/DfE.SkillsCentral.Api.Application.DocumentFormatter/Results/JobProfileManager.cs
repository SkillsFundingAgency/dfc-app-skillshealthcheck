// -----------------------------------------------------------------------
// <copyright file="JobProfileManager.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using DfE.SkillsCentral.Api.Domain.Models;
    using DocumentFormat.OpenXml.Office.CustomUI;
    /// <summary>
    /// JobProfileManager - This class is used to retrieve job family suggestions from sharepoint based on
    /// skills and or interest 
    /// </summary>
    public class JobProfileManager
    {
        private IReadOnlyList<JobFamily> _jobFamilies;
        public JobProfileManager(IReadOnlyList<JobFamily> jobFamilies)
        {
            _jobFamilies = jobFamilies;
        }
        #region | Private Methods |
        /// <summary>
        /// Prepares a List of Job Family Profiles without Fit Score calculation
        /// The application requires the .net 3.5 service update download from:
        /// http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=2343
        /// </summary>
        /// <param name="dc">data context</param>
        /// <returns>JobFamilyProfile List</returns>
        private List<JobFamilyProfile> GetAllJobFamilyProfile()
        {
            List<JobFamilyProfile> allJobProfiles = new List<JobFamilyProfile>();

            try
            {
                
                foreach (JobFamily jobFamily in _jobFamilies)
                {
                    JobFamilyProfile jfp = new JobFamilyProfile();
                    jfp.Title = jobFamily.Title;
                    jfp.Abstract = jobFamily.Abstract;
                    jfp.AchievingResults = jobFamily.AchievingResults;
                    jfp.Checking = jobFamily.Checking;
                    jfp.CreationAndInnovation = jobFamily.CreationAndInnovation;
                    jfp.HandlingChangeAndPressure = jobFamily.HandlingChangeAndPressure;


                   foreach(InterestArea intrest in  jobFamily.InterestAreas)
                    {
                        jfp.InterestAreas.Append(intrest.Name);
                    }
                    

                    jfp.JobFamilyDetailID = jobFamily.Code;
                    jfp.JobFamilyID = jobFamily.Id;
                    jfp.JobFamilyKeySkillsStatement1 = jobFamily.KeySkillsStatement1;
                    jfp.JobFamilyKeySkillsStatement2 = jobFamily.KeySkillsStatement2;
                    jfp.JobFamilyKeySkillsStatement3 = jobFamily.KeySkillsStatement3;
                    jfp.JobFamilyUrl = jobFamily.Url;
                    jfp.LearningAndTechnology = jobFamily.LearningAndTechnology;
                    jfp.Mechanical = jobFamily.Mechanical;
                    jfp.Numerical = jobFamily.Numerical;
                    jfp.PersuadingAndSpeaking = jobFamily.PersuadingAndSpeaking;
                    jfp.PlanningAndOrganising = jobFamily.PlanningAndOrganising;
                    jfp.RelevantTasksCompletedText = jobFamily.RelevantTasksCompletedText;
                    jfp.RelevantTasksNotCompletedText = jobFamily.RelevantTasksNotCompletedText;
                    jfp.Spatial = jobFamily.Spatial;
                    jfp.TakingResponsibility = jobFamily.TakingResponsibility;
                    jfp.ThinkingCritically = jobFamily.ThinkingCritically;
                    jfp.Verbal = jobFamily.Verbal;
                    jfp.WorkingWithOthers = jobFamily.WorkingWithOthers;

                    allJobProfiles.Add(jfp);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while reading job family"), ex);
            }

            return allJobProfiles;
        }

        /// <summary>
        /// Prepares a List of Job Family Profiles Fit Score calculation
        /// </summary>
        /// <param name="dc">data context</param>
        /// <param name="rankingOfTakingResponsibility">Ranking Of Taking Responsibility</param>
        /// <param name="rankingOfWorkingWithOthers">Ranking Of Working With Others</param>
        /// <param name="rankingOfPersuadingAndSpeaking">Ranking Of Persuading And Speaking</param>
        /// <param name="rankingOfThinkingCritically">Ranking Of Thinking Critically</param>
        /// <param name="rankingOfCreationAndInnovation">Ranking Of Creation And Innovation</param>
        /// <param name="rankingOfPlanningAndOrganising">Ranking Of Planning And Organising</param>
        /// <param name="rankingOfHandlingChangeAndPressure">Ranking Of Handling Change And Pressure</param>
        /// <param name="rankingOfAchievingResults">Ranking Of Achieving Results</param>
        /// <param name="rankingOfLearningAndTechnology">Ranking Of Learning And Technology</param>
        /// <returns>List of JobFamily Profiles</returns>
        private List<JobFamilyProfile> GetAllJobFamilyProfile(
            double rankingOfTakingResponsibility,
            double rankingOfWorkingWithOthers,
            double rankingOfPersuadingAndSpeaking,
            double rankingOfThinkingCritically,
            double rankingOfCreationAndInnovation,
            double rankingOfPlanningAndOrganising,
            double rankingOfHandlingChangeAndPressure,
            double rankingOfAchievingResults,
            double rankingOfLearningAndTechnology)
        {
            List<JobFamilyProfile> allJobProfiles = GetAllJobFamilyProfile();
            foreach (JobFamilyProfile item in allJobProfiles)
            {
                GetJobFamilyProfileWithFitScore(
                                        rankingOfTakingResponsibility,
                                        rankingOfWorkingWithOthers,
                                        rankingOfPersuadingAndSpeaking,
                                        rankingOfThinkingCritically,
                                        rankingOfCreationAndInnovation,
                                        rankingOfPlanningAndOrganising,
                                        rankingOfHandlingChangeAndPressure,
                                        rankingOfAchievingResults,
                                        rankingOfLearningAndTechnology,
                                        item);
            }

            return allJobProfiles;
        }

        /// <summary>
        /// Calculates Fit Score
        /// </summary>
        /// <param name="rankingOfTakingResponsibility">Ranking Of Taking Responsibility</param>
        /// <param name="rankingOfWorkingWithOthers">Ranking Of Working With Others</param>
        /// <param name="rankingOfPersuadingAndSpeaking">Ranking Of Persuading And Speaking</param>
        /// <param name="rankingOfThinkingCritically">Ranking Of Thinking Critically</param>
        /// <param name="rankingOfCreationAndInnovation">Ranking Of Creation And Innovation</param>
        /// <param name="rankingOfPlanningAndOrganising">Ranking Of Planning And Organising</param>
        /// <param name="rankingOfHandlingChangeAndPressure">Ranking Of Handling Change And Pressure</param>
        /// <param name="rankingOfAchievingResults">Ranking Of Achieving Results</param>
        /// <param name="rankingOfLearningAndTechnology">Ranking Of Learning And Technology</param>
        /// <param name="jobfam">Job Family</param>      
        private void GetJobFamilyProfileWithFitScore(
            double rankingOfTakingResponsibility,
            double rankingOfWorkingWithOthers,
            double rankingOfPersuadingAndSpeaking,
            double rankingOfThinkingCritically,
            double rankingOfCreationAndInnovation,
            double rankingOfPlanningAndOrganising,
            double rankingOfHandlingChangeAndPressure,
            double rankingOfAchievingResults,
            double rankingOfLearningAndTechnology, JobFamilyProfile jobfam)
        {
            jobfam.JobFamilyFitScore = Math.Abs(((double)jobfam.TakingResponsibility - rankingOfTakingResponsibility)) +
                Math.Abs(((double)jobfam.WorkingWithOthers - rankingOfWorkingWithOthers)) +
                Math.Abs(((double)jobfam.PersuadingAndSpeaking - rankingOfPersuadingAndSpeaking)) +
                Math.Abs(((double)jobfam.PlanningAndOrganising - rankingOfPlanningAndOrganising)) +
                Math.Abs(((double)jobfam.ThinkingCritically - rankingOfThinkingCritically)) +
                Math.Abs(((double)jobfam.CreationAndInnovation - rankingOfCreationAndInnovation)) +
                Math.Abs(((double)jobfam.HandlingChangeAndPressure - rankingOfHandlingChangeAndPressure)) +
                Math.Abs(((double)jobfam.AchievingResults - rankingOfAchievingResults)) +
                Math.Abs(((double)jobfam.LearningAndTechnology - rankingOfLearningAndTechnology));
        }
        #endregion

        #region | Public Methods |
        /// <summary>
        /// Get Job Familes with respect to Skill Level
        /// </summary>
        /// <param name="exFamilies">Families to exclude</param>
        /// <param name="exlevels">Levels to exclude</param>
        /// <param name="jobFamilyProfiles">Complete List of Job Profiles</param>
        /// <param name="jobFamilies">The Job Families requested</param>
        /// <param name="rows">The number of rows required</param>
        public List<JobFamilyProfile> GetSkillJobFamilies(
            string[] exFamilies,
            string exlevel,
            int skillsRows,
            double rankingOfTakingResponsibility,
            double rankingOfWorkingWithOthers,
            double rankingOfPersuadingAndSpeaking,
            double rankingOfThinkingCritically,
            double rankingOfCreationAndInnovation,
            double rankingOfPlanningAndOrganising,
            double rankingOfHandlingChangeAndPressure,
            double rankingOfAchievingResults,
            double rankingOfLearningAndTechnology)
        {
            List<JobFamilyProfile> allFamilies = GetAllJobFamilyProfile(
                                                                        rankingOfTakingResponsibility,
                                                                        rankingOfWorkingWithOthers,
                                                                        rankingOfPersuadingAndSpeaking,
                                                                        rankingOfThinkingCritically,
                                                                        rankingOfCreationAndInnovation,
                                                                        rankingOfPlanningAndOrganising,
                                                                        rankingOfHandlingChangeAndPressure,
                                                                        rankingOfAchievingResults,
                                                                        rankingOfLearningAndTechnology);
            List<JobFamilyProfile> jobFamilies = new List<JobFamilyProfile>();
            List<JobFamilyProfile> exFamList = new List<JobFamilyProfile>();
            foreach (string fam in exFamilies)
            {
                var exItem = from F in allFamilies
                             where F.JobFamilyDetailID == fam
                             select F;
                exFamList.AddRange(exItem);
            }

            var exlvl = from F in allFamilies
                        where F.ExcludeQualificationLevel != null && F.ExcludeQualificationLevel.Contains(exlevel)
                        select F;
            exFamList.AddRange(exlvl);

            var sjf = from F in allFamilies
                      orderby F.JobFamilyFitScore, F.Title
                      select F;

            jobFamilies = sjf.Except(exFamList).Take(skillsRows).ToList<JobFamilyProfile>();
            jobFamilies.ForEach(obj => obj.IsFromSkills = true);

            return jobFamilies;
        }

        /// <summary>
        /// Get JobFamilies with respct to both Skills And Interests
        /// </summary>
        /// <param name="exFamilies">Families to exclude</param>
        /// <param name="exLevels">Levels to exclude</param>
        /// <param name="interestBand"></param>
        /// <param name="jobFamilyProfiles">Complete List of Job Profiles<</param>
        /// <param name="jobFamilies">The Job Families requested</param>
        /// <param name="interestedRows">The number of interest rows required</param>
        /// <param name="skilledRows">The number of skill rows required</param>
        public List<JobFamilyProfile> GetJobFamiliesSkillsAndInterests(
            string[] exFamilies,
            string exLevels,
            string[] interestBand,
            int interestedRows,
            int skillsRows,
            double rankingOfTakingResponsibility,
            double rankingOfWorkingWithOthers,
            double rankingOfPersuadingAndSpeaking,
            double rankingOfThinkingCritically,
            double rankingOfCreationAndInnovation,
            double rankingOfPlanningAndOrganising,
            double rankingOfHandlingChangeAndPressure,
            double rankingOfAchievingResults,
            double rankingOfLearningAndTechnology)
        {
            List<JobFamilyProfile> jobFamilies = new List<JobFamilyProfile>();
            List<JobFamilyProfile> interestJobFamilies = new List<JobFamilyProfile>();
            List<JobFamilyProfile> allJobFamilyProfiles = GetAllJobFamilyProfile(
                rankingOfTakingResponsibility,
                rankingOfWorkingWithOthers,
                rankingOfPersuadingAndSpeaking,
                rankingOfThinkingCritically,
                rankingOfCreationAndInnovation,
                rankingOfPlanningAndOrganising,
                rankingOfHandlingChangeAndPressure,
                rankingOfAchievingResults,
                rankingOfLearningAndTechnology);

            List<JobFamilyProfile> skillJobFamilies = GetSkillJobFamilies(
                exFamilies,
                exLevels,
                skillsRows,
                rankingOfTakingResponsibility,
                rankingOfWorkingWithOthers,
                rankingOfPersuadingAndSpeaking,
                rankingOfThinkingCritically,
                rankingOfCreationAndInnovation,
                rankingOfPlanningAndOrganising,
                rankingOfHandlingChangeAndPressure,
                rankingOfAchievingResults,
                rankingOfLearningAndTechnology);

            for (int i = 0; i < interestBand.Length; i++)
            {
                string[] interest = interestBand[i].Split('|');
                for (int j = 0; j < interest.Length; j++)
                {
                    var interestedItem = from F in allJobFamilyProfiles
                                         where F.InterestAreas != null && F.InterestAreas.Contains(interest[j])
                                         && exFamilies.Contains(F.JobFamilyDetailID) == false
                                         orderby F.JobFamilyFitScore
                                         select F;

                    if (interestJobFamilies.Count < interestedRows)
                    {
                        foreach (JobFamilyProfile lijf in interestedItem)
                        {
                            if (!skillJobFamilies.Exists(jf => jf.JobFamilyID == lijf.JobFamilyID))
                            {
                                lijf.IsFromInterests = true;
                                interestJobFamilies.Add(lijf);
                                break;
                            }
                        }
                    }

                    if (i <= 2)
                    {
                        foreach (JobFamilyProfile lijf in interestedItem)
                        {
                            if (skillJobFamilies.Exists(jf => jf.JobFamilyID == lijf.JobFamilyID))
                            {
                                JobFamilyProfile lsjf = skillJobFamilies.Find(jf => jf.JobFamilyID == lijf.JobFamilyID);
                                lsjf.InterestAreaUsedInSklills = true;
                                lsjf.InterestLevel = i;
                            }
                        }
                    }
                }
            }

            if (interestJobFamilies != null && interestJobFamilies.Count > 0)
            {
                jobFamilies.AddRange(skillJobFamilies);
                jobFamilies.AddRange(interestJobFamilies);
            }
            else
            {
                skillJobFamilies = GetSkillJobFamilies(
                    exFamilies,
                    exLevels,
                    (skillsRows + interestedRows),
                    rankingOfTakingResponsibility,
                rankingOfWorkingWithOthers,
                rankingOfPersuadingAndSpeaking,
                rankingOfThinkingCritically,
                rankingOfCreationAndInnovation,
                rankingOfPlanningAndOrganising,
                rankingOfHandlingChangeAndPressure,
                rankingOfAchievingResults,
                rankingOfLearningAndTechnology);
                jobFamilies = skillJobFamilies.ToList<JobFamilyProfile>();
            }

            return jobFamilies;
        }
        #endregion
    }
}
