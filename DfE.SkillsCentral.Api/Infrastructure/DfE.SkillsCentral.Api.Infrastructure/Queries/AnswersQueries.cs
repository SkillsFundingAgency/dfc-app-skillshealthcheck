
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class AnswersQueries
    {
        public static string AllAnswersByQuestionId => "SELECT * FROM [Answers] WHERE QuestionId = @QuestionId";

        public static string AnswerById => "SELECT * FROM [Answers] WHERE Id = @Id";


    }
}

