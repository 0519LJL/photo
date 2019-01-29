using IW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.IDao
{
    public abstract class IViewPageService
    {
        public abstract List<ViewPageInfo> GetPageInfos();

        public abstract ViewPageInfo GetPageInfoById(Guid id);

        public abstract int addPageInfos(ViewPageInfo viewPage);

        public abstract int AddPageViewNum(ViewPageInfo viewPage);
    }
}
