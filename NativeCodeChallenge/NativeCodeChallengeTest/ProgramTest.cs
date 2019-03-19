using NativeCodeChallenge;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSum1()
        {
            Assert.AreEqual(Program.Sum(new List<int> { -2, 2 }), 0);
        }
        [Test]
        public void TestSum2()
        {
            Assert.AreEqual(Program.Sum(new List<int> { 1,2,4,3 }), 6);
        }
        [Test]
        public void TestSum3()
        {
            Assert.AreEqual(Program.Sum(new List<int> { -2, -4, -3 }), -6);
        }
        [Test]
        public void TestMakeRequestOK()
        {
            string response;
            var statusCode =Program.MakeRequest("http://google.com",5000, out response);
            Assert.AreEqual(statusCode, HttpStatusCode.OK);
        }

        [Test]
        public void TestMakeRequestTimeout()
        {
            string response;
            var statusCode = Program.MakeRequest("http://google.com", 1, out response);
            Assert.AreEqual(statusCode, HttpStatusCode.RequestTimeout);
        }

        [Test]
        public void TestRequestWith500()
        {
            string response;
            var statusCode = Program.MakeRequest("http://localhost:24869/api/Values/abc", 5000, out response);
            Assert.AreEqual(statusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public void TestAndLogRequest1()
        {
            Program.MakeAndLogRequest("http://google.com", 5000);
        }

        [Test]
        public void TestAndLogRequest2()
        {
            Program.MakeAndLogRequest("http://localhost:24869/api/Values/abc", 5000);
        }

        [Test]
        public void TestAndLogRequest3()
        {
            Program.MakeAndLogRequest("http://google.com", 1);
        }

    }
}