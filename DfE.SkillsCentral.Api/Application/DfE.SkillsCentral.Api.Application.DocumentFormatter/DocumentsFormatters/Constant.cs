// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    /// <summary>
    /// Common constants used across skills health check report
    /// </summary>
    public class Constant
    {
        #region | Numerical Constants |
        public const string NumericalQuestionTypeName = "sdt.qs.sr_num.";
        public const string NumericalQuestionSetB = "Numeric_CorrectAnswers_SetB";
        public const string NumericalQuestionSetA = "Numeric_CorrectAnswers_SetA";
        public const string XmlShowNumericalElement = "ShowNumeric";
        public const string XmlNumericalRootElement = "Numeric";
        public const string XmlTimingElement = "Timing";
        public const string XmlEaseElement = "Ease";
        public const string XmlQuestionsAttemptedElement = "QuestionsAttempted";
        public const string XmlTotalQuestionsElement = "TotalQuestions";
        public const string XmlQuestionsCorrectElement = "QuestionsCorrect";
        public const string XmlStyleElement = "Style";
        public const string XmlAccuracyElement = "Accuracy";
        public const string XmlOverallPotentialElement = "OverallPotential";
        #endregion

        #region | Checking Information |
        public const string ChkQuestionTypeName = "sdt.qs.check.6.0";
        public const string ChkQuestionSetB = "Checking_CorrectAnswers_SetB";
        public const string ChkQuestionSetA = "Checking_CorrectAnswers_SetA";
        public const string XmlShowChkElement = "ShowChecking";
        public const string XmlChkRootElement = "Checking";
        public const string XmlChkTimingElement = "Timing";
        public const string XmlChkEaseElement = "Ease";
        public const string XmlChkEnjoymentElement = "Enjoyment";
        public const string XmlChkQuestionsCorrectElement = "QuestionsCorrect";
        public const string XmlChkBandElement = "Band";
        public const string XmlHSTElement = "HST";
        public const string XmlLSTElement = "LST";

        #endregion

        #region | Mechanical Constants |
        public const string MechanicalPhysicalPrincpleQuestionToCheck = "Mechanical_PhysicalPrincpleQuestionToCheck";
        public const string MechanicalMovementOfObjectsQuestionToCheck = "Mechanical_MovementOfObjectsQuestionToCheck";
        public const string MechanicalStructureAndWeightQuestionToCheck = "Mechanical_StructureAndWeightQuestionToCheck";
        public const string MechanicalCorrectAnswer = "Mechanical_CorrectAnswers";
        public const string XmlShowMechanicalElement = "ShowMechanical";
        public const string XmlMechanicalRootElement = "Mechanical";
        public const string XmlQuestionsCorrectBandElement = "QuestionsCorrectBand";
        public const string XmlEnjoymentElement = "Enjoyment";
        public const string XmlPhysicalPrinciplesPercentElement = "PhysicalPrinciplesPercent";
        public const string XmlMovementOfObjectsPercentElement = "MovementOfObjectsPercent";
        public const string XmlStructureAndWeightsPercentElement = "StructureAndWeightsPercent";
        public const string XmlMechanicalTypeIdElement = "MechanicalTypeId";
        #endregion

        #region | Shapes |
        public const string XmlShapesRootElement = "Shapes";
        public const string ShapesCorrectAnswer = "Shapes_CorrectAnswers";
        #endregion

        #region | Abstract |
        public const string XmlAbstractRootElement = "Abstract";
        public const string AbstractCorrectAnswer = "AbstractProblems_CorrectAnswers";
        public const string AbstractReflectionQuestions = "AbstractProblems_ReflectionQuestionToCheck";
        public const string AbstractRotationQuestions = "AbstractProblems_RotationQuestionToCheck";
        public const string AbstractMovementQuestions = "AbstractProblems_MovementQuestionToCheck";
        public const string AbstractRepetitionQuestions = "AbstractProblems_RepetitionQuestionToCheck";
        public const string XmlRelectionPercentElement = "ReflectionPercent";
        public const string XmlRotationPercentElement = "RotationPercent";
        public const string XmlMovementPercentElement = "MovementPercent";
        public const string XmlRepetitionPercentElement = "RepetitionPercent";
        public const string XmlAbstractTypeIdElement = "AbstractTypeId";
        #endregion

        #region | Verbal |
        public const string VerbalCorrectAnswers = "Verbal_CorrectAnswers_SetA";
        public const string XmlVerbalRootElement = "Verbal";
        #endregion

        #region | Common Constants |
        public const char AnswerSeparator = ',';
        public const string AnswerSkippedMarker = "X";
        public const string ShowTagPrefix = "Show";
        public const string ServiceUri = "Show";
        public const string InterestJobFamilesSeparator = ";#";
        #endregion

        #region | Skills Document Fields |
        public const string NumericType = "Numerical.Type";
        public const string NumericAnswers = "Numerical.Answers";
        public const string NumericComplete = "Numerical.Complete";
        public const string NumericEase = "Numerical.Ease";
        public const string NumericTiming = "Numerical.Timing";
        public const string VerbalType = "Verbal.Type";
        public const string VerbalAnswers = "Verbal.Answers";
        public const string VerbalComplete = "Verbal.Complete";
        public const string VerbalEase = "Verbal.Ease";
        public const string VerbalTiming = "Verbal.Timing";
        public const string MotivationType = "Motivation.Type";
        public const string MotivationAnswers = "Motivation.Answers";
        public const string MotivationComplete = "Motivation.Complete";
        public const string PersonalType = "Personal.Type";
        public const string PersonalAnswers = "Personal.Answers";
        public const string PersonalComplete = "Personal.Complete";
        public const string SkillAreasType = "SkillAreas.Type";
        public const string SkillAreasAnswers = "SkillAreas.Answers";
        public const string SkillAreasComplete = "SkillAreas.Complete";
        public const string SkillAreasExcludedJobFamilies1 = "SkillAreas.ExcludedJobFamilies1";
        public const string SkillAreasExcludedJobFamilies2 = "SkillAreas.ExcludedJobFamilies2";
        public const string SkillAreasExcludedJobFamilies3 = "SkillAreas.ExcludedJobFamilies3";
        public const string MechanicalType = "Mechanical.Type";
        public const string MechanicalAnswers = "Mechanical.Answers";
        public const string MechanicalComplete = "Mechanical.Complete";
        public const string MechanicalEase = "Mechanical.Ease";
        public const string MechanicalTiming = "Mechanical.Timing";
        public const string MechanicalEnjoyment = "Mechanical.Enjoyment";
        public const string CheckingType = "Checking.Type";
        public const string CheckingAnswers = "Checking.Answers";
        public const string CheckingComplete = "Checking.Complete";
        public const string CheckingEase = "Checking.Ease";
        public const string CheckingTiming = "Checking.Timing";
        public const string CheckingEnjoyment = "Checking.Enjoyment";
        public const string QualificationLevel = "Qualification.Level";
        public const string InterestType = "Interests.Type";
        public const string InterestAnswers = "Interests.Answers";
        public const string InterestComplete = "Interests.Complete";
        public const string SpatialType = "Spatial.Type";
        public const string SpatialAnswers = "Spatial.Answers";
        public const string SpatialComplete = "Spatial.Complete";
        public const string SpatialEase = "Spatial.Ease";
        public const string SpatialTiming = "Spatial.Timing";
        public const string SpatialEnjoyment = "Spatial.Enjoyment";
        public const string AbstractType = "Abstract.Type";
        public const string AbstractAnswers = "Abstract.Answers";
        public const string AbstractComplete = "Abstract.Complete";
        public const string AbstractEase = "Abstract.Ease";
        public const string AbstractTiming = "Abstract.Timing";
        public const string AbstractEnjoyment = "Abstract.Enjoyment";
        public const string CandidateFullName = "CandidateFullName";

        public const string SkillsDocumentDataValue = "SkillsDocumentDataValue";
        public const string Title = "Title";
        public const string Value = "Value";

        public const string RootStart = "<Root>";
        public const string RootEnd = "</Root>";
        #endregion

        #region | Motivation |
        public const string XmlMotivationRootElement = "Motivation";
        public const string MotivationCategories = "Motivation_Categories";
        public const string MotivationUniqueCategories = "Motivation_UniqueCategories";
        public const string XmlMotivationCategoryElement = "MotivationCategory";
        public const string XmlCategoryElement = "Category";
        public const string XmlScoreElement = "Score";
        public const string XmlScaleScoreElement = "ScaleScore";
        public const string XmlNameElement = "Name";
        public const string XmlDefinitionElement = "Definition";

        public const string MotivationName = "Motivation{0}_Name";
        public const string MotivationDefintion = "Motivation{0}_Definition";

        #endregion

        #region | Interest |
        public const string XmlInterestRootElement = "Interest";
        public const string InterestBandNames = "Interest_BandNames";
        public const string InterestBandDisplayNames = "Interest_Band_DisplayNames";
        public const string InterestResultScoring = "Interest_ResultScoring";
        public const string InterestStrengthOfInterestColors = "Interest_StrengthOfInterestColors";
        public const string InterestMaximumScore = "Interest_MaximumScore";
        public const string InterestAnswerClasification = "Interest_AnswerClasification";
        public const string InterestNames = "Interest_Names";
        public const string InterestInternalNames = "Interest_InternalNames";
        public const string InterestRelatedJobFamilies = "Interest_RelatedJobFamilies";
        public const string InterestWhatThisInvolves = "Interest_WhatThisInvolves";

        public const string XmlInterestCategoryElement = "InterestCategory";
        public const string XmlDisplayNameElement = "DisplayName";
        public const string XmlColorElement = "Colour";
        public const string XmlInterestItemElement = "InterestItem";
        public const string XmlInternalNameElement = "InternalName";
        public const string XmlMaxScoreCountElement = "MaxScoreCount";
        public const string XmlRelatedJobFamiliesElement = "RelatedJobFamilies";
        public const string XmlWhatItInvolvesElement = "WhatItInvolves";
        public const string JobFamiliySplitor = "|";
        public const string XmlJobName = "Job";
        public const string XmlQualificationLevelElement = "QualificationLevel";

        #endregion

        #region | Personal Style |
        public const string PersonalCategories = "PersonalStyle_Categories";
        public const string PersonalUniqueCategories = "PersonalStyle_UniqueCategories";
        public const string PersonalRightNames = "PersonalStyle_RightNames";
        public const string PersonalStrengthLeft = "PersonalStyle_StrengthLeft";
        public const string PersonalStrengthMid = "PersonalStyle_StrengthMid";
        public const string PersonalStrengthRight = "PersonalStyle_StrengthRight";
        public const string PersonalChallengeLeft = "PersonalStyle_ChallengeLeft";
        public const string PersonalChallengeMid = "PersonalStyle_ChallengeMid";
        public const string PersonalChallengeRight = "PersonalStyle_ChallengeRight";
        public const string PersonalDevelopmentLeft = "PersonalStyle_DevelopmentLeft";
        public const string PersonalDevelopmentRight = "PersonalStyle_DevelopmentRight";
        public const string PersonalRelaxedTenseScore = "PersonalStyle_RelaxedTenseScore";
        public const string PersonalTransformedRelaxedTenseScore = "PersonalStyle_TransformedRelaxedTenseScore";
        public const string PersonalNoOfStrengthsToDisplay = "PersonalStyle_NoOfStrengthsToDisplay";
        public const string PersonalNoOfChallengesToDisplay = "PersonalStyle_NoOfChallengesToDisplay";

        public const string XmlPersonalRootElement = "PersonalStyle";
        public const string XmlPersonalListOfStrengthElement = "ListOfStrength";
        public const string XmlPersonalStrengthElement = "Strength";
        public const string XmlPersonalItem = "Name";
        public const string XmlPersonalListOfChallengeElement = "ListOfChallenge";
        public const string XmlPersonalChallengeElement = "Challenge";
        public const string XmlPersonalTip = "Tip";

        #endregion

        #region | Skill Areas |
        public const string SkillsAreaCategories = "SkillsArea_Categories";
        public const string SkillsAreaName = "SkillsArea{0}_Name";
        public const string SkillsAreaDefinition = "SkillsArea{0}_Definition";
        public const string SkillsAreaDescription = "SkillsArea{0}_Description";
        public const string SkillsAreaDevelopmentTip1 = "SkillsArea{0}_DevelopmentTip1";
        public const string SkillsAreaDevelopmentTip2 = "SkillsArea{0}_DevelopmentTip2";
        public const string SkillsAreaStrength = "SkillsArea{0}_Strength";

        public const string XmlSkillsRootElement = "Skills";
        public const string XmlSkillsCategoryElement = "SkillsCategory";
        public const string XmlTitleElement = "Title";
        public const string XmlDescriptionElement = "Description";
        public const string XmlDevTip1Element = "DevTip1";
        public const string XmlDevTip2Element = "DevTip2";
        public const string XmlRankOnResponseElement = "RankOnResponse";
        public const string XmlScoreScaleElement = "ScoreScale";
        public const string XmlStengthElement = "Strength";
        #endregion

        #region | Job Suggestions |
        public const string XmlJobSuggestionsRootElement = "JobSuggestions";
        public const string XmlJobFamilies = "JobFamilies";
        public const string XmlJobSummary = "Summary";
        public const string XmlJobBody = "Body";
        public const string XmlkeyStatement1 = "Statement1";
        public const string XmlkeyStatement2 = "Statement2";
        public const string XmlkeyStatement3 = "Statement3";
        public const string XmlJobFamiliyTask = "Task";
        public const string XmlJobInterestBandName = "InterestBandName";
        public const string XmlJobFamiliyBandMatched = "BandMatched";
        public const string XmlInterestedJobFamilies = "InterestedJobFamilies";
        public const string XmlInterestedJobFamily = "InterestedJobFamily";
        public const string XmlSkillsJobFamilies = "SkillsJobFamilies";
        public const string XmlSkillsJobFamily = "SkillsJobFamily";

        public const string SkillsAndInterestJobFamiliesToDisplay = "JobReport_SkillsAndInterest_JobFamiliesToDisplay";
        public const string Skills_JobFamiliesToDisplay = "JobReport_Skills_JobFamiliesToDisplay";
        public const string Interests_JobFamiliesToDisplay = "JobReport_Interests_JobFamiliesToDisplay";
        public const string InterestBandNamesToGetJobFamilies = "Interest_BandNames_ToGetJobFamilies";

        #endregion
    }
}
