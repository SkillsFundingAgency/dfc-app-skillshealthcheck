namespace DFC.App.SkillsHealthCheck
{
    public static class Constants
    {
        public static class Assessments
        {
            public static class Skills
            {
                public const string Action = "Start skills assessment";

                public const string Title = "Skills";

                public const string Category = "Personal";

                public const string Description = @"<p class='govuk-body'>Knowing your strengths could help you decide which jobs you'd enjoy.</p>
                <p class='govuk-body'>This assessment helps you work out how you see yourself and what things you're good at and enjoy doing.  The results will help you identify your strengths and areas for development.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Interests
            {
                public const string Action = "Start interests assessment";

                public const string Title = "Interests";

                public const string Category = "Personal";

                public const string Description = @"<p class='govuk-body'>Knowing what you're interested in could help you decide what you want to do for a job.</p>
                <p class='govuk-body'>This assessment helps you work out what areas of work might interest you. The results will give you some career paths to explore.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Personal
            {
                public const string Action = "Start personal style assessment";

                public const string Title = "Personal style";

                public const string Category = "Personal";

                public const string Description = @"<p class='govuk-body'>Knowing your personal style could help you decide how you prefer to work, and what sort of roles might suit you. This assessment helps you work out your working style. The results will help you identify what jobs allow you to work comfortably.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Motivation
            {
                public const string Action = "Start motivation assessment";

                public const string Title = "Motivation";

                public const string Category = "Personal";

                public const string Description = @"<p class='govuk-body'>Knowing what you want to get out of work could help you find a job that satisfies you.</p>
                <p class='govuk-body'>This assessment will help you work out what motivates you. The result will help you identify career paths that would help you feel happy at work.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Numeric
            {
                public const string Action = "Start numerical assessment";

                public const string Title = "Working with numbers";

                public const string Category = "Activity";

                public const string Description = @"<p class='govuk-body'>Some careers use number skills more than others. This skill is useful if you want to go into:</p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>finance</li>
                  <li>management</li>
                  <li>science</li>
                  <li>research</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how you solve problems using numbers. The results could help you work out if working with numbers suits you.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 25 minutes.</p>";
            }

            public static class Verbal
            {
                public const string Action = "Start verbal reasoning assessment";

                public const string Title = "Verbal reasoning";

                public const string Category = "Activity";

                public const string Description = @"<p class='govuk-body'>Some careers use speaking and writing skills more than others. This skill is useful if you want to go into:</p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>marketing</li>
                  <li>publishing</li>
                  <li>journalism</li>
                  <li>law</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how you solve word problems. The results could help you work out if problem solving using words suits you.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 25 minutes.</p>";
            }

            public static class Checking
            {
                public const string Action = "Start clerical assessment";

                public const string Title = "Checking information activity";

                public const string Category = "Activity";

                public const string Description = @"<p class='govuk-body'>Some jobs need you to pay attention to details. This skill is useful if you want to go into:</p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>administration</li>
                  <li>customer service</li>
                  <li>retail</li>
                  <li>sales</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how comfortable you feel doing this sort of work. Your results could help you work out if you want to do this in a job.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Mechanical
            {
                public const string Action = "Start mechanical assessment";

                public const string Title = "Solving mechanical problems";

                public const string Category = "Activity";

                public const string Description = @"<p class='govuk-body'>Some jobs need you to solve problems using mechanical knowledge. This skill is useful if you want to go into: </p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>engineering</li>
                  <li>manufacturing</li>
                  <li>construction</li>
                </ul>
                <p class='govuk-body'>The assessment will identify how comfortable you feel solving mechanical problems. Your results could help you work out if you want to do this in a job.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Spatial
            {
                public const string Action = "Start spatial assessment";

                public const string Title = "Working with shapes";

                public const string Category = "Activity";

                public const string Description = @"<p class='govuk-body'>Some jobs need you to have an understanding of shape and space. This skill is useful if you want to go into: </p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>construction</li>
                  <li>art and design</li>
                  <li>manufacturing</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how comfortable you feel working with shapes and space. Your results could help you work out if this is a skill you would enjoy using at work.</p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";
            }

            public static class Abstract
            {
                public const string Action = "Start abstract assessment";

                public const string Title = "Solving abstract problems";

                public const string Category = "Activity";

                public const string Description = @"<p class='govuk-body'>This skill is useful for jobs that involve creative thinking and logical decision-making. This skill is useful if you want to go into: </p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>IT and digital</li>
                  <li>science</li>
                  <li>medicine</li>
                </ul>
                <p class='govuk-body'>The assessment will identify how comfortable you feel solving abstract problems. Your results could help you work out if you want to do this in a job. </p>";

                public const string TimeToComplete = "<p class='govuk-body'>This will take around 25 minutes.</p>";
            }
        }

        public static class SkillsHealthCheck
        {
            public const string DefaultDocumentName = "Skills Health Check";

            public const string DocumentType = "sdt.DR.6";

            public const string AnonymousUser = "Anonymous";

            public const string QualificationProperty = "Qualification.Level";

            public const string CandidateFullNameKeyName = "CandidateFullName";

            public const string FieldName = "Name";

            public const string DocumentSystemIdentifierName = "sdt.id";

            public const string DocumentProfileIdentifierName = "san.id";

            public const int SkillsDocumentExpiryTime = 72;

            public const string ActiveSkillsHealthCheckDocument = "ActiveSkillsHealthCheckDocument_{0}";

            public const string ActiveSkillsHealthCheckDocumentId = "ActiveSkillsHealthCheckDocumentId";

            public const string ActiveSkillsHealthCheckAuthenticatedDocumentId =
                "ActiveSkillsHealthCheckAuthenticatedDocumentId";

            public const string QuestionSetNotStartedAction = "Start";

            public const string QuestionSetStartedAction = "Continue";

            public const string QuestionSetCompletedAction = "Completed";

            public const string SkillsAssessmentComplete = "SkillAreas.Complete";

            public const string InterestsAssessmentComplete = "Interest.Complete";

            public const string PersonalAssessmentComplete = "Personal.Complete";

            public const string MotivationAssessmentComplete = "Motivation.Complete";

            public const string NumericAssessmentComplete = "Numeric.Complete";

            public const string VerbalAssessmentComplete = "Verbal.Complete";

            public const string CheckingAssessmentComplete = "Checking.Complete";

            public const string MechanicalAssessmentComplete = "Mechanical.Complete";

            public const string SpatialAssessmentComplete = "Spatial.Complete";

            public const string AbstractAssessmentComplete = "Abstract.Complete";

            public const string CheckingTypeProperty = "Checking.Type";

            public const string NumericTypeProperty = "Numeric.Type";

            public const string AssessmentQuestionOverviewId = "AssessmentQuestionOverviewId_{0}";

            public const string SkillsAssessmentDataValue = "sdt.qs.skills.6.0";

            public const string InterestsAssessmentDataValue = "sdt.qs.int.6.3";

            public const string PersonalAssessmentDataValue = "sdt.qs.per.6.3";

            public const string MotivationAssessmentDataValue = "sdt.qs.mot.6.3";

            public const string NumericAssessmentDataValue = "sdt.qs.num.6.3";

            public const string VerbalAssessmentDataValue = "sdt.qs.verb.6.3";

            public const string CheckingAssessmentDataValue = "sdt.qs.check.6.0";

            public const string MechanicalAssessmentDataValue = "sdt.qs.mech.6.0";

            public const string SpatialAssessmentDataValue = "sdt.qs.spl.6.0";

            public const string AbstractAssessmentDataValue = "sdt.qs.abt.6.0";

            public const string JobFamilyTitle = "SkillAreas.ExcludedJobFamilies{0}";

            public const string HowLongToCompleteAssessment =
                "How long did you spend answering the questions? Do not include any time spent on breaks or interruptions.";

            public const string HowEasyToCompleteAssessment = "Overall, how did you find this assessment?";

            public const string HowEnjoyableToCompleteAssessment =
                "Overall, how much did you enjoy completing the activity?";
        }
    }
}
