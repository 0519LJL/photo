using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IW.IDao;
using IW.Model;
using System.Configuration;
using System.IO;
using System.Web;

namespace IW.Dao
{
    public class ImageService : IImageService
    {
        private Guid viewPageId = new Guid("49DB4BC3-0D56-406D-81DC-F9B87F11D7C7");
        private static string domainPath = System.AppDomain.CurrentDomain.BaseDirectory;

        public override void addViewNum()
        {
            IViewPageService viewPageService = new ViewPageService();

            ViewPageInfo pageInfos = viewPageService.GetPageInfoById(viewPageId);
            ViewPageInfo pageInfo = new ViewPageInfo()
            {
                pageId = viewPageId,
                pageName = "相册纪念页",
                viewNum = 1,
            };

            if (pageInfos == null)
            {
                viewPageService.addPageInfos(pageInfo);
                return;
            }

            viewPageService.AddPageViewNum(pageInfo);
        }

        public override List<string> getPhotoList()
        {
            addViewNum();

            var config = ConfigurationManager.AppSettings["xiang"];
            if (!string.IsNullOrEmpty(config))
            {
                List<string> paths = new List<string>();
                var path = domainPath + config;
                DirectoryInfo folder = new DirectoryInfo(path);

                foreach (FileInfo file in folder.GetFiles())
                {
                    paths.Add(file.Name);
                }

                return paths;
            }

            return new List<string>();
        }

        public override int UploadImage(HttpFileCollection files)
        {
            var config = ConfigurationManager.AppSettings["xiang"];
            var fileName = domainPath + config + Guid.NewGuid();

            int number = 0;
            for (int i = 0; i < files.Count; i++)
            {
                fileName = domainPath + config + Guid.NewGuid() + Path.GetExtension(files[i].FileName);

                System.IO.Stream stream = files[i].InputStream;
                System.Drawing.Image initImage = System.Drawing.Image.FromStream(stream, true);
                initImage.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
               
                bool ok = System.IO.File.Exists(fileName.ToString());
                if (ok)
                {
                    number++;
                }
            }

            return number;
        }
    }
}
