using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int? AssessmentId { get; set; }

        public string? Title { get; set; }

        public int Number { get; set; }

        public string? Text { get; set; }

        public string? DataHTML { get; set; }

        public string? ImageTitle { get; set; }

        public string? ImageCaption { get; set; }

        public string? ImageURL { get; set; }

    }
}
