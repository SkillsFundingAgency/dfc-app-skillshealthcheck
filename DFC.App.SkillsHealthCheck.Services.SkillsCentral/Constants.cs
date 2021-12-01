using System;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral
{
    public class Constants
    {
        /// <summary>
        ///
        /// </summary>
        public static class SkillsHealthCheck
        {
            /// <summary>
            /// The default document name
            /// </summary>
            public const string DefaultDocumentName = "Skills Health Check";

            /// <summary>
            /// The document type
            /// </summary>
            public const string DocumentType = "sdt.DR.6";

            /// <summary>
            /// The anonymous user
            /// </summary>
            public const string AnonymousUser = "Anonymous";

            /// <summary>
            /// The qualification property
            /// </summary>
            public const string QualificationProperty = "Qualification.Level";

            /// <summary>
            /// The candidate full name key name
            /// </summary>
            public const string CandidateFullNameKeyName = "CandidateFullName";

            /// <summary>
            /// The field name
            /// </summary>
            public const string FieldName = "Name";

            /// <summary>
            /// The document system identifier name
            /// </summary>
            public const string DocumentSystemIdentifierName = "sdt.id";

            /// <summary>
            /// The document profile identifier name
            /// </summary>
            public const string DocumentProfileIdentifierName = "san.id";

            /// <summary>
            /// The skills document expiry time
            /// </summary>
            public const int SkillsDocumentExpiryTime = 72;

            /// <summary>
            /// The active skills health check document
            /// </summary>
            public const string ActiveSkillsHealthCheckDocument = "ActiveSkillsHealthCheckDocument_{0}";

            /// <summary>
            /// The active skills health check document identifier
            /// </summary>
            public const string ActiveSkillsHealthCheckDocumentId = "ActiveSkillsHealthCheckDocumentId";

            /// <summary>
            /// The active skills health check authenticated document identifier
            /// </summary>
            public const string ActiveSkillsHealthCheckAuthenticatedDocumentId =
                "ActiveSkillsHealthCheckAuthenticatedDocumentId";

            /// <summary>
            /// The question set not started action
            /// </summary>
            public const string QuestionSetNotStartedAction = "Start";

            /// <summary>
            /// The question set started action
            /// </summary>
            public const string QuestionSetStartedAction = "Continue";

            /// <summary>
            /// The question set completed action
            /// </summary>
            public const string QuestionSetCompletedAction = "Completed";

            /// <summary>
            /// The skills assessment complete
            /// </summary>
            public const string SkillsAssessmentComplete = "SkillAreas.Complete";

            /// <summary>
            /// The interests assessment complete
            /// </summary>
            public const string InterestsAssessmentComplete = "Interest.Complete";

            /// <summary>
            /// The personal assessment complete
            /// </summary>
            public const string PersonalAssessmentComplete = "Personal.Complete";

            /// <summary>
            /// The motivation assessment complete
            /// </summary>
            public const string MotivationAssessmentComplete = "Motivation.Complete";

            /// <summary>
            /// The numeric assessment complete
            /// </summary>
            public const string NumericAssessmentComplete = "Numeric.Complete";

            /// <summary>
            /// The verbal assessment complete
            /// </summary>
            public const string VerbalAssessmentComplete = "Verbal.Complete";

            /// <summary>
            /// The checking assessment complete
            /// </summary>
            public const string CheckingAssessmentComplete = "Checking.Complete";

            /// <summary>
            /// The mechanical assessment complete
            /// </summary>
            public const string MechanicalAssessmentComplete = "Mechanical.Complete";

            /// <summary>
            /// The spatial assessment complete
            /// </summary>
            public const string SpatialAssessmentComplete = "Spatial.Complete";

            /// <summary>
            /// The abstract assessment complete
            /// </summary>
            public const string AbstractAssessmentComplete = "Abstract.Complete";

            /// <summary>
            /// The checking type property
            /// </summary>
            public const string CheckingTypeProperty = "Checking.Type";

            /// <summary>
            /// The numeric type property
            /// </summary>
            public const string NumericTypeProperty = "Numeric.Type";

            /// <summary>
            /// The assessment question overview identifier
            /// </summary>
            public const string AssessmentQuestionOverviewId = "AssessmentQuestionOverviewId_{0}";

            // can be moved in sitefinity settings section
            /// <summary>
            /// The skills assessment title
            /// </summary>
            public const string SkillsAssessmentTitle = "Skills";

            /// <summary>
            /// The skills assessment category
            /// </summary>
            public const string SkillsAssessmentCategory = "Personal";

            /// <summary>
            /// The skills assessment description
            /// </summary>
            public const string SkillsAssessmentDescription = @"<p class='govuk-body'>Knowing your strengths could help you decide which jobs you'd enjoy.</p>
                <p class='govuk-body'>This assessment helps you work out how you see yourself and what things you're good at and enjoy doing.  The results will help you identify your strengths and areas for development.</p>";

            /// <summary>
            /// The skills assessment time to complete
            /// </summary>
            public const string SkillsAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The interests assessment title
            /// </summary>
            public const string InterestsAssessmentTitle = "Interests";

            /// <summary>
            /// The interests assessment category
            /// </summary>
            public const string InterestsAssessmentCategory = "Personal";

            /// <summary>
            /// The interests assessment description
            /// </summary>
            public const string InterestsAssessmentDescription =
                @"<p class='govuk-body'>Knowing what you're interested in could help you decide what you want to do for a job.</p>
                <p class='govuk-body'>This assessment helps you work out what areas of work might interest you. The results will give you some career paths to explore.</p>";

            /// <summary>
            /// The interests assessment time to complete
            /// </summary>
            public const string InterestsAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The personal assessment title
            /// </summary>
            public const string PersonalAssessmentTitle = "Personal style";

            /// <summary>
            /// The personal assessment category
            /// </summary>
            public const string PersonalAssessmentCategory = "Personal";

            /// <summary>
            /// The personal assessment description
            /// </summary>
            public const string PersonalAssessmentDescription = @"<p class='govuk-body'>Knowing your personal style could help you decide how you prefer to work, and what sort of roles might suit you. This assessment helps you work out your working style. The results will help you identify what jobs allow you to work comfortably.</p>";

            /// <summary>
            /// The personal assessment time to complete
            /// </summary>
            public const string PersonalAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The motivation assessment title
            /// </summary>
            public const string MotivationAssessmentTitle = "Motivation";

            /// <summary>
            /// The motivatio assessment category
            /// </summary>
            public const string MotivatioAssessmentCategory = "Personal";

            /// <summary>
            /// The motivation assessment description
            /// </summary>
            public const string MotivationAssessmentDescription = @"<p class='govuk-body'>Knowing what you want to get out of work could help you find a job that satisfies you.</p>
                <p class='govuk-body'>This assessment will help you work out what motivates you. The result will help you identify career paths that would help you feel happy at work.</p>";

            /// <summary>
            /// The motivation assessment time to complete
            /// </summary>
            public const string MotivationAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The numeric assessment title
            /// </summary>
            public const string NumericAssessmentTitle = "Working with numbers";

            /// <summary>
            /// The numeric assessment category
            /// </summary>
            public const string NumericAssessmentCategory = "Activity";

            /// <summary>
            /// The numeric assessment description
            /// </summary>
            public const string NumericAssessmentDescription = @"<p class='govuk-body'>Some careers use number skills more than others. This skill is useful if you want to go into:</p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>finance</li>
                  <li>management</li>
                  <li>science</li>
                  <li>research</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how you solve problems using numbers. The results could help you work out if working with numbers suits you.</p>";

            /// <summary>
            /// The numeric assessment time to complete
            /// </summary>
            public const string NumericAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 25 minutes.</p>";

            /// <summary>
            /// The verbal assessment title
            /// </summary>
            public const string VerbalAssessmentTitle = "Verbal reasoning";

            /// <summary>
            /// The verbal assessment category
            /// </summary>
            public const string VerbalAssessmentCategory = "Activity";

            /// <summary>
            /// The verbal assessment description
            /// </summary>
            public const string VerbalAssessmentDescription =
                @"<p class='govuk-body'>Some careers use speaking and writing skills more than others. This skill is useful if you want to go into:</p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>marketing</li>
                  <li>publishing</li>
                  <li>journalism</li>
                  <li>law</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how you solve word problems. The results could help you work out if problem solving using words suits you.</p>";

            /// <summary>
            /// The verbal assessment time to complete
            /// </summary>
            public const string VerbalAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 25 minutes.</p>";

            /// <summary>
            /// The checking assessment title
            /// </summary>
            public const string CheckingAssessmentTitle = "Checking information activity";

            /// <summary>
            /// The checkin assessment category
            /// </summary>
            public const string CheckinAssessmentCategory = "Activity";

            /// <summary>
            /// The checking assessment description
            /// </summary>
            public const string CheckingAssessmentDescription = @"<p class='govuk-body'>Some jobs need you to pay attention to details. This skill is useful if you want to go into:</p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>administration</li>
                  <li>customer service</li>
                  <li>retail</li>
                  <li>sales</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how comfortable you feel doing this sort of work. Your results could help you work out if you want to do this in a job.</p>";

            /// <summary>
            /// The checking assessment time to complete
            /// </summary>
            public const string CheckingAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The mechanical assessment title
            /// </summary>
            public const string MechanicalAssessmentTitle = "Solving mechanical problems";

            /// <summary>
            /// The mechanical assessment category
            /// </summary>
            public const string MechanicalAssessmentCategory = "Activity";

            /// <summary>
            /// The mechanical assessment description
            /// </summary>
            public const string MechanicalAssessmentDescription = @"<p class='govuk-body'>Some jobs need you to solve problems using mechanical knowledge. This skill is useful if you want to go into: </p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>engineering</li>
                  <li>manufacturing</li>
                  <li>construction</li>
                </ul>
                <p class='govuk-body'>The assessment will identify how comfortable you feel solving mechanical problems. Your results could help you work out if you want to do this in a job.</p>";

            /// <summary>
            /// The mechanical assessment time to complete
            /// </summary>
            public const string MechanicalAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The spatial assessment title
            /// </summary>
            public const string SpatialAssessmentTitle = "Working with shapes";

            /// <summary>
            /// The spatial assessment category
            /// </summary>
            public const string SpatialAssessmentCategory = "Activity";

            /// <summary>
            /// The spatial assessment description
            /// </summary>
            public const string SpatialAssessmentDescription = @"<p class='govuk-body'>Some jobs need you to have an understanding of shape and space. This skill is useful if you want to go into: </p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>construction</li>
                  <li>art and design</li>
                  <li>manufacturing</li>
                </ul>
                <p class='govuk-body'>This assessment will identify how comfortable you feel working with shapes and space. Your results could help you work out if this is a skill you would enjoy using at work.</p>";

            /// <summary>
            /// The spatial assessment time to complete
            /// </summary>
            public const string SpatialAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 15 minutes.</p>";

            /// <summary>
            /// The abstract assessment title
            /// </summary>
            public const string AbstractAssessmentTitle = "Solving abstract problems";

            /// <summary>
            /// The abstract assessment category
            /// </summary>
            public const string AbstractAssessmentCategory = "Activity";

            /// <summary>
            /// The abstract assessment description
            /// </summary>
            public const string AbstractAssessmentDescription = @"<p class='govuk-body'>This skill is useful for jobs that involve creative thinking and logical decision-making. This skill is useful if you want to go into: </p>
                <ul class='govuk-list govuk-list--bullet'>
                  <li>IT and digital</li>
                  <li>science</li>
                  <li>medicine</li>
                </ul>
                <p class='govuk-body'>The assessment will identify how comfortable you feel solving abstract problems. Your results could help you work out if you want to do this in a job. </p>";

            /// <summary>
            /// The abstract assessment time to complete
            /// </summary>
            public const string AbstractAssessmentTimeToComplete = "<p class='govuk-body'>This will take around 25 minutes.</p>";

            //Data Values
            /// <summary>
            /// The skills assessment data value
            /// </summary>
            public const string SkillsAssessmentDataValue = "sdt.qs.skills.6.0";

            /// <summary>
            /// The interests assessment data value
            /// </summary>
            public const string InterestsAssessmentDataValue = "sdt.qs.int.6.3";

            /// <summary>
            /// The personal assessment data value
            /// </summary>
            public const string PersonalAssessmentDataValue = "sdt.qs.per.6.3";

            /// <summary>
            /// The motivation assessment data value
            /// </summary>
            public const string MotivationAssessmentDataValue = "sdt.qs.mot.6.3";

            /// <summary>
            /// The numeric assessment data value
            /// </summary>
            public const string NumericAssessmentDataValue = "sdt.qs.num.6.3";

            /// <summary>
            /// The verbal assessment data value
            /// </summary>
            public const string VerbalAssessmentDataValue = "sdt.qs.verb.6.3";

            /// <summary>
            /// The checking assessment data value
            /// </summary>
            public const string CheckingAssessmentDataValue = "sdt.qs.check.6.0";

            /// <summary>
            /// The mechanical assessment data value
            /// </summary>
            public const string MechanicalAssessmentDataValue = "sdt.qs.mech.6.0";

            /// <summary>
            /// The spatial assessment data value
            /// </summary>
            public const string SpatialAssessmentDataValue = "sdt.qs.spl.6.0";

            /// <summary>
            /// The abstract assessment data value
            /// </summary>
            public const string AbstractAssessmentDataValue = "sdt.qs.abt.6.0";

            /// <summary>
            /// The job family title
            /// </summary>
            public const string JobFamilyTitle = "SkillAreas.ExcludedJobFamilies{0}";

            /// <summary>
            /// The how long to complete assessment
            /// </summary>
            public const string HowLongToCompleteAssessment =
                "How long did you spend answering the questions? Do not include any time spent on breaks or interruptions.";

            /// <summary>
            /// The how easy to complete assessment
            /// </summary>
            public const string HowEasyToCompleteAssessment = "Overall, how did you find this assessment?";

            /// <summary>
            /// The how enjoyable to complete assessment
            /// </summary>
            public const string HowEnjoyableToCompleteAssessment =
                "Overall, how much did you enjoy completing the activity?";

        }
    }
}
