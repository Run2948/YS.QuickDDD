/* ==============================================================================
* 命名空间：Quick.Core
* 类 名 称：JsonHelper
* 创 建 者：Qing
* 创建时间：2018-10-27 12:18:47
* CLR 版本：4.0.30319.42000
* 保存的文件名：JsonHelper
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
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Quick.Common.Json;

namespace Quick.Core
{
    /// <summary>
    /// JSON序列化和反序列化辅助类
    /// </summary>
    public class JsonHelper : JsonUtils
    {
        /// <summary> 
        /// Json序列化 
        /// </summary> 
        public static string JsonSerializer<T>(T obj)
        {

            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary> 
        /// Json反序列化
        /// </summary> 
        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        // 将 DataTable 序列化成 json 字符串
        public static string DataTableToJson(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "\"\"";
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return myJson.Serialize(list);
        }

        // 将对象序列化成 json 字符串
        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.Serialize(obj);
        }

        // 将 json 字符串反序列化成对象
        public static object JsonToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.DeserializeObject(json);
        }

        // 将 json 字符串反序列化成对象
        public static T JsonToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.Deserialize<T>(json);
        }
    }
}
