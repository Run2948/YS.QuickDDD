/* ==============================================================================
* 命名空间：Quick.Data.Entities
* 类 名 称：SysUser
* 创 建 者：Qing
* 创建时间：2018-06-17 21:54:46
* CLR 版本：4.0.30319.42000
* 保存的文件名：SysUser
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masuit.Tools.Validator;

namespace Quick.Data.Entities.Sys
{
    [Serializable]
    public class UserInfo : BaseEntity
    {
        [Required]
        [Display(Name ="用户名")]
        [MinLength(5,ErrorMessage = "用户名最小长度不能低于5位")]
        [MaxLength(16, ErrorMessage = "账户名最大长度不能超过16位")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="密  码")]
        [MinLength(6, ErrorMessage = "密码最小长度不能低于6位")]
        [MaxLength(32, ErrorMessage = "密码最大长度不能超过32位")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 加密盐
        /// </summary>
        [Required]
        public string SaltKey { get; set; }

        [Required]
        [Display(Name = "用户类型")]
        [DefaultValue(0)]
        public int UserType { get; set; }

        [MaxLength(8)]
        [Display(Name = "真实姓名")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// QQ或微信
        /// </summary>
        public string QQorWeChat { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [IsEmail]
        public string Email { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// AccessToken，接入第三方登陆时用
        /// </summary>
        public string AccessToken { get; set; }

        public virtual ICollection<LoginRecord> LoginRecord { get; set; }
    }
}
