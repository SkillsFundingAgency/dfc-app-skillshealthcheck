﻿CREATE TABLE [dbo].[JobFamilies]
(
	Id INT PRIMARY KEY IDENTITY,
    Code NVARCHAR(50) UNIQUE,
    Title NVARCHAR(255),
    KeySkillsStatement1 NVARCHAR(MAX),
    KeySkillsStatement2 NVARCHAR(MAX),
    KeySkillsStatement3 NVARCHAR(MAX),
    TakingResponsibility FLOAT,
    WorkingWithOthers FLOAT,
    PersuadingAndSpeaking FLOAT,
    ThinkingCritically FLOAT,
    CreationAndInnovation FLOAT,
    PlanningAndOrganising FLOAT,
    HandlingChangeAndPressure FLOAT,
    AchievingResults FLOAT,
    LearningAndTechnology FLOAT,
    Verbal BIT,
    Numerical BIT,
    Checking BIT,
    Spatial BIT,
    Abstract BIT,
    Mechanical BIT,
    RelevantTasksCompletedText NVARCHAR(MAX),
    RelevantTasksNotCompletedText NVARCHAR(MAX),
    Url NVARCHAR(MAX)
)
