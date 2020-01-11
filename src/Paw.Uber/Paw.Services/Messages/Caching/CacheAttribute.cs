using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Paw.Services.Messages.Caching
{
    public class CacheAttribute : Attribute
    {
        public virtual string GetCacheKey(object item)
        {
            return string.Empty;
        }


        public CacheTypes CacheType
        {
            get { return _CacheType; }
            set { _CacheType = value; }
        }
        private CacheTypes _CacheType = CacheTypes.Item;
        

        public virtual bool AddItem<T>(T item) where T : class, IId
        {
            // Step 1. Get Cache
            Cache cache = CacheHelper2.GetCache();

            if (cache == null)
            {
                return false;
            }

            // Step 2. 
            string cacheKey = this.GetCacheKey(item);

            // Step 3. 
            switch (this.CacheType)
            {
                case CacheTypes.Item:
                    var itemContainer = CacheHelper2.GetCacheValue<CacheItemContainer<T>>(cacheKey);
                    itemContainer.Refresh();
                    break;
                case CacheTypes.ItemList:
                    var listContainer = CacheHelper2.GetCacheValue<CacheListContainer<T>>(cacheKey);
                    listContainer.Put(item);
                    break;

            }

            return false;
        }

        public virtual bool UpdateItem(object item)
        {
            // Step 1. Get Cache
            Cache cache = CacheHelper2.GetCache();

            if (cache == null)
            {
                return false;
            }

            return false;
        }

        public virtual bool DeleteItem(object item)
        {
            // Step 1. Get Cache
            Cache cache = CacheHelper2.GetCache();

            if (cache == null)
            {
                return false;
            }

            return false;
        }
        
    }
}
