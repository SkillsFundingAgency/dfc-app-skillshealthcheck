CREATE TABLE [dbo].[Questions]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[AssessmentId] INT NOT NULL,
	[Number] INT NOT NULL,
	[Text] NVARCHAR(MAX),
	[DataHTML] NVARCHAR(MAX),
	[ImageTitle] NVARCHAR(MAX),
	[ImageCaption] NVARCHAR(MAX),
	[ImageURL] NVARCHAR(MAX), 
    CONSTRAINT [FK_Questions_Assessments] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessments]([Id])
)
