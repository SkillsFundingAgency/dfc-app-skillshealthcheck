using System.Xml.Linq;

namespace ReferenceDataMigrations;

public static class JobFamiliesDataSetupScript
{
    public const string HeaderSql = @"
SET IDENTITY_INSERT JobFamilies ON;

DECLARE @JobFamilies TABLE (
	        Id INT,
            Code NVARCHAR(50),
            Title NVARCHAR(255),
            KeySkillsStatement1 NVARCHAR(MAX),
            KeySkillsStatement2 NVARCHAR(MAX),
            KeySkillsStatement3 NVARCHAR(MAX),
            TakingResponsibility FLOAT,
            WorkingWithOthers FLOAT,
            PersuadingAndSpeaking FLOAT,
            ThinkingCritically FLOAT,
            CreationAndInnovation FLOAT,
            PlanningAndOrganising FLOAT,
            HandlingChangeAndPressure FLOAT,
            AchievingResults FLOAT,
            LearningAndTechnology FLOAT,
            Verbal BIT,
            Numerical BIT,
            Checking BIT,
            Spatial BIT,
            Abstract BIT,
            Mechanical BIT,
            RelevantTasksCompletedText NVARCHAR(MAX),
            RelevantTasksNotCompletedText NVARCHAR(MAX)
        )


INSERT INTO @JobFamilies (  Id,
                            Code, 
                            Title, 
                            KeySkillsStatement1, 
                            KeySkillsStatement2, 
                            KeySkillsStatement3, 
                            TakingResponsibility, 
                            WorkingWithOthers, 
                            PersuadingAndSpeaking,
                            ThinkingCritically,
                            CreationAndInnovation,
                            PlanningAndOrganising,
                            HandlingChangeAndPressure,
                            AchievingResults,
                            LearningAndTechnology,
                            Verbal,
                            Numerical,
                            Checking,
                            Spatial,
                            Abstract,
                            Mechanical,
                            RelevantTasksCompletedText,
                            RelevantTasksNotCompletedText) VALUES";

    public const string FooterSql = @"
MERGE JobFamilies AS target using (SELECT * FROM   @JobFamilies) AS source
ON source.id = target.Id
WHEN matched THEN
    UPDATE SET target.Code = source.Code,
             target.Title = source.Title,
             target.KeySkillsStatement1 = source.KeySkillsStatement1,
             target.KeySkillsStatement2 = source.KeySkillsStatement2,
             target.KeySkillsStatement3 = source.KeySkillsStatement3,
             target.TakingResponsibility = source.TakingResponsibility,
             target.WorkingWithOthers = source.WorkingWithOthers,
             target.PersuadingAndSpeaking = source.PersuadingAndSpeaking,
             target.ThinkingCritically = source.ThinkingCritically,
             target.CreationAndInnovation = source.CreationAndInnovation,
             target.PlanningAndOrganising = source.PlanningAndOrganising,
             target.HandlingChangeAndPressure =
             source.HandlingChangeAndPressure,
             target.AchievingResults =
             source.AchievingResults,
             target.LearningAndTechnology = source.LearningAndTechnology,
             target.Verbal = source.Verbal,
             target.Numerical = source.Numerical,
             target.Checking = source.Checking,
             target.Spatial = source.Spatial,
             target.Abstract = source.Abstract,
             target.Mechanical = source.Mechanical,
             target.RelevantTasksCompletedText =
             source.RelevantTasksCompletedText,
             target.RelevantTasksNotCompletedText =
             source.RelevantTasksNotCompletedText
WHEN NOT matched THEN
    INSERT ( Id,
           Code,
           Title,
           KeySkillsStatement1,
           KeySkillsStatement2,
           KeySkillsStatement3,
           TakingResponsibility,
           WorkingWithOthers,
           PersuadingAndSpeaking,
           ThinkingCritically,
           CreationAndInnovation,
           PlanningAndOrganising,
           HandlingChangeAndPressure,
           AchievingResults,
           LearningAndTechnology,
           Verbal,
           Numerical,
           Checking,
           Spatial,
           Abstract,
           Mechanical,
           RelevantTasksCompletedText,
           RelevantTasksNotCompletedText )
    VALUES ( source.Id,
           source.Code,
           source.Title,
           source.KeySkillsStatement1,
           source.KeySkillsStatement2,
           source.KeySkillsStatement3,
           source.TakingResponsibility,
           source.WorkingWithOthers,
           source.PersuadingAndSpeaking,
           source.ThinkingCritically,
           source.CreationAndInnovation,
           source.PlanningAndOrganising,
           source.HandlingChangeAndPressure,
           source.AchievingResults,
           source.LearningAndTechnology,
           source.Verbal,
           source.Numerical,
           source.Checking,
           source.Spatial,
           source.Abstract,
           source.Mechanical,
           source.RelevantTasksCompletedText,
           source.RelevantTasksNotCompletedText )
    WHEN NOT matched BY source THEN
        DELETE;

SET IDENTITY_INSERT JobFamilies ON;";

    public static void Execute(string sourceXml, string targetSql)
    {
        try
        {
            var doc = XDocument.Load(sourceXml);

            using var writer = new StreamWriter(targetSql);
            writer.WriteLine(HeaderSql);
            foreach (var element in doc.Descendants("JobFamily"))
            {
                var result = GenerateInsertStatement(element);
                writer.WriteLine(result);
            }

            writer.WriteLine(FooterSql);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string GenerateInsertStatement(XElement e)
    {
        var id = e.Element("JobFamilyId")!.Value;
        var code = e.Element("JobFamilyDetailId")!.Value;
        var title = e.Element("JobFamilyTitle")!.Value;
        var keySkillsStatement1 = e.Element("JobFamilyKeySkillsStatement1")!.Value;
        var keySkillsStatement2 = e.Element("JobFamilyKeySkillsStatement2")!.Value;
        var keySkillsStatement3 = e.Element("JobFamilyKeySkillsStatement3")!.Value;
        var takingResponsibility = e.Element("TakingResponsibility")!.Value;
        var workingWithOthers = e.Element("WorkingWithOthers")!.Value;
        var persuadingAndSpeaking = e.Element("PersuadingAndSpeaking")!.Value;
        var thinkingCritically = e.Element("ThinkingCritically")!.Value;
        var creationAndInnovation = e.Element("CreationAndInnovation")!.Value;
        var planningAndOrganising = e.Element("PlanningAndOrganising")!.Value;
        var handlingChangeAndPressure = e.Element("HandlingChangeAndPressure")!.Value;
        var achievingResults = e.Element("AchievingResults")!.Value;
        var learningAndTechnology = e.Element("LearningAndTechnology")!.Value;
        var verbal = e.Element("Verbal")!.Value.Equals("True") ? "1" : "0";
        var numerical = e.Element("Numerical")!.Value.Equals("True") ? "1" : "0";
        var checking = e.Element("Checking")!.Value.Equals("True") ? "1" : "0";
        var spatial = e.Element("Spatial")!.Value.Equals("True") ? "1" : "0";
        var abs = e.Element("Abstract")!.Value.Equals("True") ? "1" : "0";
        var mechanical = e.Element("Mechanical")!.Value.Equals("True") ? "1" : "0";
        var relevantTasksCompletedText = e.Element("RelevantTasksCompletedText")!.Value;
        var relevantTasksNotCompletedText = e.Element("RelevantTasksNotCompletedText")!.Value;

        var insertStatement = $"({id}, '{code}', '{title}', '{keySkillsStatement1}', '{keySkillsStatement2}' ,'{keySkillsStatement3}', {takingResponsibility}, {workingWithOthers}, {persuadingAndSpeaking}, {thinkingCritically}, {creationAndInnovation}, {planningAndOrganising}, {handlingChangeAndPressure}, {achievingResults}, {learningAndTechnology}, {verbal}, {numerical}, {checking}, {spatial}, {abs}, {mechanical}, '{relevantTasksCompletedText}', '{relevantTasksNotCompletedText}'),";

        return insertStatement;

    }
}