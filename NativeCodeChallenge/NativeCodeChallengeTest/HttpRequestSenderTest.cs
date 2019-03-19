using NativeCodeChallenge;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NativeCodeChallengeTest
{
    public class HttpRequestSenderTest
    {
        [Test]
        public void TestMakeRequestOK()
        {
            string response;
            var statusCode = new HttpRequestSender().MakeRequest("http://google.com", 5000, out response);
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
        }

        [Test]
        public void TestMakeRequestTimeout()
        {
            string response;
            var statusCode = new HttpRequestSender().MakeRequest("http://google.com", 1, out response);
            Assert.AreEqual(statusCode, HttpStatusCode.RequestTimeout);
        }

        [Test]
        public void TestRequestWith500()
        {
            string response;
            var statusCode = new HttpRequestSender().MakeRequest("http://localhost:24869/api/Values/abc", 5000, out response);
            Assert.AreEqual(statusCode, HttpStatusCode.InternalServerError);
        }


    }
}
