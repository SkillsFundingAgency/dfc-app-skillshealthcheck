using System.Data;

namespace DFC.SkillsCentral.Api.Infrastructure;

public interface IDatabaseContext
{
    IDbConnection CreateConnection();
}