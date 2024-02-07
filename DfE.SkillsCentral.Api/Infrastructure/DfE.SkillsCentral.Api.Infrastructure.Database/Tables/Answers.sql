CREATE TABLE [dbo].[tblAnswers]
(
	[AnswerId] INT NOT NULL PRIMARY KEY,
	[QuestionID] INT NULL,
	[AnswerValue] TEXT, /* is this how we know the correct answer? if not, what is? */
	[AnswerText] TEXT,
	[ImageSource] TEXT, 
    [AssessmentId] INT NULL
)
