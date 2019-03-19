using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeWebApplication
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private const string connectionString = "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Database=ae_code_challenge;";
        // GET: /<controller>/
        public IActionResult Index()
        {
            Dictionary<int, IEnumerable<KeyValuePair<int, int>>> model;
             using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<AlsoEnergyContext>().UseSqlServer(connection).Options;

                using (var context = new AlsoEnergyContext(options))
                {
                    IServerResponseLogRepository serverResponseLogRepository = new ServerResponseLogRepository(new AlsoEnergyContext(options));
                    model = serverResponseLogRepository.GetErrorCodeAndCount(DateTime.Now.AddHours(-3));

                }
            }
            return View(model);
        }
    }
}
