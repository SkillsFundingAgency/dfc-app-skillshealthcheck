using Dapper;
using DFC.SkillsCentral.Api.Infrastructure;
using DfE.SkillsCentral.Api.Application.Interfaces.Repositories;
using DfE.SkillsCentral.Api.Domain.Models;

namespace DfE.SkillsCentral.Api.Infrastructure.Repositories;

public class JobFamiliesRepository : IJobFamiliesRepository
{
    private readonly DatabaseContext dbContext;

    public JobFamiliesRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<JobFamily>> GetAllAsync()
    {
        var query = @"
            SELECT 
                jf.*, 
                ia.Name AS InterestAreaName 
            FROM 
                JobFamilies jf
            LEFT JOIN 
                JobFamiliesInterestAreas jfia ON jf.Id = jfia.JobFamilyId";

        using var connection = dbContext.CreateConnection();
        var jobFamilies = (await connection.QueryAsync<JobFamily, InterestArea, JobFamily>(
                query,
                (jobFamily, interestArea) =>
                {
                    jobFamily.InterestAreas.Add(interestArea);
                    return jobFamily;
                },
                splitOn: "InterestAreaName")
            )
            .Distinct()
            .ToList();

        return jobFamilies;
    }
}