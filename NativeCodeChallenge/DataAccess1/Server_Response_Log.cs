using System;

namespace DataAccess
{
    public class Server_Response_Log
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StatusCode { get; set; }
        public short ErrorCode { get; set; }
        public string ResponseText { get; set; }
    }
}
