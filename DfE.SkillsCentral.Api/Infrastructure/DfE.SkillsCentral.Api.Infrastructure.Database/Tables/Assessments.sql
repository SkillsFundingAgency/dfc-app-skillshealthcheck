﻿CREATE TABLE [dbo].[Assessments]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Type] NVARCHAR(MAX) NOT NULL, 
	[Title] NVARCHAR(MAX) NOT NULL,
	[Subtitle] NVARCHAR(MAX) NULL,
	[Introduction] NVARCHAR(MAX) NULL,
)
