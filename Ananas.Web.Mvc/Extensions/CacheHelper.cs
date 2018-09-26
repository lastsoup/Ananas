using System;
using Microsoft.Extensions.Caching.Memory;

namespace Ananas.Web.Mvc.Extensions
{
    public class CacheHelper
    {
         static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 获取缓存中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static object GetCacheValue(string key)
        {
            if ( !string.IsNullOrEmpty(key) && Cache.TryGetValue(key, out var val))
            {
                return val;
            }
            return default(object);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetCacheValue(string key, object value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Cache.Set(key, value, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromHours(1)
                });
            }
        }
    }
}