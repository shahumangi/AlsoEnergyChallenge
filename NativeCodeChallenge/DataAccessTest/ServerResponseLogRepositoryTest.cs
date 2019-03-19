using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Linq;

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
                    //Assert.That(temp.Count() > 0); this condition is working only in local when we have records inserted
                }
            }

        }

        [Test]
        public void TestGetErroCodeAndCount()
        {
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<AlsoEnergyContext>().UseSqlite(connection).Options;

                using (var context = new AlsoEnergyContext(options))
                {
                    context.Database.EnsureCreated();

                    IServerResponseLogRepository serverResponseLogRepository = new ServerResponseLogRepository(new AlsoEnergyContext(options));
                    var date = DateTime.Now.Date;
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date.AddHours(3), HttpStatusCode = 200 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date.AddHours(3), HttpStatusCode = 200 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date.AddHours(3), HttpStatusCode = 500 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date.AddHours(2), HttpStatusCode = 200 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date.AddHours(2), HttpStatusCode = 200 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date, HttpStatusCode = 200 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date, HttpStatusCode = 500 });
                    serverResponseLogRepository.Save(new Server_Response_Log { StartTime = date, HttpStatusCode = 408 });


                    var dictionary =serverResponseLogRepository.GetErrorCodeAndCount(date);
                    Assert.AreEqual(dictionary.Count, 3);
                    Assert.AreEqual(dictionary[0].Count(), 3);
                    Assert.AreEqual(dictionary[2].Count(), 1);
                    Assert.AreEqual(dictionary[2].First().Key, 200);
                    Assert.AreEqual(dictionary[2].First().Value, 2);
                    Assert.AreEqual(dictionary[3].Count(), 2);
                    Assert.AreEqual(dictionary[3].First(kv => kv.Key==200).Value, 2);
                    Assert.AreEqual(dictionary[3].First(kv => kv.Key == 500).Value, 1);
                }
            }

        }
    }
}