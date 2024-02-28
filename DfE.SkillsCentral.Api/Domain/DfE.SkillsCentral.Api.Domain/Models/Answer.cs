using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Answer
    {
        public int Id { get; }
        public int QuestionID { get; }

        public string? Value { get; }

        public int? IsCorrect { get; }

        public string? Text { get; }

        public string? ImageTitle { get; }

        public string? ImageCaption { get; }

        public string? ImageURL { get; }


    }
}
