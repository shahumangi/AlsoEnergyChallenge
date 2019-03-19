using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NativeCodeChallenge
{
    public class HttpRequestSender
    {
        public HttpStatusCode MakeRequest(string url, int timeOut, out string responseFromServer)
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
                    if (statusCode != HttpStatusCode.OK)
                        return statusCode;
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
            catch (WebException ex)
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


    }
}
