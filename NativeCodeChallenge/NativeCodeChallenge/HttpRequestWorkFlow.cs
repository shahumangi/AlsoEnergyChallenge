using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NativeCodeChallenge
{
    public class HttpRequestWorkFlow
    {
        public void MakeAndLogRequest(string url, int timeOut)
        {
            string response;
            DateTime startTime = DateTime.Now;
            var statusCode = new HttpRequestSender().MakeRequest(url, timeOut, out response);
            DateTime endTime = DateTime.Now;
            new HttpRequestLogger().LogRequest(startTime, endTime, (int)statusCode, (short)(statusCode == HttpStatusCode.OK ? 1 : (statusCode == HttpStatusCode.RequestTimeout ? -999 : 2)), response);
        }

    }
}
