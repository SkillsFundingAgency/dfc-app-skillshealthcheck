using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Question
    {
        public int Id { get; }

        public int AssessmentId { get; }

        public string? Title { get; }

        public int Number { get; }

        public string? Text { get; }

        public string? DataHTML { get; }

        public string? ImageTitle { get; }

        public string? ImageCaption { get; }

        public string? ImageURL { get; }

    }
}
