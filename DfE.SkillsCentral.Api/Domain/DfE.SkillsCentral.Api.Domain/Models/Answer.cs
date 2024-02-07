using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Answer
    {
        public int AnswerId { get; }
        public int QuestionID { get; }

        public string AnswerValue { get; }

        public string AnswerText { get; }

        public string ImageSource { get; }

        public int AssessmentId { get; }
    }
}
