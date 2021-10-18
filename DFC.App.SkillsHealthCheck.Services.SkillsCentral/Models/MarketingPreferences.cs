﻿namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    public class MarketingPreferences
    {
        public bool OptOutOfMarketing { get; set; }
        public bool MarketingOptIn
        {
            get
            {
                return !OptOutOfMarketing;
            }
            set
            {
                OptOutOfMarketing = !value;
            }
        }

        public bool OptOutOfMarketResearch { get; set; }
        public bool MarketResearchOptIn
        {
            get
            {
                return !OptOutOfMarketResearch;
            }
            set
            {
                OptOutOfMarketResearch = !value;
            }
        }
    }
}
