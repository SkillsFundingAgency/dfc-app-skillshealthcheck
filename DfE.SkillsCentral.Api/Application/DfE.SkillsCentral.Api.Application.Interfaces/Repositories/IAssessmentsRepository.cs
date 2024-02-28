using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Application.Interfaces.Repositories
{
    public interface IAssessmentsRepository :IRepository<Assessment>
    {
        Task<IReadOnlyList<Assessment>> GetAllAsync();
    }
}
