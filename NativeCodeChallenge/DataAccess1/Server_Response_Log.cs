using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table("Server_Response_Log")]
    public class Server_Response_Log
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HttpStatusCode { get; set; }
        public short Status { get; set; }
        public string ResponseText { get; set; }
    }
}
