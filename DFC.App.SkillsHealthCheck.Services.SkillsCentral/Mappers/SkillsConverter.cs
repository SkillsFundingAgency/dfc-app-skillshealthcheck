using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using SkillsDocumentsService;
using System.Collections.Generic;
using Accessibility = DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums.Accessibility;
using Answer = DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models.Answer;
using Level = DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums.Level;
using Question = DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models.Question;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Mappers
{
    /// <summary>
    /// Skills Converter 
    /// </summary>
    internal static class SkillsConverter
    {
        /// <summary>
        /// Gets the type of the API assessment.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        internal static AssessmentType GetApiAssessmentType(this Enums.AssessmentType assessmentType)
        {
            switch (assessmentType)
            {
                case Enums.AssessmentType.Abstract:
                    return AssessmentType.Abstract;
                case Enums.AssessmentType.Checking:
                    return AssessmentType.Checking;
                case Enums.AssessmentType.Interest:
                    return AssessmentType.Interests;
                case Enums.AssessmentType.Mechanical:
                    return AssessmentType.Mechanical;
                case Enums.AssessmentType.Motivation:
                    return AssessmentType.Motivation;
                case Enums.AssessmentType.Numeric:
                    return AssessmentType.Numerical;
                case Enums.AssessmentType.Personal:
                    return AssessmentType.Personal;
                case Enums.AssessmentType.SkillAreas:
                    return AssessmentType.SkillAreas;
                case Enums.AssessmentType.Spatial:
                    return AssessmentType.Spatial;
                case Enums.AssessmentType.Verbal:
                    return AssessmentType.Verbal;
                default:
                    return AssessmentType.SkillAreas;
            }
        }

        /// <summary>
        /// Gets the type of the model assessment.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        internal static Enums.AssessmentType GetModelAssessmentType(this AssessmentType assessmentType)
        {
            switch (assessmentType)
            {
                case AssessmentType.Abstract:
                    return Enums.AssessmentType.Abstract;
                case AssessmentType.Checking:
                    return Enums.AssessmentType.Checking;
                case AssessmentType.Interests:
                    return Enums.AssessmentType.Interest;
                case AssessmentType.Mechanical:
                    return Enums.AssessmentType.Mechanical;
                case AssessmentType.Motivation:
                    return Enums.AssessmentType.Motivation;
                case AssessmentType.Numerical:
                    return Enums.AssessmentType.Numeric;
                case AssessmentType.Personal:
                    return Enums.AssessmentType.Personal;
                case AssessmentType.SkillAreas:
                    return Enums.AssessmentType.SkillAreas;
                case AssessmentType.Spatial:
                    return Enums.AssessmentType.Spatial;
                case AssessmentType.Verbal:
                    return Enums.AssessmentType.Verbal;
                default:
                    return Enums.AssessmentType.SkillAreas;
            }
        }

        /// <summary>
        /// Converts to model question.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        internal static Question ConvertToModelQuestion(this SkillsDocumentsService.Question question)
        {
            return new Question
            {
                Level = FromSet<Level>.Get(question.Level.ToString()),
                Accessibility = FromSet<Accessibility>.Get(question.Accessibility.ToString()),
                AnswerHeadings = question.AnswerHeadings,
                AssessmentTitle = question.AssessmentTitle,
                AssessmentType = question.AssessmentType.GetModelAssessmentType(),
                ImageCaption = question.ImageCaption,
                ImageTitle = question.ImageTitle,
                ImageUrl = question.ImageUrl.GetModelImageUrl(question.AssessmentType.GetModelAssessmentType()),
                NextQuestionNumber =
                    question.TotalQuestionNumber == question.QuestionNumber ? -1 : question.QuestionNumber + 1,
                PossibleResponses = question.PossibleResponses.GetModelAnswers(question.AssessmentType.GetModelAssessmentType()),
                QuestionData = question.QuestionData,
                QuestionNumber = question.QuestionNumber,
                QuestionText = question.QuestionText,
                QuestionTitle = question.QuestionTitle,
                TotalQuestionNumber = question.TotalQuestionNumber,
                SubTitle = question.SubTitle,
                IntroductionText = question.IntroductionText


            };
        }

        /// <summary>
        /// Gets the model answers.
        /// </summary>
        /// <param name="answers">The answers.</param>
        /// <param name="assessmentType"></param>
        /// <returns></returns>
        private static List<Answer> GetModelAnswers(this IEnumerable<SkillsDocumentsService.Answer> answers, Enums.AssessmentType assessmentType)
        {
            var result = new List<Answer>();

            var assessmentTypeString = assessmentType == Enums.AssessmentType.Numeric
                    ? "Numerical"
                    : assessmentType.ToString();

            foreach (var answer in answers)
            {
                result.Add(new Answer
                {
                    ImageSource =
                        string.IsNullOrWhiteSpace(answer.ImageSource)
                            ? string.Empty
                            : $"{ConfigHelper.ImageStorageUrl}{assessmentTypeString}/{answer.ImageSource}",
                    Text = answer.Text,
                    Value = answer.Value
                });
            }

            return result;
        }

        private static string GetModelImageUrl(this string imageUrl, Enums.AssessmentType assessmentType)
        {

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return string.Empty;
            }

            var assessmentTypeString = assessmentType == Enums.AssessmentType.Numeric
                    ? "Numerical"
                    : assessmentType.ToString();

            return $"{ConfigHelper.ImageStorageUrl}{assessmentTypeString}/{imageUrl}";
        }
    }
}
