using System;
using System.Data;
using Microsoft.Data.SqlClient;
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
        private readonly int monthsToKeep = 12;

        private dynamic timeToLive;
        private dynamic sqlTransaction;

        public DataCleanup(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            timeToLive = DateTime.UtcNow.AddMonths(-monthsToKeep);
            sqlTransaction =
            $@"BEGIN TRANSACTION [DeleteOldSkillsDocuments]
               BEGIN TRY
                 DELETE FROM [dbo].[SkillsDocuments]
                 WHERE UpdatedAt <DATEADD(MONTH, -{monthsToKeep}, GETUTCDATE())
                 or
                 (UpdatedAt IS NULL and
                 CreatedAt <DATEADD(MONTH, -{monthsToKeep}, GETUTCDATE()))
                 COMMIT TRANSACTION [DeleteOldSkillsDocuments]
               END TRY
               BEGIN CATCH
                 ROLLBACK TRANSACTION [DeleteOldSkillsDocuments];
                 THROW
               END CATCH";
        }

        [FunctionName("DataCleanup")]
        public async Task Run([TimerTrigger("00 00 03 * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed daily at 03:00 UTC i.e. at: {DateTime.Now} server-time.");
            //note: when running this locally on machine this uses local time; on azure this uses UTC.

            if (timer.IsPastDue)
            {
                log.LogInformation($"Timer is past due. Exiting function.");
                return;
            }

            log.LogInformation($"Running the daily stored procedure to delete SkillsDocuments records not updated in the last {monthsToKeep} months i.e. since: {timeToLive.ToShortDateString()}");

            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                await connection.QueryAsync(sqlTransaction as string);
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