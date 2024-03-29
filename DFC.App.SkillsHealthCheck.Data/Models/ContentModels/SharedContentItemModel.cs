﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using DFC.Compui.Cosmos.Contracts;

namespace DFC.App.SkillsHealthCheck.Data.Models.ContentModels
{
    [ExcludeFromCodeCoverage]
    public class SharedContentItemModel : DocumentModel
    {
        public const string DefaultPartitionKey = "shared-content";

        public override string? PartitionKey { get; set; } = DefaultPartitionKey;

        [Required]
        public string? Title { get; set; }

        [Required]
        public Uri? Url { get; set; }

        [Required]
        public string? Content { get; set; }

        public DateTime LastReviewed { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastCached { get; set; } = DateTime.UtcNow;
    }
}
