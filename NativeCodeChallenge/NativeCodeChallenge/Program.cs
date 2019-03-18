using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace NativeCodeChallenge
{
    public class Program
    {
        private const string connectionString = "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Database=ae_code_challenge;";
        static void Main(string[] args)
        {
            Console.WriteLine(Sum(new List<int> { 1, 2, 3 }));
            Console.WriteLine(Sum(new List<int> { 2, 2, 4 }));
            Console.WriteLine(Sum(new List<int> { -4, 2 }));
            string response;
            MakeRequest("http://google.com",5000,out response);

            PrintNumbers(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
            Console.ReadLine();
        }

        public static long? Sum(List<int> numbers)
        {
            if (numbers == null)
                return null;
            return numbers.Sum(i => i % 2 == 0 ? (long)i : 0);
        }

        public static HttpStatusCode MakeRequest(string url,int timeOut,out string responseFromServer)
        {
            var statusCode = HttpStatusCode.BadRequest;
            responseFromServer = null;
            HttpWebResponse response = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Timeout = timeOut;
                // Get the response.  
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    statusCode = response.StatusCode;
                    if(statusCode != HttpStatusCode.OK)
                    // Get the stream containing content returned by the server.  
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.  
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            // Read the content.  
                            responseFromServer = reader.ReadToEnd();
                            // Display the content.  
                            Console.WriteLine(responseFromServer);
                            // Clean up the streams and the response.  
                            reader.Close();
                        }
                    }
                    response.Close();
                }
            }
            catch(WebException ex)
            {
                if (ex.Response != null && ex.Response is HttpWebResponse)
                    statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                if (ex.Status == WebExceptionStatus.Timeout)
                    statusCode = HttpStatusCode.RequestTimeout;
            }
            catch (Exception)
            {
            }
            return statusCode;
        }

        public static void MakeAndLogRequest(string url, int timeOut)
        {
            string response;
            var statusCode = MakeRequest(url, timeOut, out response);
        }

        static void LogRequest(DateTime startTime, DateTime endTime, int statusCode, short errorCode,string responseText)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<AlsoEnergyContext>().UseSqlServer(connection).Options;

            using (var context = new AlsoEnergyContext(options))
            {
                IServerResponseLogRepository serverResponseLogRepository = new ServerResponseLogRepository(new AlsoEnergyContext(options));
                serverResponseLogRepository.Save(new Server_Response_Log() { StartTime=startTime, EndTime = endTime, ErrorCode = errorCode, StatusCode = statusCode, ResponseText = responseText });
            }
        }

        static void PrintNumbers(List<int> numbers)
        {
            var reverseList = numbers.Reverse<int>();
            var uniqueThreadAndNumberCollection = new ConcurrentDictionary<int,string>();
            var task1=  PrintNumbersAsync( numbers, "t1",500,uniqueThreadAndNumberCollection);
            var task2 = PrintNumbersAsync(reverseList, "t2",1000,uniqueThreadAndNumberCollection);
            task1.Wait();
            task2.Wait();

            //here there is assumption that the number will be only once in list. So for example if the list is 1,2,1,3,4 then 1 will be only comes once and which thread executed it.
            Console.WriteLine("Threads who printed number first");
            foreach(var keyValPair in uniqueThreadAndNumberCollection)
            {
                Console.WriteLine(keyValPair.Key + " : " + keyValPair.Value);
            }

        }

        static  async Task PrintNumbersAsync(IEnumerable<int> numbers,string threadName,int delay, ConcurrentDictionary<int,string> dictionary)
        {
            await Task.Run(() =>
            {
                foreach (var number in numbers)
                {
                    Console.WriteLine(threadName + " : " + number);
                    if (!dictionary.ContainsKey(number))
                        dictionary.TryAdd(number, threadName);
                    Thread.Sleep(delay);
                }
            });
        }
    }
}
