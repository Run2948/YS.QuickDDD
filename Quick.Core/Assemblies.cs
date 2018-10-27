/* ==============================================================================
* 命名空间：Quick.Core
* 类 名 称：Assemblies
* 创 建 者：Qing
* 创建时间：2018-10-26 15:52:16
* CLR 版本：4.0.30319.42000
* 保存的文件名：Assemblies
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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Core
{
    /// <summary>
    /// 程序集缓存
    /// </summary>
    public static class Assemblies
    {
        public static string ServiceAssembly { get; set; } = ConfigurationManager.AppSettings["BllPath"] ?? "Quick.Service";
        public static string RepositoryAssembly { get; set; } = ConfigurationManager.AppSettings["DalPath"] ?? "Quick.Repository";
    }
}
