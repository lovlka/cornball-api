using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Cornball.Web.Models;

namespace Cornball.Web.Services
{
    public class AzureService
    {
        private readonly string _connectionString;

        public AzureService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Cornball"].ConnectionString;
        }

        public void IncreaseValue(string name)
        {
            Execute("update [Statistics] set [Value] = [Value] + 1 where [Name] = @Name",
                new SqlParameter("@Name", name));
        }

        public List<DataRecord> GetStatistics()
        {
            return Read("select [Name], [Value] from [Statistics]");
        }

        public void SaveHighscore(string name, int score)
        {
            Execute("insert into HighScore values(@Name, @Score, GETDATE())",
                new SqlParameter("@Name", name),
                new SqlParameter("@Score", score));
        }

        public List<DataRecord> GetHighscores(int limit, DateTime? startDate, DateTime? endDate)
        {
            return Read(string.Format("select top {0} [Name], [Score], [DateTime] from [HighScore] " +
                                      "where [DateTime] > @StartDate and [DateTime] < @EndDate " +
                                      "order by [Score] desc", limit),
                        new SqlParameter("@StartDate", startDate ?? SqlDateTime.MinValue),
                        new SqlParameter("@EndDate", endDate ?? SqlDateTime.MaxValue));
        }

        private void Execute(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private List<DataRecord> Read(string sql, params SqlParameter[] parameters)
        {
            var values = new List<DataRecord>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                command.Connection.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        values.Add(GetDataRecord(dataReader));
                    }
                }
            }
            return values;
        }

        private static DataRecord GetDataRecord(IDataRecord dataRecord)
        {
            return new DataRecord
            {
                Name = dataRecord.GetString(0),
                Value = dataRecord.GetInt32(1),
                Date = dataRecord.FieldCount > 2 ? dataRecord.GetDateTime(2) : (DateTime?) null
            };
        }
    }
}