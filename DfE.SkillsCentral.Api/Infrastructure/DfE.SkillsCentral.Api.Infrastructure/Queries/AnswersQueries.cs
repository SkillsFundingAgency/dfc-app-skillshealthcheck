
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class AssessmentsQueries
    {
        public static string AllAssessments => "SELECT * FROM [Assessments] (NOLOCK)";

        public static string AssessmentById => "SELECT * FROM [Assessments] (NOLOCK) WHERE [AssessmentId] = @AssessmentId";
    }
}

