using IW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.Dao
{
    public class UserService
    {
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> getUsers()
        {
            var db = new PetaPoco.Database("connection");
            var dataList = db.Query<UserInfo>("SELECT * FROM userInfo").ToList();
            return dataList;
        }

       
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public int addUser(UserInfo use)
        {
            var db = new PetaPoco.Database("connection");
            var result = db.Execute("INSERT INTO dbo.userInfo ( id, name, Phone, sex, age )VALUES  ( @id,@name, @Phone, @sex, @age )", use);
            return result;
        }
    }

    
}
