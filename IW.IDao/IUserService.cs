using IW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IW.IDao
{
    public abstract class IUserService
    {
        public abstract List<UserInfo> getUsers();

        public abstract int addUser(UserInfo use);
    }
}
