
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class AssessmentsQueries
    {
        public static string AllAssessments => "SELECT * FROM [Assessments]";

        public static string AssessmentById => "SELECT * FROM [Assessments] WHERE Id = @Id";

        public static string AssessmentByType => "SELECT * FROM [Assessments] WHERE Type = @Type";

    }
}

