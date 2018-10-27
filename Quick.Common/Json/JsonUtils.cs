/* ==============================================================================
* 命名空间：Quick.Common.Json
* 类 名 称：JsonUtils
* 创 建 者：Qing
* 创建时间：2018-10-27 12:18:06
* CLR 版本：4.0.30319.42000
* 保存的文件名：JsonUtils
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



using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Json
{
    public class JsonUtils
    {
        public static string ConvertJson<T>(T value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return null;
            }
        }

        public static T ConvertObj<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                return default(T);
            }
        }

        public static T FileCovert<T>(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    var text = File.ReadAllText(fileName);
                    return ConvertObj<T>(text);
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string SaveFile<T>(T list, string fileName)
        {
            try
            {
                if (list != null)
                {
                    string text = ConvertJson(list);
                    File.WriteAllText(fileName, text, Encoding.UTF8);
                }
                return fileName;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
