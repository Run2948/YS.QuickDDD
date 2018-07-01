using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quick.Web.Models.RequestModel
{
    public class PasswordRequest : LoginRequest
    {
        [StringLength(32)]
        [Required]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [StringLength(32)]
        [Required]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        public override void Trim()
        {
            if (!string.IsNullOrEmpty(UserName)) UserName = UserName.Trim();
            if (!string.IsNullOrEmpty(Password)) Password = Password.Trim();
            if (!string.IsNullOrEmpty(ConfirmPassword)) ConfirmPassword = ConfirmPassword.Trim();
        }
    }
}