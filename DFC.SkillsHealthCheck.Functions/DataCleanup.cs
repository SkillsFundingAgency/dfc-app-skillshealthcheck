using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DFC.SkillsHealthCheck.Functions
{
    public class DataCleanup
    {
        private readonly DatabaseContext _dbContext;
        private readonly string sqlTransaction = "BEGIN TRANSACTION [DeleteOldSkillsDocuments]\r\n  BEGIN TRY\r\n\tDELETE \r\n\tFROM [dbo].[SkillsDocuments]\r\n\tWHERE UpdatedAt <DATEADD(MONTH, -12, GETUTCDATE())\r\n\tCOMMIT TRANSACTION [DeleteOldSkillsDocuments]\r\n  END TRY\r\n  BEGIN CATCH\r\n        ROLLBACK TRANSACTION [DeleteOldSkillsDocuments];\r\n        THROW\r\n  END CATCH  ";

        public DataCleanup(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("DataCleanup")]
        public async Task Run([TimerTrigger("0 00 13 * * *")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed daily at 13:00 localtime (for testing) i.e. at: {DateTime.UtcNow} UTC");

            if (timer.IsPastDue)
            {
                log.LogInformation($"Timer is past due. Exiting function.");
                return;
            }

            var timeToLive = DateTime.UtcNow.AddMonths(-12);

            log.LogInformation($"Running the daily stored procedure to delete SkillsDocuments records not updated in the last 12 months i.e. since: {timeToLive.ToShortDateString()}");
            
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                await connection.QueryAsync(sqlTransaction);
            }
        }
    }

    public class DatabaseContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
