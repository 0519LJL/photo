using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.Model
{
    public class ViewPageInfo
    {
        public Guid pageId { get; set; }

        public string pageName { get; set; }

        public int viewNum { get; set; }

        public bool isEnable { get; set; }

        public DateTime createTime { get; set; }

        public DateTime lastUpdateDate { get; set; }
    }
}
