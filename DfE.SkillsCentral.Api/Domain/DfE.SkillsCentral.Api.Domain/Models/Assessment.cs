using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Assessment
    {
        public int AssessmentId { get; }
        public string AssessmentType { get; }

        public string AssessmentTitle { get; }

        public string AssessmentSubtitle { get;}

        public string AssessmentIntroduction { get; }

        public int QualificationLevelNumber { get; }

        public int AccessibilityLevelNumber { get; }
    }

}

