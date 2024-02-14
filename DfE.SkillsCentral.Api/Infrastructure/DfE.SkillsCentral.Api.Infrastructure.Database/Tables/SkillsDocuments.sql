CREATE TABLE [dbo].[tblSkillsDocument]
(
	[SkillsDocumentId] INT NOT NULL PRIMARY KEY,
	[SkillsDocumentTitle] TEXT,
	[CreatedAt] DATETIME,
	[CreatedBy] TEXT,
	[UpdatedAt] DATETIME,
	[UpdatedBy] TEXT,
	[DeletedAt] DATETIME,
	[DeletedBy] TEXT,
	[ExpiresTimespan] INT,
	[ExpiresType] INT, /* ??? */
	[XMLValueKeys] TEXT, /* ??? */
	[LastAccessed] DATETIME, 
    [ReferenceCode] NVARCHAR(MAX) NULL, 
    [SkillsDocumentTypeSysId] INT NULL
)
