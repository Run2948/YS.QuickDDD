/* ==============================================================================
* 命名空间：Quick.Data.Entities.Sys
* 类 名 称：Interview
* 创 建 者：Qing
* 创建时间：2018-10-26 18:06:32
* CLR 版本：4.0.30319.42000
* 保存的文件名：Interview
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
    /// 访客信息
    /// </summary>
    [Table("Interview")]
    public class Interview : GeneratedId
    {
        public Interview()
        {
            ViewTime = DateTime.Now;
            Uid = Guid.NewGuid();
            InterviewDetails = new HashSet<InterviewDetail>();
        }

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// UA
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OperatingSystem { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string BrowserType { get; set; }

        /// <summary>
        /// 来访时间
        /// </summary>
        public DateTime ViewTime { get; set; }

        /// <summary>
        /// 来自哪里
        /// </summary>
        public string FromUrl { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// ISP
        /// </summary>
        public string ISP { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 详细地理位置
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 参考地理位置
        /// </summary>
        public string ReferenceAddress { get; set; }

        /// <summary>
        /// 着陆页
        /// </summary>
        public string LandPage { get; set; }

        /// <summary>
        /// 在线时长
        /// </summary>
        public string OnlineSpan { get; set; }

        /// <summary>
        /// 在线时长秒数
        /// </summary>
        public double OnlineSpanSeconds { get; set; }

        public ICollection<InterviewDetail> InterviewDetails { get; set; }
    }
}
