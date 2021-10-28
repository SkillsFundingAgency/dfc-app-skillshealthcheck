using DFC.App.SkillsHealthCheck.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace DFC.App.SkillsHealthCheck.Models
{
    public class SHCWidgetSettings
    {
        // Start
        [DisplayName("Skills Assessment - Add Start Description")]
        public static string StartSkillsDescription { get; set; } = "Start skills assessment";

        [DisplayName("Interests Assessment - Add Start Description")]
        public static string StartInterestsDescription { get; set; } = "Start interests assessment";

        [DisplayName("Personal Style Assessment - Add Start Description")]
        public static string StartPersonalStyleDescription { get; set; } = "Start personal style assessment";

        [DisplayName("Numeric - Add Start Assessment Description")]
        public static string StartNumericalDescription { get; set; } = "Start numerical assessment";

        [DisplayName("Motivation Assessment - Add Start Description")]
        public static string StartMotivationDescription { get; set; } = "Start motivation assessment";

        [DisplayName("Checking Information Assessment - Add Start Description")]
        public static string StartCheckingInformationDescription { get; set; } = "Start clerical assessment";

        [DisplayName("Mechanical Assessment - Add Start Description")]
        public static string StartMechanicalDescription { get; set; } = "Start mechanical assessment";

        [DisplayName("Spatial Awareness Assessment - Add Start Description")]
        public static string StartSpatialAwarenessDescription { get; set; } = "Start spatial assessment";

        [DisplayName("Abstract Awareness Assessment - Add Start Description")]
        public static string StartAbstractAwarenessDescription { get; set; } = "Start abstract assessment";

        [DisplayName("Verbal Assessment - Add Start Description")]
        public static string StartVerbalAssessment { get; set; } = "Start verbal reasoning assessment";

        //Continue
        [DisplayName("Skills Assessment - Add Continue Description")]
        public static string ContinueSkillsDescription { get; set; } = "Continue skills assessment";

        [DisplayName("Interests Assessment - Add Continue Description")]
        public static string ContinueInterestsDescription { get; set; } = "Continue interests assessment";

        [DisplayName("PersonalStyle Assessment - Add Continue Description")]
        public static string ContinuePersonalStyleDescription { get; set; } = "Continue personal style assessment";

        [DisplayName("Numeric Assessment - Add Continue Description")]
        public static string ContinueNumericalDescription { get; set; } = "Continue numerical assessment";

        [DisplayName("Motivation Assessment - Add Continue Description")]
        public static string ContinueMotivationDescription { get; set; } = "Continue motivation assessment";

        [DisplayName("Checking Information Assessment - Add Continue Description")]
        public static string ContinueCheckingInformationDescription { get; set; } = "Continue clerical assessment";

        [DisplayName("Mechanical Assessment - Add Continue Description")]
        public static string ContinueMechanicalDescription { get; set; } = "Continue mechanical assessment";

        [DisplayName("Spatial Awareness Assessment - Add Continue Description")]
        public static string ContinueSpatialAwarenessDescription { get; set; } = "Continue spatial assessment";

        [DisplayName("Abstract Awareness Assessment - Add Continue Description")]
        public static string ContinueAbstractAwarenessDescription { get; set; } = "Continue abstract assessment";

        [DisplayName("Verbal Assessment - Add Continue Description")]
        public static string ContinueVerbalAssessment { get; set; } = "Continue verbal reasoning assessment";

        //Completed
        [DisplayName("Skills Assessment - Add Continue Description")]
        public static string CompletedSkillsDescription { get; set; } = "View report of skills assessment";

        [DisplayName("Interests Assessment - Add Continue Description")]
        public static string CompletedInterestsDescription { get; set; } = "View report of interests assessment";

        [DisplayName("PersonalStyle Assessment - Add Continue Description")]
        public static string CompletedPersonalStyleDescription { get; set; } = "View report of personal style assessment";

        [DisplayName("Numeric Assessment - Add Completed Description")]
        public static string CompletedNumericalDescription { get; set; } = "View report of numerical assessment";

        [DisplayName("Motivation Assessment - Add Completed Description")]
        public static string CompletedMotivationDescription { get; set; } = "View report of motivation assessment";

        [DisplayName("Checking Information Assessment - Add Completed Description")]
        public static string CompletedCheckingInformationDescription { get; set; } = "View report of clerical assessment";

        [DisplayName("Mechanical Assessment - Add Completed Description")]
        public static string CompletedMechanicalDescription { get; set; } = "View report of mechanical assessment";

        [DisplayName("Spatial Awareness Assessment - Add Completed Description")]
        public static string CompletedSpatialAwarenessDescription { get; set; } = "View report of spatial assessment";

        [DisplayName("Abstract Awareness Assessment - Add Completed Description")]
        public static string CompletedAbstractAwarenessDescription { get; set; } = "View report of abstract assessment";

        [DisplayName("Verbal Assessment - Add Completed Description")]
        public static string CompletedVerbalAssessment { get; set; } = "View report of verbal reasoning assessment";

        public static List<SHCWidgetSettings> SHCWidgetSettingsList = new List<SHCWidgetSettings>
        {
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Verbal, StartLinkText = StartVerbalAssessment, ContinueLinkText = ContinueVerbalAssessment, CompleteLinkText = CompletedVerbalAssessment },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.SkillAreas, StartLinkText = StartSkillsDescription, ContinueLinkText = ContinueSkillsDescription, CompleteLinkText = CompletedSkillsDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Interest, StartLinkText = StartInterestsDescription, ContinueLinkText = ContinueInterestsDescription, CompleteLinkText = CompletedInterestsDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Personal, StartLinkText = StartPersonalStyleDescription, ContinueLinkText = ContinuePersonalStyleDescription, CompleteLinkText = CompletedPersonalStyleDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Numeric, StartLinkText = StartNumericalDescription, ContinueLinkText = ContinueNumericalDescription, CompleteLinkText = CompletedNumericalDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Motivation, StartLinkText = StartMotivationDescription, ContinueLinkText = ContinueMotivationDescription, CompleteLinkText = CompletedMotivationDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Checking, StartLinkText = StartCheckingInformationDescription, ContinueLinkText = ContinueCheckingInformationDescription, CompleteLinkText = CompletedCheckingInformationDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Mechanical, StartLinkText = StartMechanicalDescription, ContinueLinkText = ContinueMechanicalDescription, CompleteLinkText = CompletedMechanicalDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Spatial, StartLinkText = StartSpatialAwarenessDescription, ContinueLinkText = ContinueSpatialAwarenessDescription, CompleteLinkText = CompletedSpatialAwarenessDescription },
                new SHCWidgetSettings { SHCAssessmentType = AssessmentType.Abstract, StartLinkText = StartAbstractAwarenessDescription, ContinueLinkText = ContinueAbstractAwarenessDescription, CompleteLinkText = CompletedAbstractAwarenessDescription },
        };

        public AssessmentType SHCAssessmentType { get; set; }

        public string StartLinkText { get; set; }

        public string ContinueLinkText { get; set; }

        public string CompleteLinkText { get; set; }
    }
}
