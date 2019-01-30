using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace IW.IDao
{
    public abstract class IImageService
    {
        public abstract void addViewNum();

        public abstract List<string> getPhotoList();

        public abstract int UploadImage(HttpFileCollection files);
    }
}
