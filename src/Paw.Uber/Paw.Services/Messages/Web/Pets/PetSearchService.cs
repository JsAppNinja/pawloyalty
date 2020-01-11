using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Paw.Services.Messages.Web.Pets
{
    [Obsolete]
    public class PetSearchService
    {
        public static int GetPetLinkListExpirationInSeconds()
        {
            return 40;
        }

        public static int GetActiveProviderGroupSlidingExpirationInMinutes()
        {
            return 10;
        }

        public static List<PetLink> Search(Guid providerGoupId, string query = "")
        {
            List<PetLink> providerGroupPetList = null;

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            // Step 2. Get cache object
            bool cacheHit = false;
            if (httpContext != null)
            {
                // Mark Active
                httpContext.Cache.Add(GetActiveProviderGroupCacheKey(providerGoupId), true, null, Cache.NoAbsoluteExpiration, new TimeSpan(GetActiveProviderGroupSlidingExpirationInMinutes()), CacheItemPriority.Default, null);

                providerGroupPetList = httpContext.Cache[GetPetLinkListCacheKey(providerGoupId)] as List<PetLink>;
                cacheHit = providerGroupPetList != null;
            }

            // Step 3. Get PetList if necessary
            if (!cacheHit)
            {
                providerGroupPetList = GetPetLinkList(providerGoupId);
            }

            // Step 4. 
            if (!string.IsNullOrEmpty(query))
            {
                
                    query = query.Trim();
                    return providerGroupPetList.FindAll(x => x.Pet.StartsWith(query, StringComparison.InvariantCultureIgnoreCase) || x.Owner.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Any(y => y.StartsWith(query, StringComparison.InvariantCultureIgnoreCase)));
                
                
            }
            
            // Step 5. Find All
            return providerGroupPetList;
            
        }
        

        #region Cache Keys ....

        public static string GetPetLinkListCacheKey(Guid providerGroupId)
        {
            return string.Format("PetList_{0}", providerGroupId);
        }

        public static string GetActiveProviderGroupCacheKey(Guid providerGroupId)
        {
            return string.Format("ActiveProviderGroup_{0}", providerGroupId);
        }

        public static Guid? GetPetLinkListCacheKeyProviderGroupId(string input)
        {
            var s = input.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            if (s.Length < 2) return null;

            Guid result;
            if(Guid.TryParse(s[1], out result))
            {
                return result;
            }

            return (Guid?)null;
        }

        #endregion

        #region Get List and Cache Insert ...

        public static List<PetLink> GetPetLinkList(Guid providerGroupId)
        {
            // Step 1. Get List
            List<PetLink> petLinkList = new GetPetLinkList() { ProviderGroupId = providerGroupId }.ExecuteList();

            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                // Active Dependency
                CacheDependency cacheDependency = new CacheDependency(null, new string[] { GetActiveProviderGroupCacheKey(providerGroupId) });
                
                httpContext.Cache.Insert(GetPetLinkListCacheKey(providerGroupId), petLinkList, null, DateTime.Now.AddSeconds(GetPetLinkListExpirationInSeconds()), Cache.NoSlidingExpiration, PetLinkListCacheItemUpdateCallbackMethod);
            }

            return petLinkList;
        }
        
        public static void PetLinkListCacheItemUpdateCallbackMethod(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            // Step 0.
            expensiveObject = null;
            dependency = null;
            absoluteExpiration = DateTime.Now.AddSeconds(GetPetLinkListExpirationInSeconds());
            slidingExpiration = Cache.NoSlidingExpiration;

            if (DateTime.Now.Minute % 10 == 0)
            {
                return; // do nothing
            }

            // Step 1. Parse Key
            Guid? providerGroupId = GetPetLinkListCacheKeyProviderGroupId(key);
            if (providerGroupId != null)
            {
                expensiveObject = new GetPetLinkList() { ProviderGroupId = providerGroupId.Value }.ExecuteList();
            }
        }
        
        #endregion
        
    }
}
