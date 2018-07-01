using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quick.Data.Entities;

namespace Quick.Core.Domain
{
    public class UserDto : BaseEntity
    {
        public string UserName { get; set; }

        public int UserType { get; set; }

        public string NickName { get; set; }
    }
}