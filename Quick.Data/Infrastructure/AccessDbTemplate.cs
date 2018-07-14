/* ==============================================================================
* 命名空间：Quick.Data.Infrastructure
* 类 名 称：AccessDbTemplate
* 创 建 者：Qing
* 创建时间：2018-07-14 19:04:53
* CLR 版本：4.0.30319.42000
* 保存的文件名：AccessDbTemplate
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Infrastructure
{
    public static class AccessDbTemplate
    {
        private static readonly string ACCDB = AppDomain.CurrentDomain.BaseDirectory + "/Config/Quick_V1.0.accdb";

        private static readonly string MDB = AppDomain.CurrentDomain.BaseDirectory + "/Config/Quick_V1.0.mdb";

        public static string DbTemplate(this string filePath)
        {
            var ext = Path.GetExtension(filePath).ToLower();
            return ext == ".accdb" ? ACCDB : MDB;
        }
    }
}
