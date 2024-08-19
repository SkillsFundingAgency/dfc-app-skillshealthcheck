CREATE TABLE [dbo].[SkillsDocuments]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[CreatedAt] DATETIME,
	[CreatedBy] NVARCHAR(MAX),
	[UpdatedAt] DATETIME,
	[UpdatedBy] NVARCHAR(MAX),
	[DataValueKeys] NVARCHAR(MAX) NULL, 
    [ReferenceCode] NVARCHAR(MAX) NOT NULL, 
)
