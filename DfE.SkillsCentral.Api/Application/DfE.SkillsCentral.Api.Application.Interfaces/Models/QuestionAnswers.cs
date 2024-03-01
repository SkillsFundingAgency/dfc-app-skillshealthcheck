using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfE.SkillsCentral.Api.Application.Interfaces.Models
{
    public class QuestionAnswers
    {
        public Question Question { get; set; }
        public IReadOnlyList<Answer> Answers { get; set; }

    }
}
