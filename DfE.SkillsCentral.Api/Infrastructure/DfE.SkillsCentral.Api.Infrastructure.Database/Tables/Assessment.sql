CREATE TABLE [dbo].[Assessments]
(
	[AssessmentId] INT NOT NULL PRIMARY KEY,
	[AssessmentType] TEXT NOT NULL, /* what is this? */
	[AssessmentTitle] TEXT NOT NULL,
	[AssessmentSubtitle] TEXT NOT NULL,
	[AssessmentIntroduction] TEXT NOT NULL,
	[QualificationLevelNumber] INT NOT NULL,
	[AccessibilityLevelNumber] INT NOT NULL,
)
