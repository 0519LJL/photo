using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult UpLoad(List<string> strbase)
        {
            string base64str = strbase[0].Replace("data:image/jpeg;base64,", "");
            string[] base64strs = base64str.Split(',');
            try
            {
                for (int i = 0; i < base64strs.Length; i++)
                {
                    byte[] bt = Convert.FromBase64String(base64strs[i]);//获取图片base64
                    string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();//年月
                    string ImageFilePath = "/Image" + "/" + fileName;
                    if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(ImageFilePath)) == false)//如果不存在就创建文件夹
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Server.MapPath(ImageFilePath));
                    }
                    string ImagePath = HttpContext.Current.Server.MapPath(ImageFilePath) + "/" + System.DateTime.Now.ToString("yyyyHHddHHmmss");//定义图片名称
                    File.WriteAllBytes(ImagePath + ".png", bt); //保存图片到服务器，然后获取路径  
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return new JsonResult() { Data = "上传成功" };
        }

        public class photo
        {
            public string name;
            public string base64;
        }

    }
}
