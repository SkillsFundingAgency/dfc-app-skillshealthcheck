CREATE TABLE [dbo].[tblQuestions]
(
	[QuestionId] INT NOT NULL PRIMARY KEY,
	[AssessmentId] INT NOT NULL,
	[QuestionTitle] TEXT,
	[QuestionNumber] INT,
	[QuestionText] TEXT,
	[QuestionDataHTML] TEXT,
	[ImageTitle] TEXT,
	[ImageCaption] TEXT,
	[ImageURL] TEXT
)
