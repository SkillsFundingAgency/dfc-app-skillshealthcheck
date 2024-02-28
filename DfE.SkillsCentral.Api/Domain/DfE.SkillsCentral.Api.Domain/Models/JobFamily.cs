namespace DfE.SkillsCentral.Api.Domain.Models;

public class JobFamily
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? KeySkillsStatement1 { get; set; }
    public string? KeySkillsStatement2 { get; set; }
    public string? KeySkillsStatement3 { get; set; }
    public double TakingResponsibility { get; set; }
    public double WorkingWithOthers { get; set; }
    public double PersuadingAndSpeaking { get; set; }
    public double ThinkingCritically { get; set; }
    public double CreationAndInnovation { get; set; }
    public double PlanningAndOrganising { get; set; }
    public double HandlingChangeAndPressure { get; set; }
    public double AchievingResults { get; set; }
    public double LearningAndTechnology { get; set; }
    public bool Verbal { get; set; }
    public bool Numerical { get; set; }
    public bool Checking { get; set; }
    public bool Spatial { get; set; }
    public bool Abstract { get; set; }
    public bool Mechanical { get; set; }
    public string? RelevantTasksCompletedText { get; set; }
    public string? RelevantTasksNotCompletedText { get; set; }
    public List<InterestArea> InterestAreas { get; set; } = new();
    public string? Url { get; set; }
}