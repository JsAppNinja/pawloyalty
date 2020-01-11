using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Paw.Services.Util;
using Paw.Services.Attributes;

namespace Paw.Services.Messages.Web.Pets
{
    public class GetPet : IGetProviderGroup<Pet>
    {
        [GetModelProperty(ParameterName = "PetId")]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [GetViewDataValue(ParameterName = "__ProviderGroupId")]
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;


        public Pet ExecuteItem(bool useCache = true)
        {
            ProviderGroup cacheResult = null;
            if (useCache && CacheHelper.TryGetResult<ProviderGroup>(this.ProviderGroupId.ToString(), out cacheResult))
            {
                Pet cacheItem = cacheResult.PetCollection.Where(x => x.Id == this.Id).SingleOrDefault();
                if (cacheItem != null)
                {
                    Logger.Info($"Returned [{typeof(Pet)}]({cacheItem.Id}) from cache graph.");
                    return cacheItem;
                }
            }
            
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                return dataContext.PetSet
                    .Include("Owner.PetCollection.Breed")
                    .Where(x => x.Id == this.Id && x.ProviderGroupId == this.ProviderGroupId)
                   .SingleOrDefault();
            }
        }
    }
}
