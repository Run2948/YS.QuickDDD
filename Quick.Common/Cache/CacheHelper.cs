/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：CacheHelper
* 创 建 者：Qing
* 创建时间：2018-07-01 17:35:52
* CLR 版本：4.0.30319.42000
* 保存的文件名：CacheHelper
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
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace Quick.Common
{
    public class CacheHelper
    {
        /// <summary>
        /// 创建缓存项的文件
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        public static void Insert(string key, object obj)
        {
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj);
        }

        /// <summary>
        /// 移除缓存项的文件
        /// </summary>
        /// <param name="key">缓存Key</param>
        public static void Remove(string key)
        {
            //移除缓存
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //创建缓存依赖项
            CacheDependency dep = new CacheDependency(fileName);
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        /// <summary>
        /// 创建缓存项过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void Insert(string key, object obj, int expires)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>object对象</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            try
            {
                return HttpContext.Current.Cache.Get(key);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">T对象</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }

        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string cacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveAllCache(string cacheKey)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}