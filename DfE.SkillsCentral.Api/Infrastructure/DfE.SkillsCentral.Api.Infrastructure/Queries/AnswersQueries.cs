
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class AnswersQueries
    {
        public static string AllAnswersByQuestionId => "SELECT * FROM [Answers] (NOLOCK) WHERE [QuestionId] = @QuestionId";

        public static string AllAnswersByAssessmentId => "SELECT * FROM [Answers] (NOLOCK) WHERE [AssessmentId] = @AssessmentId";

        public static string AnswerById => "SELECT * FROM [Answers] (NOLOCK) WHERE [AnswersId] = @AnswersId";


    }
}

