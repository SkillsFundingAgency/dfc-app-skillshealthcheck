using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class AssessmentQuestionViewModel
    {
        [Required(ErrorMessage = "Choose an answer")]
        public string QuestionAnswer { get; set; }

        public DFC.SkillsCentral.Api.Domain.Models.QuestionAnswers? QuestionAnswers { get; set; }

        public int QuestionNumber { get; set; }

        public int ActualTotalQuestions { get; set; }

        public IEnumerable<Image>? QuestionImages { get; set; } = new List<Image>();

        public SkillsHealthCheckValidationErrors? ValidationErrors { get; set; }
        public string? AssessmentTitle { get; set; }

        public string? AssessmentSubtitle { get; set; }

        public string? IntroductionText { get; set; }


        public AssessmentType? AssessmentType { get; set; }

        public string? ViewName { get; set; }
    }
}
