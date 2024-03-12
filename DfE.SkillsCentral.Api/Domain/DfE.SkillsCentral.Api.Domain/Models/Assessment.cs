using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        public string? Title { get; set; }

        public string? Subtitle { get; set; }

        public string? Introduction { get; set; }

    }

}

