CREATE TABLE [dbo].[Answers]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[QuestionID] INT NOT NULL,
	[Value] NVARCHAR(MAX),
	[Text] NVARCHAR(MAX),
	[ImageTitle] NVARCHAR(MAX),
	[ImageCaption] NVARCHAR(MAX),
	[ImageURL] NVARCHAR(MAX),  
	[IsCorrect] INT NULL, 
    CONSTRAINT [FK_Answers_Questions] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([Id])

)
