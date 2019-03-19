using DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace Tests
{
    public class Tests
    {
        private const string connectionString = "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Database=ae_code_challenge;";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetFiveRecentServerLogsInTimeSpan()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<AlsoEnergyContext>().UseSqlServer(connection).Options;

                using (var context = new AlsoEnergyContext(options))
                {
                    IServerResponseLogRepository serverResponseLogRepository = new ServerResponseLogRepository(new AlsoEnergyContext(options));
                    var temp =serverResponseLogRepository.GetFiveRecentServerLogsInTimeSpan(DateTime.Now.AddHours(-2), DateTime.Now);
                }
            }

        }
    }
}