CREATE TABLE [dbo].[JobFamiliesInterestAreas]
(
	Id INT PRIMARY KEY IDENTITY,
    JobFamilyId INT,
    InterestAreaId INT,
    FOREIGN KEY (JobFamilyId) REFERENCES JobFamilies(Id),
    FOREIGN KEY (InterestAreaId) REFERENCES InterestAreas(Id)
)
