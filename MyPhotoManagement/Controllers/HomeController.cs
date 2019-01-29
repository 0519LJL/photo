using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IW.Dao;
using IW.IDao;

namespace MyPhotoManagement.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Image()
        {
            IImageService imageService = new ImageService();
            imageService.addViewNum();
            return View();
        }

    }
}
