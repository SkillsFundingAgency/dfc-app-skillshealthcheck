using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DFC.SkillsHealthCheck.Functions
{
    public class DataCleanup
    {
        private readonly DatabaseContext _dbContext;
        private readonly string sqlTransaction = "BEGIN TRANSACTION [DeleteOldSkillsDocuments]\r\n  BEGIN TRY\r\n\tDELETE \r\n\tFROM [dbo].[SkillsDocuments]\r\n\tWHERE UpdatedAt <DATEADD(MONTH, -1, GETUTCDATE())\r\n\tCOMMIT TRANSACTION [DeleteOldSkillsDocuments]\r\n  END TRY\r\n  BEGIN CATCH\r\n        ROLLBACK TRANSACTION [DeleteOldSkillsDocuments];\r\n        THROW\r\n  END CATCH  ";

        public DataCleanup(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("DataCleanup")]
        public async Task Run([TimerTrigger("00 00 9 * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed daily at 03:00 UTC i.e. at: {DateTime.Now} server-time.");
            //note: when running this locally on machine this uses local time; on azure this uses UTC.

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