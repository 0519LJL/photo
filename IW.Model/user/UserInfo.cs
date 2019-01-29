using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.Model
{
    public class UserInfo
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string Phone { get; set; }
        public int sex { get; set; }
        public int age { get; set; }

        public bool isEnable { get; set; }

        public DateTime createTime { get; set; }

        public DateTime lastUpdateDate { get; set; }
    }
}
