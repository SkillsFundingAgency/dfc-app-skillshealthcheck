using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Question
    {
        public int QuestionId { get; }

        public int AssessmentId { get; }

        public string QuestionTitle { get; }

        public int QuestionNumber { get; }

        public string QuestionText { get; }

        public string QuestionDataHTML { get; }

        public string ImageTitle { get; }

        public string ImageCaption { get; }

        public string ImageURL { get; }

    }
}
