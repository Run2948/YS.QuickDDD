/* ==============================================================================
* 命名空间：Quick.Data.Entities.Enum
* 类 名 称：Status
* 创 建 者：Qing
* 创建时间：2018-10-29 18:13:22
* CLR 版本：4.0.30319.42000
* 保存的文件名：Status
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Entities.Enum
{
    /// <summary>
    /// 数据状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Display(Name = "默认")] Default,

        /// <summary>
        /// 可用
        /// </summary>
        [Display(Name = "可用")] Available,

        /// <summary>
        /// 禁用
        /// </summary>
        [Display(Name = "禁用")] Forbidden,

        /// <summary>
        /// 已删除
        /// </summary>
        [Display(Name = "已删除")] Deleted,

        /// <summary>
        /// 审核中
        /// </summary>
        [Display(Name = "审核中")] Pending,

        /// <summary>
        /// 已发表
        /// </summary>
        [Display(Name = "已发表")] Pended,

        /// <summary>
        /// 订阅中
        /// </summary>
        [Display(Name = "订阅中")] Subscribing,

        /// <summary>
        /// 订阅成功
        /// </summary>
        [Display(Name = "订阅成功")] Subscribed,

        /// <summary>
        /// 显示
        /// </summary>
        [Display(Name = "显示")] Display,

        /// <summary>
        /// 隐藏
        /// </summary>
        [Display(Name = "隐藏")] Hidden,

        /// <summary>
        /// 定时发表
        /// </summary>
        [Display(Name = "定时发表")] Schedule,

        /// <summary>
        /// 已取消
        /// </summary>
        [Display(Name = "已取消")] Canceled,

        /// <summary>
        /// 待处理
        /// </summary>
        [Display(Name = "待处理")] WaitingHandle,

        /// <summary>
        /// 已处理
        /// </summary>
        [Display(Name = "已处理")] Handled,
        /// <summary>
        /// 不可用
        /// </summary>
        [Display(Name = "不可用")] Unavailable
    }
}
