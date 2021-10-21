using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    public class AssessmentQuestionViewModel
    {
        [Required(ErrorMessage = "Choose an answer")]
        public string QuestionAnswer { get; set; }

        public Services.SkillsCentral.Models.Question Question { get; set; }

        public string SaveAnswerError { get; set; }

        public int QuestionNumber { get; set; }

        public int ActualTotalQuestions { get; set; }

        public IEnumerable<Image> QuestionImages { get; set; }

        // Used to pass the error messages to the client-side validation script skillsHealthCheck.js
        public SkillsHealthCheckValidationErrors ValidationErrors { get; set; }
        //Used to store the content from the sitefinity content block when fetched the title "Speak to an Adviser".
        //which is passed to the view to render the page . On the right hand side of the page
        //Speak to an Adviser is displayed.

        public string ViewName { get; set; }
    }
}
