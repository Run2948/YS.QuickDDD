/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：ExcelExport
* 创 建 者：Qing
* 创建时间：2018-07-01 17:25:16
* CLR 版本：4.0.30319.42000
* 保存的文件名：ExcelExport
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
using OfficeOpenXml;
using OfficeOpenXml.Extends;

namespace Quick.Common
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    public class ExcelExport
    {
        #region 构造函数、私有实用方法
        /// <summary>
        /// 由于此类仅提供一些静态方法，设置默认构造函数私有化，以阻止使用 "new" 来创建实例。
        /// </summary>
        internal ExcelExport()
        {
            // Do Nothing!
        }
        #endregion

        #region 静态方法

        #region 根据模板导出Excel文件(OfficeOpenXml格式操作)
        /// <summary>
        /// 根据模板导出Excel文件(OfficeOpenXml格式操作)
        /// </summary>
        /// <typeparam name="T">类对象</typeparam>
        /// <param name="t">对象实例</param>
        /// <param name="tplfileName">模板名称</param>
        /// <param name="fileName">要保存的文件名称</param>
        public static string ExportModelToExcel<T>(T t, string tplfileName, string fileName)
        {
            // 模板文件路径
            string tplfilePath = System.Web.HttpContext.Current.Server.MapPath("/upload/templates/") + tplfileName;
            // 导出文件的路径
            string optfilePath = System.Web.HttpContext.Current.Server.MapPath("/upload/excels/export/" + DateTime.Now.ToString("yyyyMMdd") + "/");
            try
            {
                FileInfo template = new FileInfo(tplfilePath);
                if (!template.Exists)
                {
                    return "no|模板文件不存在，请添加模板文件后再试!";
                }
                ExcelPackage packet = new ExcelPackage(template);
                var workbook = packet.Workbook;
                workbook.Worksheets.First().FillModel(t);
                if (!Directory.Exists(optfilePath))
                    Directory.CreateDirectory(optfilePath);

                var filePath = optfilePath + fileName;
                packet.SaveAs(new FileInfo(filePath));
                return "ok|" + filePath;
            }
            catch (Exception e)
            {
                return "no|" + e.Message;
            }
        }
        #endregion

        #endregion
    }
}
