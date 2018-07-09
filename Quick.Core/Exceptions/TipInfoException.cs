/* ==============================================================================
* 命名空间：Quick.Core.Exceptions
* 类 名 称：TipInfoException
* 创 建 者：Qing
* 创建时间：2018-07-09 15:00:57
* CLR 版本：4.0.30319.42000
* 保存的文件名：TipInfoException
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

namespace Quick.Core.Exceptions
{
    /// <summary>
    /// 业务异常
    /// </summary>
    [Serializable]
    public class TipInfoException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipMsg">是用于显示给用户看的首要信息</param>
        /// <param name="errorInfo">所有需要附加给用户知晓的，附加给调用方进行处理的额外信息，通常通过约定进行内容获取的处理</param>
        public TipInfoException(string tipMsg, object errorInfo = null) : base(tipMsg)
        {
            InitErrorInfo(errorInfo);
        }

        public TipInfoException(string tipMsg, Exception innerException, object errorInfo = null) : base(tipMsg, innerException)
        {
            InitErrorInfo(errorInfo);
        }

        private void InitErrorInfo(object errorInfo)
        {
            Data["TipForUI"] = errorInfo;
        }
    }
}
