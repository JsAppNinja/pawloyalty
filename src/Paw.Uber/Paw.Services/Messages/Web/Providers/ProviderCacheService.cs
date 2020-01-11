using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Paw.Services.Messages.Web.Providers
{
    public class ProviderCacheService
    {
        public static int ProviderListExpirationInSeconds
        {
            get { return 40; }
        }

        public static int ProviderSlidingExpirationInMinutes
        {
            get { return 30; }
        }

        public static List<Provider> GetProviderList()
        {
            List<Provider> result = null;

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            // Step 2. Get cache object
            bool cacheHit = false;
            if (httpContext != null)
            {
                // Mark Active
                httpContext.Cache.Add(ProviderCacheService.ProviderDomainListCacheKey, true, null, Cache.NoAbsoluteExpiration, new TimeSpan(ProviderCacheService.ProviderSlidingExpirationInMinutes), CacheItemPriority.Default, null);

                result = httpContext.Cache[ProviderCacheService.ProviderDomainListCacheKey] as List<Provider>;
                cacheHit = result != null;
            }

            // Step 3. Get list if necessary
            if (!cacheHit)
            {
                result = new GetProviderList().ExecuteList();
            }
            
            // Step 5. return all
            return result;

        }

        public static Provider GetProvider(string domain)
        {
            if (string.IsNullOrEmpty(domain))
            {
                return null;
            }

            return GetProviderList().Find(x => x.Domain == domain);
        }

        public static Provider GetProvider(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            return GetProviderList().Find(x => x.Id == id);
        }


        public static string ProviderDomainListCacheKey = "ProviderDomainList";

    }
}
