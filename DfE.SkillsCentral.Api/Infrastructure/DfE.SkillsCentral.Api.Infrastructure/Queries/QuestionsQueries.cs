
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class QuestionsQueries
    {
        public static string AllQuestionsByAssessmentId => "SELECT * FROM [Questions] (NOLOCK) WHERE [AssessmentId] = @AssessmentId";

        public static string QuestionById => "SELECT * FROM [Questions] (NOLOCK) WHERE [QuestionId] = @QuestionId";
    }
}

