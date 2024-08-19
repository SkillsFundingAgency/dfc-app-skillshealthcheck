using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class AssessmentQuestions
    {
        public Assessment Assessment { get; set; }
        public List<QuestionAnswers> Questions { get; set; } = new List<QuestionAnswers>();
    }
}
