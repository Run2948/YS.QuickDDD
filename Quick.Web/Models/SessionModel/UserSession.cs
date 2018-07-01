using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quick.Web.Models.SessionModel
{
    public class UserSession
    {
        public UserSession()
        {

        }

        public UserSession(object userId, string userName, int userType, string nickName)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.UserType = userType;
            this.NickName = nickName;
        }

        public object UserId { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public string NickName { get; set; }
    }
}