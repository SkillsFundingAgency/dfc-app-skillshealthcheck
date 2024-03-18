namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    public class JobFamilyProfile
    {
        public JobFamilyProfile()
        {
        }
        public string Title { get; set; }

        public string JobFamilyDetailID { get; set; }

        public int JobFamilyID { get; set; }

        public string JobFamilyKeySkillsStatement1 { get; set; }

        public string JobFamilyKeySkillsStatement2 { get; set; }

        public string JobFamilyKeySkillsStatement3 { get; set; }

        public double? TakingResponsibility { get; set; }

        public double? WorkingWithOthers { get; set; }

        public double? PersuadingAndSpeaking { get; set; }

        public double? ThinkingCritically { get; set; }

        public double? CreationAndInnovation { get; set; }

        public double? PlanningAndOrganising { get; set; }

        public double? HandlingChangeAndPressure { get; set; }

        public double? AchievingResults { get; set; }

        public double? LearningAndTechnology { get; set; }

        public bool? Verbal { get; set; }

        public bool? Numerical { get; set; }

        public bool? Checking { get; set; }

        public bool? Mechanical { get; set; }

        public bool? Spatial { get; set; }

        public bool? Abstract { get; set; }

        public string RelevantTasksCompletedText { get; set; }

        public string RelevantTasksNotCompletedText { get; set; }
        
        public string JobFamilyUrl { get; set; }

        public string[] InterestAreas { get; set; } = Array.Empty<string>();
        public string[] ExcludeQualificationLevel { get; set; }

        public string[] InterestBands { get; set; }
        
        public int? InterestLevel { get; set; }

        public double? JobFamilyFitScore
        {
            get;
            set;
        }

        public bool InterestAreaUsedInSklills { get; set; }

        public bool IsFromSkills { get; set; }

        public bool IsFromInterests { get; set; }
    }
}
