using System;
using IW.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IW.Model;
using IW.IDao;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IUserService cla = new UserService();

            UserInfo  user = new UserInfo();
            user.id = Guid.NewGuid();
            user.name = "测试用户2";
            user.Phone = "1300045";
            user.sex = 1;
            user.age = 12;

            cla.addUser(user);
        }

        [TestMethod]
        public void TestViewPageMethod1()
        {
            IViewPageService cla = new ViewPageService();

            ViewPageInfo viewPage = new ViewPageInfo();
            viewPage.pageId = Guid.NewGuid();
            viewPage.pageName = "查看网页";
            cla.addPageInfos(viewPage);
        }

        [TestMethod]
        public void TestConfigMethod1()
        {
            IImageService imageService = new ImageService();
            imageService.getPhotoList();
        }
    }
}
