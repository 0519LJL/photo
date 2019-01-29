using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IW.IDao;
using IW.Model;

namespace IW.Dao
{
    public class ImageService : IImageService
    {
        private Guid viewPageId = new Guid("49DB4BC3-0D56-406D-81DC-F9B87F11D7C7");

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
    }
}
