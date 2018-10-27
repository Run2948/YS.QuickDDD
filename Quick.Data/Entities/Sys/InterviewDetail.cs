/* ==============================================================================
* 命名空间：Quick.Data.Entities.Sys
* 类 名 称：InterviewDetail
* 创 建 者：Qing
* 创建时间：2018-10-26 18:06:55
* CLR 版本：4.0.30319.42000
* 保存的文件名：InterviewDetail
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Entities.Sys
{
    /// <summary>
    /// 访客浏览详情
    /// </summary>
    [Table("InterviewDetail")]
    public class InterviewDetail : GeneratedId
    {
        public InterviewDetail()
        {
            Time = DateTime.Now;
        }

        /// <summary>
        /// 访问过的页面
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 关联的访客表ID
        /// </summary>
        [ForeignKey("Interview")]
        public long InterviewId { get; set; }

        public virtual Interview Interview { get; set; }
    }
}
