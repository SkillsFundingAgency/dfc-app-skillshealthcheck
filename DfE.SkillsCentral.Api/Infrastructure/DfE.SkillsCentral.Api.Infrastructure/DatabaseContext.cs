using System.Data;
using Microsoft.Data.SqlClient;

namespace DFC.SkillsCentral.Api.Infrastructure;

public class DatabaseContext : IDatabaseContext
{
    private readonly string connectionString;


    public DatabaseContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }
}