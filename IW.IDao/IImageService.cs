using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.IDao
{
    public abstract class IImageService
    {
        public abstract void addViewNum();

        public abstract List<string> getPhotoList();
    }
}
