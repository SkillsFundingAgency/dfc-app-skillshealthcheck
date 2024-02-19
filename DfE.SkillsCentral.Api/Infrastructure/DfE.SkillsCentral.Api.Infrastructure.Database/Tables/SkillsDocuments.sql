CREATE TABLE [dbo].[SkillsDocuments]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[CreatedAt] DATETIME,
	[CreatedBy] NVARCHAR(MAX),
	[UpdatedAt] DATETIME,
	[UpdatedBy] NVARCHAR(MAX),
	[DataValueKeys] XML, 
    [ReferenceCode] NVARCHAR(MAX) NOT NULL, 
)
