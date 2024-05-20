using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionID { get; set; }

        public string? Value { get; set; }

        public int? IsCorrect { get; set; }

        public string? Text { get; set; }

        public string? ImageTitle { get; set; }

        public string? ImageCaption { get; set; }

        public string? ImageURL { get; set; }


    }
}
