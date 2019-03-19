using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class AlsoEnergyContext:DbContext
    {
        public AlsoEnergyContext(DbContextOptions<AlsoEnergyContext> options)
            : base(options)
        {

        }
        public DbSet<Server_Response_Log> Server_Response_Logs { get; set; }

    }
}
