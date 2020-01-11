using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Caching;

namespace Paw.Services.Messages.Web.Providers
{
    public class GetProvider
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;


        public Provider ExecuteItem(bool useCache = true)
        {
            Provider cacheResult = null;

            if (useCache && CacheHelper.TryGetResult<Provider>(this.Id.ToString(), out cacheResult))
            {
                return cacheResult;
            }

            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                Provider result = dataContext
                    .ProviderSet
                    .Include(x => x.ProviderGroup)
                    .Include("SkuCategoryCollection.SchedulerType")
                    .Include("EmployeeCollection")
                    .Include("ResourceCollection")
                    .Where(x => x.Id == this.Id).SingleOrDefault();


                    // Step 1. Add to cache by Id
                    CacheHelper.CacheResult<Provider>(this.Id.ToString(), result, absoluteExpiration: DateTime.Now.AddMinutes(10), slidingExpiration: null, cachedItemRemovedCallback: CacheItemRemovedCallback);


                return result;
            }
        }

        public static void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            CacheHelper.Log($"CacheItemRemovedCallback({reason.ToString()}): [{typeof(Provider).FullName}]({key})");
            if (reason == CacheItemRemovedReason.Expired)
            {
                Provider provider = value as Provider;
                if (provider != null)
                {
                    new GetProvider() { Id = provider.Id }.ExecuteItem(false);
                }
            }
        }
    }
}
