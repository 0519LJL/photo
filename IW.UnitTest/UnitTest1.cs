using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IW.Service;

namespace IW.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UserService dd = new UserService();
            var d=dd.getUser();
        }
    }
}
