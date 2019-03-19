using NativeCodeChallenge;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NativeCodeChallengeTest
{
    public class HttpRequestWorkflowTest
    {
        [Test]
        public void TestAndLogOKRequest()
        {
            new HttpRequestWorkFlow().MakeAndLogRequest("http://google.com", 5000);
        }

        [Test]
        public void TestAndLogInternalErrorRequest()
        {
            new HttpRequestWorkFlow().MakeAndLogRequest("http://localhost:24869/api/Values/abc", 5000);
        }

        [Test]
        public void TestAndLogTimeoutRequest()
        {
            new HttpRequestWorkFlow().MakeAndLogRequest("http://google.com", 1);
        }

    }
}
