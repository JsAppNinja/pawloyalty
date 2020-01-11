using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Caching;
using Paw.Services.Util;

namespace Paw.Services.Messages.Web.Providers
{
    public class GetProviderByDomain
    {
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }
        private string _Domain = String.Empty;

        public Provider ExecuteItem(bool useCache = true)
        {

            if (string.IsNullOrEmpty(this.Domain))
            {
                return null;
            }

            Provider cacheResult = null;
            if (useCache && CacheHelper.TryGetIndexResult<Provider>(GetCacheKey(this.Domain), out cacheResult))
            {
                return cacheResult;
            }

            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                // Step 1. Get the providerId
                Guid? providerId = dataContext
                    .ProviderSet
                    .Where(x => x.Domain == this.Domain)
                    .Select(x => x.Id)
                    .SingleOrDefault();

                if (providerId == null)
                {
                    return null;
                }

                // Step 2. Call GetProvider (Handles caching value)
                Provider result = new GetProvider() { Id = providerId.Value }.ExecuteItem(useCache);

                // Step 2. Add to cache by Domain, cache dependency on Id
                // cacheDependency: new CacheDependency(null, new string[] { this.Id.ToString() }), 
                CacheHelper.CacheResult<string>(GetProviderByDomain.GetCacheKey(result.Domain), result.Id.ToString(), slidingExpiration: new TimeSpan(0, 20, 0), cachedItemRemovedCallback: CacheItemRemovedCallback);

                return result;
            }
        }

        public static string GetCacheKey(string domain)
        {
            return "ProviderDomain_" + domain;
        }

        protected static void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            Logger.Info($"CacheItemRemovedCallback({reason.ToString()}): [{typeof(Provider).FullName}]({key})[{value}]");
        }
    }
}
