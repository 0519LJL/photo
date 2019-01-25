using System;
using IW.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IW.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UserService cla = new UserService();

            UserInfo  user = new UserInfo();
            user.id = Guid.NewGuid();
            user.name = "测试用户2";
            user.Phone = "1300045";
            user.sex = 1;
            user.age = 12;

            cla.addUser(user);
        }
    }
}
