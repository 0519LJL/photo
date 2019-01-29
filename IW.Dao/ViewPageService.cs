using IDH.Common.Utility;
using IW.IDao;
using IW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.Dao
{
    public class ViewPageService : IViewPageService
    {

        /// <summary>
        /// 查询浏览记录
        /// </summary>
        /// <returns></returns>
        public override List<ViewPageInfo> GetPageInfos()
        {
            var db = new PetaPoco.Database("connection");
            var dataList = db.Query<ViewPageInfo>("SELECT * FROM ViewPageInfo").ToList();
            return dataList;
        }

        public override ViewPageInfo GetPageInfoById(Guid id)
        {
            var db = new PetaPoco.Database("connection");

            var dataList = db.Query<ViewPageInfo>("SELECT * FROM ViewPageInfo where pageId = @pageId", new ViewPageInfo(){pageId = id}).ToList();

            return dataList.FirstOrDefault();
        }

       
        /// <summary>
        /// 添加浏览记录
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public override int addPageInfos(ViewPageInfo viewPage)
        {
            var db = new PetaPoco.Database("connection");
            var result = db.Execute("INSERT  INTO dbo.viewPageInfo ( pageId, pageName, viewNum ) VALUES  ( @pageId, @pageName, @viewNum )", viewPage);
            return result;
        }

        /// <summary>
        /// 增加浏览记录
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public override int AddPageViewNum(ViewPageInfo viewPage)
        {
            var db = new PetaPoco.Database("connection");
            var result = db.Execute(@"UPDATE dbo.viewPageInfo  SET viewNum +=1 WHERE pageId =@pageId", viewPage);
            return result;
        }
    }

    
}
