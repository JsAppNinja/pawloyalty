using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Caching
{
    public class ProviderCacheAttribute : CacheAttribute
    {
        public override string GetCacheKey(object item)
        {
            string result = string.Empty;

            switch (this.CacheType)
            {
                case CacheTypes.Item:
                    result = $"mc_{item.GetType().Name}_{item.GetProviderId()}_{item.GetId()}";
                    break;
                case CacheTypes.ItemList:
                    result = $"mc_{item.GetType().Name}_{item.GetProviderId()}";
                    break;
            }

            return result;
        }
        

        
    }
}
