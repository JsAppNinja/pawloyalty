using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Omu.ValueInjecter;
using System.Collections;

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

            Logger.Info($"Add cache item: [{typeof(T).FullName}]({key})");

            Logger.Info($"Verify cache insert [{key}]: {httpContext.Cache[key]}");


            return 1;
        }
        

        public static bool TryGetResult<T>(string key, out T result)
        {
            result = default(T);

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return false;
            }

            // Step 2. Get cache object
            result = (T)httpContext.Cache[key];
            bool cacheHit = result != null;

            if (cacheHit)
            {
                Logger.Info($"Cache hit: [{typeof(T).FullName}]({key})");
            }
            else
            {
                Logger.Info($"Cache miss: [{typeof(T).FullName}]({key})");
            }

            // Step 5. return all
            return cacheHit;
        }

        public static bool AddUpdateItem<T,C>(string key, object message, Func<T,C> accessor) 
            where T : class
            where C : class
        {
            T cacheItem;
            if (TryGetResult<T>(key, out cacheItem))
            {
                C item = accessor(cacheItem);
                if (item != null)
                {
                    cacheItem.InjectFrom<CommonValueInjection>(message);
                }
            }

            return false;
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
                Logger.Info($"Cache index hit: {key}={indexResult}");

                return TryGetResult<T>(indexResult, out result);
            }

            Logger.Info($"Cache index miss: [{key}]");
            return false;

        }

        public static List<string> GetCacheKeyList(Predicate<string> match = null)
        {
            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return new List<string>();
            }

            List<string> result = new List<string>();

            IDictionaryEnumerator enumerator = httpContext.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Key.ToString());
            }

            if (match != null)
            {
                return result.FindAll(match);
            }

            return result;
        }

        #region ...

        public static List<Guid> GetActiveProviderIdList(int windowInMinutes = 40)
        {
            // Step 1. Read ActiveProvide Keys
            List<string> keyList = CacheHelper.GetCacheKeyList(x => x.StartsWith("ActiveProvider_"));

            // Step 2. Filter active providers inside the window
            DateTime now = DateTime.UtcNow;
            List<string> activeKeyList = keyList.FindAll(x => {
                DateTime activeDateTime;
                if (!CacheHelper.TryGetResult<DateTime>(x, out activeDateTime))
                {
                    return false;
                }

                if ((DateTime.UtcNow - activeDateTime).TotalMinutes > windowInMinutes)
                {
                    return false;
                }

                return true;

            });

            // Step 3. Get ProviderList
            List<Guid> activeProviderIdList = activeKeyList.ConvertAll(x => new Guid(x.Split('_')[1])).ToList();

            return activeProviderIdList;

        }

        public static void SetActiveProvider(Guid providerId)
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return;
            }

            httpContext.Cache.Insert($"ActiveProvider_{providerId}", DateTime.UtcNow, null, Cache.NoAbsoluteExpiration, new TimeSpan(2,0,0));
        }

        public static void SetActiveProviderGroup(Guid providerGroupId)
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return;
            }

            httpContext.Cache.Insert($"ActiveProviderGroup_{providerGroupId}", DateTime.UtcNow, null, Cache.NoAbsoluteExpiration, new TimeSpan(2, 0, 0));
        }

        #endregion

    }
}
