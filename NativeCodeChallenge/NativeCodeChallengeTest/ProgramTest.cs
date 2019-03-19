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
        
    }
}