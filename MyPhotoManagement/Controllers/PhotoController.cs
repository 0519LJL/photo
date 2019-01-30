using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IW.Dao;
using IW.IDao;

namespace MyPhotoManagement.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UpLoad()
        {
            Result<string> check = new Result<string>();
            try
            {
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                IImageService imageService = new ImageService();

                int number = imageService.UploadImage(files);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
            return new JsonResult() { Data = "上传成功" };
        }

        /// <summary>
        /// 返回值
        /// </summary>
        public class Result<T>
        {
            public string Message { get; set; }
            public bool Check { get; set; }
            public IList<T> ResultList { get; set; }
        }


    }
}
