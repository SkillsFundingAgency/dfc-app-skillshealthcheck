
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class AnswersQueries
    {
        public static string AllAnswersByQuestionId => "SELECT * FROM [Answers] WHERE QuestionId = @QuestionId";

        public static string AnswerById => "SELECT * FROM [Answers] WHERE Id = @Id";

        public static string CorrectAnswersByAssessment => "SELECT * [Value] FROM[SHC].[dbo].[Answers] as a join[SHC].[dbo].[Questions] as b on a.QuestionID = b.Id " +
            "join[SHC].[dbo].[Assessments] as c on b.AssessmentId =c.Id where IsCorrect = 1 and AssessmentId = @AssessmentId";


    }
}

