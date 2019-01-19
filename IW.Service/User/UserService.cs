using IW.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.Service
{
    public class UserService
    {

        public user getUser()
        {
            //using (var container = new usersystemSql())
            //{
            //    var userd = container.user.ToList();
            //}

            return new user();
        }
    }
}
