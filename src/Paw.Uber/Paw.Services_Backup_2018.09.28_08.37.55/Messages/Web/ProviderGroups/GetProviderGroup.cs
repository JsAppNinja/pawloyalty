using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Paw.Services.Messages.Web.ProviderGroups
{
    public class GetProviderGroup
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        public ProviderGroup ExecuteItem(bool useCache = true)
        {
            ProviderGroup cacheResult = null;

            if (useCache && CacheHelper.TryGetResult<ProviderGroup>(this.Id.ToString(), out cacheResult))
            {
                return cacheResult;
            }

            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                ProviderGroup result = dataContext
                    .ProviderGroupSet
                    .Include("PetCollection.Breed")
                    .Include("OwnerCollection.SchedulerEventCollection.SkuCategory")
                    .Include("BreedCollection")
                    .Where(x => x.Id == this.Id).SingleOrDefault();


                // Step 1. Add to cache by Id
                CacheHelper.CacheResult<ProviderGroup>(this.Id.ToString(), result, slidingExpiration: new TimeSpan(0, 20, 0), cachedItemRemovedCallback: (key, value, reason) =>
                        {
                            CacheHelper.Log($"CacheItemRemovedCallback({reason.ToString()}): [{typeof(Provider).FullName}]({key})");
                            if (reason != CacheItemRemovedReason.Underused)
                            {
                                GetProviderGroup providerGroup = value as GetProviderGroup;
                                if (providerGroup != null)
                                {
                                    new GetProviderGroup() { Id = providerGroup.Id }.ExecuteItem(false);
                                }
                            }
                        });



                return result;
            }
        }
    }
}
