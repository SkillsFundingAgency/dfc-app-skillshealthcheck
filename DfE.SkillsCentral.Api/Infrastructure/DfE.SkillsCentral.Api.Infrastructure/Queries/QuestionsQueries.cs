
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class QuestionsQueries
    {
        public static string AllQuestionsByAssessmentId => "SELECT * FROM [Questions] WHERE AssessmentId = @AssessmentId";

        public static string QuestionById => "SELECT * FROM [Questions] WHERE Id = @Id";

        public static string QuestionByNumberAndAssessmentId => "SELECT * FROM [Questions] WHERE Number = @Number AND AssessmentId = @AssessmentId";
    }
}

