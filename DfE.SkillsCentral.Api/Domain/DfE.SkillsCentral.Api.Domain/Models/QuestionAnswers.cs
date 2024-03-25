using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class QuestionAnswers
    {
        public Question Question { get; set; }
        public IReadOnlyList<Answer>? Answers { get; set; }

    }
}
