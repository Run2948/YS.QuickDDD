/* ==============================================================================
* 命名空间：Quick.Core.Binders
* 类 名 称：TrimToDBCModelBinder
* 创 建 者：Qing
* 创建时间：2018-10-26 17:08:21
* CLR 版本：4.0.30319.42000
* 保存的文件名：TrimToDBCModelBinder
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quick.Core.Binders
{
    /// <summary>
    /// 自动截取空格和进行全角转换
    /// </summary>
    public class TrimToDBCModelBinder : DefaultModelBinder
    {
        //一定要使用using System.Web.Mvc下的DefaultModelBinder
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object value = base.BindModel(controllerContext, bindingContext);
            if (value is string)
            {
                string strValue = (string)value;
                string value2 = ToDBC(strValue).Trim();
                return value2;
            }
            else
            {
                return value;
            }
        }

        /// <summary> 全角转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        private static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }
            return new string(c);
        }
    }
}
