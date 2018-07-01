using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quick.Web.Models.RequestModel
{
    public class LoginRequest
    {
        public LoginRequest()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        [StringLength(16)]
        [Required]
        [Display(Name = "登录账号")]
        public string UserName { get; set; }

        [StringLength(32)]
        [Required]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        public virtual void Trim()
        {
            if (!string.IsNullOrEmpty(UserName)) UserName = UserName.Trim();
            if (!string.IsNullOrEmpty(Password)) Password = Password.Trim();
        }
    }
}