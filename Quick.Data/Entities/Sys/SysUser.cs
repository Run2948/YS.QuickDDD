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

namespace Quick.Data.Entities.Sys
{
    [Serializable]
    public class SysUser : BaseEntity
    {
        [Required]
        [Display(Name ="用户名")]
        //[MinLength(5,ErrorMessage = "用户名最小长度不能低于5位")]
        [MaxLength(16, ErrorMessage = "账户名最大长度不能超过16位")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="密  码")]
        [MinLength(6, ErrorMessage = "密码最小长度不能低于6位")]
        [MaxLength(32, ErrorMessage = "密码最大长度不能超过32位")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "用户类型")]
        [DefaultValue(0)]
        public int UserType { get; set; }

        [MaxLength(8)]
        [Display(Name = "真实姓名")]
        public string NickName { get; set; }
    }
}
