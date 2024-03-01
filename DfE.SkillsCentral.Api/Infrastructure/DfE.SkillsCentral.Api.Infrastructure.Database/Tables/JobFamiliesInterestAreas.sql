CREATE TABLE [dbo].[JobFamiliesInterestAreas]
(
    JobFamilyId INT,
    Name NVARCHAR(255),
    CONSTRAINT UC_JobFamilyId_Name UNIQUE (JobFamilyId, Name),
    FOREIGN KEY (JobFamilyId) REFERENCES JobFamilies(Id)
)