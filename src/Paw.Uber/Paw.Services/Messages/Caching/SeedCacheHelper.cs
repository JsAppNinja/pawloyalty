using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Paw.Services.Messages.Web.Providers;
using System.Web.Caching;

namespace Paw.Services.Messages.Caching
{
    public static class SeedCacheHelper
    {
        public static void CacheRefresh(Action<Guid> action)
        {
            // Step 1. Get Cache Keys
            List<Guid> activeProviderIdList = CacheHelper.GetActiveProviderIdList(40);

            // Step 2. Action
            foreach (Guid id in activeProviderIdList)
            {
                action(id);
            }

        }

        #region KeyExtensions ...

        public static Guid GetProviderId(this string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = string.Empty;
            }

            List<string> tokenList = key.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            Guid result;
            if (tokenList.Count < 3 || Guid.TryParse(tokenList[3], out result))
            {
                throw new InvalidOperationException("Unable to parse Provider key.");
            }

            return result;
        }



        #endregion

        #region CallBacks ...

        public static void SeedProvider(Guid id)
        {
            ProviderCallback($"CacheItem_Provider_{id}", null, CacheItemRemovedReason.Expired);
        }

        public static void ProviderCallback(string key, object value, CacheItemRemovedReason reason)
        {
            // Step 1. Exit if no httpContext
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return;
            }

            // Step 2. Get ProviderId
            Guid providerId = key.GetProviderId();

            // Step 3. Exit
            if (reason == CacheItemRemovedReason.Removed)
            {
                return;
            }

            // Step 4. IsLastAccessCurrent
            if (!IsLastAccessCurrent("Provider", providerId))
            {
                return;
            }
            
            // Step 5. Get Provider
            Provider provider = new GetProvider() { Id = providerId }.ExecuteItemCore();

            if (provider == null)
            {
                throw new InvalidOperationException("Provider not found.");
            }

            // Step 6. Insert Cache
            CacheDependency cacheDependency = null;
            DateTime? absoluteExpiration = Cache.NoAbsoluteExpiration;
            TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
            
            httpContext.Cache.Insert(key, value, cacheDependency, (DateTime)absoluteExpiration, (TimeSpan)slidingExpiration, CacheItemPriority.Default, ProviderCallback);
        }

        
        public static void BreedListCallback(string key, object value, CacheItemRemovedReason reason)
        {
            // Step 1. Exit if no httpContext
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return;
            }
            
            // Step 6. Insert Cache
            CacheDependency cacheDependency = null;
            DateTime? absoluteExpiration = Cache.NoAbsoluteExpiration;
            TimeSpan slidingExpiration = Cache.NoSlidingExpiration;

            httpContext.Cache.Insert(key, value, cacheDependency, (DateTime)absoluteExpiration, (TimeSpan)slidingExpiration, CacheItemPriority.Default, ProviderCallback);
        }

        #endregion

        // Used for 
        public static T SeedProviderItem<T>(Guid id, Guid providerId, Func<Guid,Guid,T> callback) where T : class, IId, IProviderId
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return null; ;
            }

            // Step 1. Execute Callback
            T result = callback(id, providerId);

            // Step 2. Set LastAccess
            SetLastAccess("Provider", id);

            // Step 3. Set


            return result;
        }

        

        #region LastAccess ...

        public static string GetLastAccessKey(string typeName, Guid id)
        {
            return $"LastAccess_{typeName}_{id}";
        }

        public static void SetLastAccess(string typeName, Guid id)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return;
            }

            httpContext.Cache.Insert(GetLastAccessKey(typeName, id), DateTime.UtcNow);
        }

        public static T SetLastAcccess<T>(this T item) where T : IId
        {
            SetLastAccess(typeof(T).Name, item.Id);
            return item;
        }

        public static DateTime? GetLastAccess(string typeName, Guid id)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return null;
            }

            return (DateTime?)httpContext.Cache[GetLastAccessKey(typeName, id)]; 
        }

        public static bool IsLastAccessCurrent(string typeName, Guid id)
        {
            DateTime? dateTime = GetLastAccess(typeName, id);

            if (dateTime == null)
            {
                return false;
            }

            return (DateTime.UtcNow - dateTime.Value).TotalHours > 2;
        }

        #endregion

        // Step 1. Seed Provider

        // Step 2. Update Provider LastAccess

        // Step 3. Fire Callback (Check Last Access, Insert Cache)

    }
}
