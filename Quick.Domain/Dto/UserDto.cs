using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quick.Data.Entities;

namespace Quick.Domain.Dto
{
    public class UserDto
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public int UserType { get; set; }

        public string NickName { get; set; }
    }
}