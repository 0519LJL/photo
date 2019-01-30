using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IW.Dao;

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
        public JsonResult UpLoad(HttpPostedFileBase file)
        {
            var res = CheckImg(file);
            
            
            return new JsonResult() { Data = "上传成功" };
        }

        private string CheckImg(HttpPostedFileBase file)
        {
            if (file == null) return "图片不能空！";
            if (file.ContentLength / 1024 > 8000)
            {
                return "图片太大";
            }
            if (file.ContentLength / 1024 < 10)
            {
                return "图片太小！";
            }
            var image = GetExtensionName(file.FileName).ToLower();
            if (image != ".bmp" && image != ".png" && image != ".gif" && image != ".jpg" && image != ".jpeg")// 这里你自己加入其他图片格式，最好全部转化为大写再判断，我就偷懒了
            {
                return "格式不对";
            }

            var scrtemp = Path.Combine("../../Content/TempFile/", file.FileName);//图片展示的地址
            var list = Session["Imgscr"] as List<string>;
            if (list != null && list.Find(n => n == scrtemp) != null)
            {
                return "同样的照片已经存在！";
            }

            return "ok";
        }

        public string GetExtensionName(string fileName)
        {
            if (fileName.LastIndexOf("\\", StringComparison.Ordinal) > -1)//在不同浏览器下，filename有时候指的是文件名，有时候指的是全路径，所有这里要要统一。
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);//IndexOf 有时候会受到特殊字符的影响而判断错误。加上这个就纠正了。
            }
            return Path.GetExtension(fileName.ToLower());
        }


    }
}
