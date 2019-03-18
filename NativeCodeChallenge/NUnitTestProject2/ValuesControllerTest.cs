using ChallengeWebApplication;
using NUnit.Framework;
using System;
using System.Web.Http;

namespace Tests
{
    public class ValuesControllerTest
    {
        private ValuesController valuesController;
        [SetUp]
        public void Setup()
        {
            valuesController = new ValuesController();
        }

        [Test]
        public void MethodWithStringParameterThrowsException()
        {
            Assert.Throws<HttpResponseException>(() => valuesController.Get("v"));
        }

        [Test]
        public void MethodWithoutParameterReturnsDateTime()
        {
            var dateTime =valuesController.Get();
            Assert.AreEqual(dateTime.Date, DateTime.Now.Date); //There is millisecond difference between dateTime returned by API and current Date
        }
    }
}