using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services
{
    internal static class UsageLog
    {
        internal static void UpdateUsagePrivate(Int64 userId, DateTime? current, String activity)
        {
            //TODO: need to know more about this
            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ImsCwpProfileDatabase"];
            //String connectionString = settings.ConnectionString;
            //String commandText = "[cds].[uspUpdateLearnerUsage]";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(commandText, connection))
            //    {
            //        command.CommandType = CommandType.StoredProcedure;
            //        command.Parameters.Add("@SkillsAccountId", SqlDbType.Decimal).Value = (Decimal)userId;
            //        command.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = DBNull.Value;
            //        command.Parameters.Add("@CourseSearchUsageDate", SqlDbType.DateTime).Value = DBNull.Value;

            //        command.Parameters.Add("@SkillsHealthCheckUsageDate", SqlDbType.DateTime).Value = (activity.Equals("SHC", StringComparison.OrdinalIgnoreCase)) ? current : (Object)DBNull.Value;
            //        command.Parameters.Add("@CVBuilderUsageDate", SqlDbType.DateTime).Value = (activity.Equals("CV", StringComparison.OrdinalIgnoreCase)) ? current : (Object)DBNull.Value;
            //        command.Parameters.Add("@PersonalActionPlanUsageDate", SqlDbType.DateTime).Value = (activity.Equals("PAP", StringComparison.OrdinalIgnoreCase) || activity.Equals("ACAP", StringComparison.OrdinalIgnoreCase)) ? current : (Object)DBNull.Value;

            //        command.Parameters.Add("@PersonalLearningRecordUsageDate", SqlDbType.DateTime).Value = DBNull.Value;
            //        command.Parameters.Add("@AccountOpenedDate", SqlDbType.DateTime).Value = DBNull.Value;

            //        command.Connection.Open();

            //        Int32 result = command.ExecuteNonQuery();
            //        command.Connection.Close();
            //    }
            //}
        }
    }
}
