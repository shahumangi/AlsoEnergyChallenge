using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeCodeChallenge
{
    public class HttpRequestLogger
    {
        private const string connectionString = "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Database=ae_code_challenge;";
        public void LogRequest(DateTime startTime, DateTime endTime, int statusCode, short status, string responseText)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<AlsoEnergyContext>().UseSqlServer(connection).Options;

                using (var context = new AlsoEnergyContext(options))
                {
                    IServerResponseLogRepository serverResponseLogRepository = new ServerResponseLogRepository(new AlsoEnergyContext(options));
                    serverResponseLogRepository.Save(new Server_Response_Log() { StartTime = startTime, EndTime = endTime, Status = status, HttpStatusCode = statusCode, ResponseText = responseText });
                }
            }
        }

    }
}
