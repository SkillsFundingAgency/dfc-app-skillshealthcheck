using DfE.SkillsCentral.Api.Domain.Models;

namespace DfE.SkillsCentral.Api.Application.Interfaces.Repositories;

public interface IJobFamiliesRepository
{
    Task<IReadOnlyList<JobFamily>> GetAllAsync();
}