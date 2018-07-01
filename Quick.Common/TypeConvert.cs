/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：TypeConvert
* 创 建 者：Qing
* 创建时间：2018-07-01 18:03:03
* CLR 版本：4.0.30319.42000
* 保存的文件名：TypeConvert
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    public static class TypeConvert
    {
        #region ToMd5

        public static string ToMd5(this string origin)
        {
            if (string.IsNullOrWhiteSpace(origin))
            {
                return string.Empty;
            }

            var md5Algorithm = MD5.Create();
            var utf8Bytes = Encoding.UTF8.GetBytes(origin);
            var md5Hash = md5Algorithm.ComputeHash(utf8Bytes);
            var hexString = new StringBuilder();
            foreach (var hexByte in md5Hash)
            {
                hexString.Append(hexByte.ToString("x2"));
            }
            return hexString.ToString();
        }

        #endregion

        #region ToBoolean
        /// <summary>
        /// ToBoolean 【string】
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string flag)
        {
            try
            {
                return Convert.ToBoolean(flag);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// ToBoolean 【object】
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object flag)
        {
            return ToBoolean(flag.ToString());
        }
        #endregion

        #region ToInt32
        /// <summary>
        /// ToInt32 【string】
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ToInt32(this string num)
        {
            try
            {
                return Convert.ToInt32(num);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// ToInt32 【object】
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ToInt32(this object num)
        {
            return ToInt32(num.ToString());
        }

        #endregion

        #region ToInt64
        /// <summary>
        /// ToInt64 [string]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Int64 ToInt64(this string num)
        {
            try
            {
                return Convert.ToInt64(num);
            }
            catch (Exception)
            {

                return -1;
            }
        }

        /// <summary>
        /// ToInt64 [object]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Int64 ToInt64(this object num)
        {
            return ToInt64(num.ToString());
        }
        #endregion

        #region ToDouble

        /// <summary>
        /// 将数据转换为双精度浮点型   转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDouble(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位   转换失败返回默认值
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, int decimals, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                return defValue;
            }
        }



        /// <summary>
        /// 将数据转换为双精度浮点型  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(object data, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToDouble(data);
            }
            catch
            {
                return defValue;
            }

        }

        /// <summary>
        /// 将数据转换为双精度浮点型  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(string data, double defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            double temp = 0;

            if (double.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }

        }

        /// <summary>
        /// 将数据转换成时间类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime ToDataTime(object o)
        {
            //如果为空则返回默认值
            if (o == null)
            {
                return DateTime.MinValue;
            }

            try
            {
                return DateTime.Parse(o.ToString());
            }
            catch
            {
                return DateTime.MinValue;
            }


        }

        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(object data, int decimals, double defValue)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(string data, int decimals, double defValue)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            double temp = 0;

            if (double.TryParse(data, out temp))
            {
                return Math.Round(temp, decimals);
            }
            else
            {
                return defValue;
            }
        }


        #endregion

        #region ToGuid

        /// <summary>
        /// ToGUid [string]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string num)
        {
            try
            {
                return new Guid(num);
            }
            catch (Exception)
            {

                return new Guid();
            }
        }
        /// <summary>
        /// ToGuid [object]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object num)
        {
            return ToGuid(num.ToString());
        }
        #endregion

        #region ToDateTime
        /// <summary>
        /// ToDateTime  [string]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string num)
        {
            try
            {
                return DateTime.Parse(num);
            }
            catch (Exception)
            {

                return default(DateTime);
            }
        }
        /// <summary>
        /// ToDateTime [object]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object num)
        {
            return ToDateTime(num.ToString());
        }
        #endregion

        #region ToSecond
        /// <summary>
        /// ToSecond [string]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ToSecond(this string num)
        {
            try
            {
                if (!num.Contains(":") && !num.Contains("：") && !num.Contains("."))
                    return num.ToInt32();

                if (num.Length != 5 && num.Length != 8)
                    return -1;
                if (num.Length != 8)
                    num = "00:" + num;
                string[] array = num.Split(':', '：', '.');
                return array[0].ToInt32() * 3600 + array[1].ToInt32() * 60 + array[2].ToInt32();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// ToSecond [object]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static long ToSecond(this object num)
        {
            return ToSecond(num.ToString());
        }
        #endregion

        #region ToPlayTime
        /// <summary>
        /// ToSecond [string]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToPlayTime(this int num)
        {
            try
            {
                return
                    $"{(num / 3600).ToString().PadLeft(2, '0')}:{(num / 60 % 60).ToString().PadLeft(2, '0')}:{(num % 60).ToString().PadLeft(2, '0')}";
            }
            catch (Exception)
            {
                return "00:00:00";
            }
        }

        /// <summary>
        /// ToSecond [object]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static long ToPlayTime(this object num)
        {
            return ToSecond(num.ToInt32());
        }
        #endregion

        #region ToRound
        public static double ToRound(this double value, int digists)
        {
            try
            {
                return Math.Round(value, digists);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ToRound(this double value)
        {
            return ToRound(value, 2);
        }
        #endregion

        #region ToDouble
        public static double ToDouble(this string num, int digists)
        {
            try
            {
                return Convert.ToDouble(num).ToRound(digists);
            }
            catch (Exception)
            {
                return 0.00;
            }
        }

        public static double ToDouble(this string num)
        {
            try
            {
                return Convert.ToDouble(num).ToRound();
            }
            catch (Exception)
            {
                return 0.00;
            }
        }
        #endregion

        #region ConvertToAnyType
        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return null;
            }

            Type type2 = data.GetType();
            if (targetType == type2)
            {
                return data;
            }
            if (((targetType == typeof(Guid)) || (targetType == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(data.ToString()))
                {
                    return null;
                }
                return new Guid(data.ToString());
            }

            if (targetType.IsEnum)
            {
                try
                {
                    return Enum.Parse(targetType, data.ToString(), true);
                }
                catch
                {
                    return Enum.ToObject(targetType, data);
                }
            }

            if (targetType.IsGenericType)
            {
                targetType = targetType.GetGenericArguments()[0];
            }

            return Convert.ChangeType(data, targetType);
        }

        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        public static T ConvertTo<T>(object data)
        {
            if (data == null || Convert.IsDBNull(data))
                return default(T);

            object obj = ConvertTo(data, typeof(T));
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }
        #endregion
    }
}
