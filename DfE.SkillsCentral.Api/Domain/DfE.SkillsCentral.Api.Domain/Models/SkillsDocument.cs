﻿using DfE.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{

    public class SkillsDocument
    {
        public int? Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public Dictionary<string,string>? DataValueKeys { get; set; } = new();

        public string? ReferenceCode { get; set; }
    }

    
}
