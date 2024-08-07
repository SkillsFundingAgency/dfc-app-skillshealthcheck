﻿namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using DfE.SkillsCentral.Api.Application.Interfaces.Repositories;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;



    public class JobSuggestionResult : SHCResultBase
    {
        private IJobFamiliesRepository _jobFamiliesRepository;
        public JobSuggestionResult(string qualificationLevel, string jobFamilyExclude1, string jobFamilyExclude2, string jobFamilyExclude3, IJobFamiliesRepository jobFamiliesRepository)
            : base(SHCReportSection.JobSugession.ToString(), qualificationLevel, string.Empty, string.Empty, string.Empty)
        {
            _jobFamiliesRepository = jobFamiliesRepository;
            JobFamilyExclude = new List<string>();
            if (!string.IsNullOrEmpty(jobFamilyExclude1))
            {
                this.JobFamilyExclude.Add(jobFamilyExclude1);
            }

            if (!string.IsNullOrEmpty(jobFamilyExclude2))
            {
                this.JobFamilyExclude.Add(jobFamilyExclude2);
            }

            if (!string.IsNullOrEmpty(jobFamilyExclude3))
            {
                this.JobFamilyExclude.Add(jobFamilyExclude3);
            }
        }
        public List<string> JobFamilyExclude { get; set; }

        public List<SkillsCategory> RankedSkillsCategories { get; set; }

        public List<InterestCategory> InterestCategories { get; set; }

        private JobSuggestionSummary Summary { get; set; }

        private JobSuggestionBody Body { get; set; }

        public bool VerbalComplete { get; set; }

        public bool NumericComplete { get; set; }

        public bool CheckingComplete { get; set; }

        public bool SpatialComplete { get; set; }

        public bool AbstractComplete { get; set; }

        public bool MechanicalComplete { get; set; }

        private string InterestBandName { get; set; }

        private List<JobFamilyProfile> GetJobFamilies()
        {
           var jobFamilyProfiles =  _jobFamiliesRepository.GetAllAsync().Result;
            JobProfileManager jpm = new JobProfileManager(jobFamilyProfiles);
            List<JobFamilyProfile> jobFamilies = new List<JobFamilyProfile>();
            float takingResponsibilityRank = 0,
                  workingWithOthersRank = 0,
                  persuadingAndSpeakingRank = 0,
                  thinkingCriticallyRank = 0,
                  creationAndInnovationRank = 0,
                  planningAndOrganisingRank = 0,
                  handlingChangeAndPressureRank = 0,
                  achievingResultsRank = 0,
                  learningAndTechnologyRank = 0;

            for (int i = 1; i <= this.RankedSkillsCategories.Count; i++)
            {
                switch (this.RankedSkillsCategories[i - 1].Title)
                {
                    case "CT1":
                        takingResponsibilityRank = i;
                        break;
                    case "CT2":
                        workingWithOthersRank = i;
                        break;
                    case "CT3":
                        persuadingAndSpeakingRank = i;
                        break;
                    case "CT4":
                        thinkingCriticallyRank = i;
                        break;
                    case "CT5":
                        creationAndInnovationRank = i;
                        break;
                    case "CT6":
                        planningAndOrganisingRank = i;
                        break;
                    case "CT7":
                        handlingChangeAndPressureRank = i;
                        break;
                    case "CT8":
                        achievingResultsRank = i;
                        break;
                    case "CT9":
                        learningAndTechnologyRank = i;
                        break;
                }
            }

            List<InterestCategory> interestCategoriesToGetJobFamilies = new List<InterestCategory>();
            int skillsRows = 0;
            int interestRows = 0;
            int interestAndSkilsRows = 0;
            int.TryParse(this.Resource.GetString(Constant.Skills_JobFamiliesToDisplay), out skillsRows);
            int.TryParse(this.Resource.GetString(Constant.Interests_JobFamiliesToDisplay), out interestRows);
            int.TryParse(this.Resource.GetString(Constant.SkillsAndInterestJobFamiliesToDisplay), out interestAndSkilsRows);

            if (this.InterestCategories != null && this.InterestCategories.Count > 0)
            {
                string[] interestBandNamesToGetJobFamilies = this.Resource.GetString(Constant.InterestBandNamesToGetJobFamilies).Split(Constant.AnswerSeparator);
                foreach (string interestBandNameToGetJobFamilies in interestBandNamesToGetJobFamilies)
                {
                    interestCategoriesToGetJobFamilies.AddRange(this.InterestCategories.FindAll(e => e.Name.Equals(interestBandNameToGetJobFamilies)));
                }

                if (interestCategoriesToGetJobFamilies != null && interestCategoriesToGetJobFamilies.Count > 0)
                {
                    List<string> interests = new List<string>();
                    int interestsCount = 0;
                    foreach (InterestCategory category in interestCategoriesToGetJobFamilies)
                    {
                        string categoryInterests = string.Empty;
                        for (int i = 0; i < category.Interests.Count; i++)
                        {
                            Interest interest = category.Interests[i];
                            if (i != category.Interests.Count - 1)
                            {
                                categoryInterests += interest.InternalName + "|";
                            }
                            else
                            {
                                categoryInterests += interest.InternalName;
                            }

                            interestsCount += 1;
                            if (interestsCount == 1)
                            {
                                InterestBandName = category.DisplayName;
                            }
                        }

                        interests.Add(categoryInterests);
                    }

                    if (interestsCount > 0)
                    {
                        jobFamilies = jpm.GetJobFamiliesSkillsAndInterests(JobFamilyExclude.ToArray(),
                                                                           "1",
                                                                           interests.ToArray(),
                                                                           interestRows,
                                                                           interestAndSkilsRows - interestRows,
                                                                           takingResponsibilityRank,
                                                                           workingWithOthersRank,
                                                                           persuadingAndSpeakingRank,
                                                                           thinkingCriticallyRank,
                                                                           creationAndInnovationRank,
                                                                           planningAndOrganisingRank,
                                                                           handlingChangeAndPressureRank,
                                                                           achievingResultsRank,
                                                                           learningAndTechnologyRank);
                        if (interestsCount == 1 && jobFamilies != null && jobFamilies.FindAll(c => c.IsFromInterests == true).Count == 0)
                        {
                            Summary = JobSuggestionSummary.OneVeryorModeratleyInterest;
                            Body = JobSuggestionBody.OnlySkills;
                        }
                        else
                        {
                            Summary = JobSuggestionSummary.MoreVeryorModeratleyInterest;
                            Body = JobSuggestionBody.SkillsAndInterests;
                        }
                    }
                    else
                    {
                        jobFamilies = jpm.GetSkillJobFamilies(JobFamilyExclude.ToArray(),
                                                                "1",
                                                                skillsRows,
                                                                takingResponsibilityRank,
                                                                workingWithOthersRank,
                                                                persuadingAndSpeakingRank,
                                                                thinkingCriticallyRank,
                                                                creationAndInnovationRank,
                                                                planningAndOrganisingRank,
                                                                handlingChangeAndPressureRank,
                                                                achievingResultsRank,
                                                                learningAndTechnologyRank);

                        Summary = JobSuggestionSummary.AllLittelAndNoInterest;
                        Body = JobSuggestionBody.OnlySkills;
                    }
                }
            }
            else if (this.RankedSkillsCategories != null && this.RankedSkillsCategories.Count > 0)
            {
                jobFamilies = jpm.GetSkillJobFamilies(JobFamilyExclude.ToArray(),
                                                       "1",
                                                       skillsRows,
                                                       takingResponsibilityRank,
                                                       workingWithOthersRank,
                                                       persuadingAndSpeakingRank,
                                                       thinkingCriticallyRank,
                                                       creationAndInnovationRank,
                                                       planningAndOrganisingRank,
                                                       handlingChangeAndPressureRank,
                                                       achievingResultsRank,
                                                       learningAndTechnologyRank);

                Summary = JobSuggestionSummary.OnlySkills;
                Body = JobSuggestionBody.OnlySkills;
            }

            return jobFamilies;
        }

        private string GetQualificationLevel()
        {
            string educationLevel;
            if (int.Parse(this.QualificationLevel) <= 1)
            {
                educationLevel = "Level 1";
            }
            else if (int.Parse(this.QualificationLevel) == 2)
            {
                educationLevel = "Level 2";
            }
            else
            {
                educationLevel = "Level 3";
            }

            return educationLevel;
        }

        private string GetInterestBandName(int interest)
        {
            string bandName = string.Empty;
            switch (interest)
            {
                case 0:
                    bandName = "extremely";
                    break;
                case 1:
                    bandName = "very";
                    break;
                case 2:
                    bandName = "moderately";
                    break;
                case 3:
                    bandName = "a little";
                    break;
                case 4:
                    bandName = "not at all";
                    break;
            }

            return bandName;
        }

        private void BuildJobFamilyXML(XmlTextWriter xml, JobFamilyProfile jsp)
        {
            xml.WriteElementString(Constant.XmlTitleElement, jsp.Title);
            xml.WriteElementString(Constant.XmlkeyStatement1, jsp.JobFamilyKeySkillsStatement1);
            xml.WriteElementString(Constant.XmlkeyStatement2, jsp.JobFamilyKeySkillsStatement2);
            xml.WriteElementString(Constant.XmlkeyStatement3, jsp.JobFamilyKeySkillsStatement3);
            if (jsp.InterestAreaUsedInSklills)
            {
                xml.WriteElementString(Constant.XmlJobFamiliyBandMatched, GetInterestBandName(jsp.InterestLevel.GetValueOrDefault()));
            }

            bool taskCompleted = true;
            Dictionary<KeyValuePair<string, bool>, bool> AssessmentFlags = new Dictionary<KeyValuePair<string, bool>, bool>();
            AssessmentFlags.Add(new KeyValuePair<string, bool>(Constant.XmlVerbalRootElement, this.VerbalComplete), jsp.Verbal.GetValueOrDefault());
            AssessmentFlags.Add(new KeyValuePair<string, bool>(Constant.XmlNumericalRootElement, this.NumericComplete), jsp.Numerical.GetValueOrDefault());
            AssessmentFlags.Add(new KeyValuePair<string, bool>(Constant.XmlChkRootElement, this.CheckingComplete), jsp.Checking.GetValueOrDefault());
            AssessmentFlags.Add(new KeyValuePair<string, bool>(Constant.XmlShapesRootElement, this.SpatialComplete), jsp.Spatial.GetValueOrDefault());
            AssessmentFlags.Add(new KeyValuePair<string, bool>(Constant.XmlAbstractRootElement, this.AbstractComplete), jsp.Abstract.GetValueOrDefault());
            AssessmentFlags.Add(new KeyValuePair<string, bool>(Constant.XmlMechanicalRootElement, this.MechanicalComplete), jsp.Mechanical.GetValueOrDefault());
            List<KeyValuePair<KeyValuePair<string, bool>, bool>> requireAssements = AssessmentFlags.ToList().FindAll(c => c.Value == true);
            if (requireAssements != null && requireAssements.Count > 0)
            {
                foreach (KeyValuePair<KeyValuePair<string, bool>, bool> assessment in requireAssements)
                {
                    if (assessment.Key.Value == false)
                    {
                        taskCompleted = false;
                        break;
                    }
                }
            }

            if (taskCompleted)
            {
                xml.WriteElementString(Constant.XmlJobFamiliyTask, jsp.RelevantTasksCompletedText);
            }
            else
            {
                xml.WriteElementString(Constant.XmlJobFamiliyTask, jsp.RelevantTasksNotCompletedText);
            }
        }

        public override string GetResult()
        {
            if (!this.IsComplete)
            {
                return GetXML(null, Constant.XmlJobSuggestionsRootElement);
            }
            else
            {
                string returnXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(sw))
                    {
                        List<JobFamilyProfile> jfs = new List<JobFamilyProfile>();
                        jfs = GetJobFamilies();
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlJobSuggestionsRootElement, this.Complete);
                        xml.WriteStartElement(Constant.XmlJobSuggestionsRootElement);
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlJobSummary, this.Summary.ToString("d"));
                        xml.WriteElementString(Constant.ShowTagPrefix + Constant.XmlJobBody, this.Body.ToString("d"));

                        xml.WriteElementString(Constant.XmlJobInterestBandName, this.InterestBandName);
                        if (Body == JobSuggestionBody.SkillsAndInterests)
                        {
                            xml.WriteStartElement(Constant.XmlInterestedJobFamilies);
                            foreach (JobFamilyProfile jsp in jfs)
                            {
                                if (jsp.IsFromInterests)
                                {
                                    xml.WriteStartElement(Constant.XmlInterestedJobFamily);
                                    BuildJobFamilyXML(xml, jsp);
                                    xml.WriteEndElement();
                                }
                            }

                            xml.WriteEndElement();

                            xml.WriteStartElement(Constant.XmlSkillsJobFamilies);
                            foreach (JobFamilyProfile jsp in jfs)
                            {
                                if (jsp.IsFromSkills)
                                {
                                    xml.WriteStartElement(Constant.XmlSkillsJobFamily);
                                    BuildJobFamilyXML(xml, jsp);
                                    xml.WriteEndElement();
                                }
                            }

                            xml.WriteEndElement();
                        }
                        else
                        {
                            xml.WriteStartElement(Constant.XmlSkillsJobFamilies);
                            foreach (JobFamilyProfile jsp in jfs)
                            {
                                if (jsp.IsFromSkills)
                                {
                                    xml.WriteStartElement(Constant.XmlSkillsJobFamily);
                                    BuildJobFamilyXML(xml, jsp);
                                    xml.WriteEndElement();
                                }
                            }

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