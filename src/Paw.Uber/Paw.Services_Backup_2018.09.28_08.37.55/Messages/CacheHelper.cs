using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Paw.Services.Messages
{
    public static class CacheHelper
    {

        public static int CacheResult<T>(string key, T value, CacheDependency cacheDependency = null, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null, CacheItemRemovedCallback cachedItemRemovedCallback = null) where T : class
        {
            if (absoluteExpiration == null)
            {
                absoluteExpiration = Cache.NoAbsoluteExpiration;
            }

            if (slidingExpiration == null)
            {
                slidingExpiration = Cache.NoSlidingExpiration;
            }

            if (string.IsNullOrEmpty(key) || value == null)
            {
                return 0;
            }

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            // Step 2. Exit if no httpContext
            if (httpContext == null)
            {
                return 0;
            }

            // Step 3. Add item to cache
            httpContext.Cache.Insert(key, value, cacheDependency, (DateTime)absoluteExpiration, (TimeSpan)slidingExpiration, CacheItemPriority.Default, cachedItemRemovedCallback);

            Log($"Add cache item: [{typeof(T).FullName}]({key})");

            Log($"Verify cache insert [{key}]: {httpContext.Cache[key]}");


            return 1;
        }


        public static bool TryGetResult<T>(string key, out T result) where T : class
        {
            result = null;

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return false;
            }

            // Step 2. Get cache object
            result = httpContext.Cache[key] as T;
            bool cacheHit = result != null;

            if (cacheHit)
            {
                Log($"Cache hit: [{typeof(T).FullName}]({key})");
            }
            else
            {
                Log($"Cache miss: [{typeof(T).FullName}]({key})");
            }

            // Step 5. return all
            return cacheHit;
        }

        public static bool TryGetIndexResult<T>(string key, out T result) where T : class
        {
            result = null;

            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            // Step 1. Get index
            string indexResult = string.Empty;
            if (TryGetResult<string>(key, out indexResult))
            {
                Log($"Cache index hit: {key}={indexResult}");

                return TryGetResult<T>(indexResult, out result);
            }

            Log($"Cache index miss: [{key}]");
            return false;

        }

        public static void Log(string message)
        {
            // string path = @"C:\Users\dwbanks\Documents\logs\paw_cache_log.txt";

            string path = @"";


            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return;
            }

            if (httpContext.Request.IsLocal && File.Exists(path))
            {
                File.AppendAllText(path, string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}\t{1}", DateTime.Now, message + "\r\n"));
            }
        }
    }
}
