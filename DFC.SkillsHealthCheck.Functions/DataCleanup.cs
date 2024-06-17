using System;
using System.Threading.Tasks;
using Dapper;
using DFC.SkillsCentral.Api.Infrastructure;
using Microsoft.Azure.WebJobs;
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
        public async Task Run([TimerTrigger("0 0 4 * * *")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed daily at 04:00 AM UTC i.e. at: {DateTime.UtcNow}");

            if (timer.IsPastDue)
            {
                return;
            }

            var timeToLive = DateTime.UtcNow.AddMonths(-12);

            log.LogInformation($"Calling the daily stored procedure to delete SkillsDocuments records not updated in the last 12 months as of {timeToLive.ToShortDateString()}");
            
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                await connection.QueryAsync(sqlTransaction);
            }
        }
    }
}
