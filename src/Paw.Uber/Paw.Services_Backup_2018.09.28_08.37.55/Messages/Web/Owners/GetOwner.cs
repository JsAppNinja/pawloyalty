using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Owners
{
    public class GetOwner : IGetProviderGroup<Owner>
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;
        
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public Owner ExecuteItem(bool useCache = true)
        {
            ProviderGroup cacheResult = null;
            if (useCache && CacheHelper.TryGetResult<ProviderGroup>(this.ProviderGroupId.ToString(), out cacheResult))
            {
                Owner cacheItem = cacheResult.OwnerCollection.Where(x => x.Id == this.Id).SingleOrDefault();
                if (cacheItem != null)
                {
                    CacheHelper.Log($"Returned [{typeof(Owner)}]({cacheItem.Id}) from cache graph.");
                    return cacheItem;
                }
            }
            
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context
                    .OwnerSet
                    .Include("PetCollection")
                    .Include("SchedulerEventCollection.SkuCategory") // NOTE: that this should be in a separate call (as owners belong to provider groups, while events belong to provders)
                    .Where(x => x.Id == this.Id && x.ProviderGroupId == this.ProviderGroupId)
                    .SingleOrDefault();
            }
        }
    }
}
