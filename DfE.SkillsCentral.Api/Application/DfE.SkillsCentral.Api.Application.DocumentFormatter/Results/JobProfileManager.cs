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
    using Microsoft.WindowsAzure.ServiceRuntime;
    /// <summary>
    /// JobProfileManager - This class is used to retrieve job family suggestions from sharepoint based on
    /// skills and or interest 
    /// </summary>
    public class JobProfileManager
    {
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
            string jobfamiliesXmlPath = GetJobFamiliesXmlPath();

            try
            {
                XDocument doc = XDocument.Load(jobfamiliesXmlPath);
                var items = from XElement c in doc.Descendants(JobFamilyConstant.JOBFAMILY)
                            select c;
                foreach (XElement item in items)
                {
                    JobFamilyProfile jfp = new JobFamilyProfile();
                    jfp.Title = GetElementValue(item.Element(JobFamilyConstant.JOBFAMILYTITLE));
                    jfp.Abstract = Convert.ToBoolean(GetElementValue(item.Element(JobFamilyConstant.ABSTRACT)));
                    jfp.AchievingResults = Convert.ToDouble(GetElementValue(item.Element(JobFamilyConstant.ACHIEVINGRESULTS)));
                    jfp.Checking = Convert.ToBoolean(GetElementValue(item.Element(JobFamilyConstant.CHECKING)));
                    jfp.CreationAndInnovation = Convert.ToDouble(GetElementValue(item.Element(JobFamilyConstant.CREATIONANDINNOVATION)));
                    jfp.HandlingChangeAndPressure = Convert.ToDouble(GetElementValue(item.Element(JobFamilyConstant.HANDLINGCHANGEANDPRESSURE)));

                    var excludeQual = from XElement qual in item.Element(JobFamilyConstant.EXCLUDEQUALIFICATIONLEVEL).Descendants()
                                      select qual;
                    if (excludeQual.Count() > 0)
                    {
                        jfp.ExcludeQualificationLevel = new string[excludeQual.Count()];
                    }

                    int idx = 0;
                    foreach (XElement qitem in excludeQual)
                    {
                        jfp.ExcludeQualificationLevel[idx] = GetElementValue(qitem);
                        idx++;
                    }

                    var interestAreas = from XElement interest in item.Element(JobFamilyConstant.INTERESTAREAS).Descendants()
                                        select interest;

                    //Intialise the array.
                    if (interestAreas.Count() > 0)
                    {
                        jfp.InterestAreas = new string[interestAreas.Count()];
                    }

                    idx = 0;
                    foreach (XElement interestitem in interestAreas)
                    {
                        jfp.InterestAreas[idx] = GetElementValue(interestitem);
                        idx++;
                    }

                    jfp.JobFamilyDetailID = GetElementValue(item.Element(JobFamilyConstant.JOBFAMILYDETAILID));
                    jfp.JobFamilyID = GetElementValueInteger(item.Element(JobFamilyConstant.JOBFAMILYID));
                    jfp.JobFamilyKeySkillsStatement1 = GetElementValue(item.Element(JobFamilyConstant.JOBFAMILYKEYSKILLSSTATEMENT1));
                    jfp.JobFamilyKeySkillsStatement2 = GetElementValue(item.Element(JobFamilyConstant.JOBFAMILYKEYSKILLSSTATEMENT2));
                    jfp.JobFamilyKeySkillsStatement3 = GetElementValue(item.Element(JobFamilyConstant.JOBFAMILYKEYSKILLSSTATEMENT3));
                    jfp.JobFamilyUrl = GetElementValue(item.Element(JobFamilyConstant.JOBFAMILYURL));
                    jfp.LearningAndTechnology = GetElementValueDouble(item.Element(JobFamilyConstant.LEARNINGANDTECHNOLOGY));
                    jfp.Mechanical = GetElementValueBool(item.Element(JobFamilyConstant.MECHANICAL));
                    jfp.Numerical = GetElementValueBool(item.Element(JobFamilyConstant.NUMERICAL));
                    jfp.PersuadingAndSpeaking = GetElementValueDouble(item.Element(JobFamilyConstant.PERSUADINGANDSPEAKING));
                    jfp.PlanningAndOrganising = GetElementValueDouble(item.Element(JobFamilyConstant.PLANNINGANDORGANISING));
                    jfp.RelevantTasksCompletedText = GetElementValue(item.Element(JobFamilyConstant.RELEVANTTASKSCOMPLETEDTEXT));
                    jfp.RelevantTasksNotCompletedText = GetElementValue(item.Element(JobFamilyConstant.RELEVANTTASKSNOTCOMPLETEDTEXT));
                    jfp.Spatial = GetElementValueBool(item.Element(JobFamilyConstant.SPATIAL));
                    jfp.TakingResponsibility = GetElementValueDouble(item.Element(JobFamilyConstant.TAKINGRESPONSIBILITY));
                    jfp.ThinkingCritically = GetElementValueDouble(item.Element(JobFamilyConstant.THINKINGCRITICALLY));
                    jfp.Verbal = GetElementValueBool(item.Element(JobFamilyConstant.VERBAL));
                    jfp.WorkingWithOthers = GetElementValueDouble(item.Element(JobFamilyConstant.WORKINGWITHOTHERS));

                    allJobProfiles.Add(jfp);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while reading job family xml file {0}", jobfamiliesXmlPath), ex);
            }

            return allJobProfiles;
        }

        /// <summary>
        /// Gets the path to job profiles xml file
        /// </summary>
        /// <returns></returns>
        private static string GetJobFamiliesXmlPath()
        {
            string jobfamiliesXmlPath = ConfigurationManager.AppSettings[JobFamilyConstant.JOBFAMILYSOURCEFILE];

            try
            {
                if (RoleEnvironment.IsAvailable)
                {
                    //if hosted in azure determine the path from role root
                    jobfamiliesXmlPath = Path.Combine(JobFamilyConstant.AzureBinFolder, jobfamiliesXmlPath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while finding path to job family xml file {0}", 
                    ConfigurationManager.AppSettings[JobFamilyConstant.JOBFAMILYSOURCEFILE]), ex);
            }

            return jobfamiliesXmlPath;
        }

        /// <summary>
        /// Returns xelement value if initialised otherwise returns empty string
        /// </summary>
        /// <param name="item">element value to be retrieved</param>
        /// <returns>string holding the element value</returns>
        private string GetElementValue(XElement item)
        {
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Value;
            }
        }

        /// <summary>
        /// Returns element value as double
        /// </summary>
        /// <param name="item">element value to be retrieved</param>
        /// <returns>Element value</returns>
        private double GetElementValueDouble(XElement item)
        {
            double returnvalue = 0;
            if (item != null)
            {
                double.TryParse(item.Value, out returnvalue);
            }

            return returnvalue;
        }

        /// <summary>
        /// Returns element value as boolean
        /// </summary>
        /// <param name="item">element value to be retrieved</param>
        /// <returns>Element value</returns>
        private bool GetElementValueBool(XElement item)
        {
            bool returnvalue = false;
            if (item != null)
            {
                bool.TryParse(item.Value, out returnvalue);
            }

            return returnvalue;
        }

        /// <summary>
        /// Returns element value as Integer
        /// </summary>
        /// <param name="item">element value to be retrieved</param>
        /// <returns>Element value</returns>
        private int GetElementValueInteger(XElement item)
        {
            int returnvalue = 0;
            if (item != null)
            {
                int.TryParse(item.Value, out returnvalue);
            }

            return returnvalue;
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
