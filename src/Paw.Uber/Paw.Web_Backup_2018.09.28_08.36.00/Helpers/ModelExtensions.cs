using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paw.Web.Helpers
{
    public static class ModelExtensions
    {
        #region Provider ...

        public static SkuCategory GetSkuCategory(this Provider provider, Guid? skuCategoryId)
        {
            if (skuCategoryId == null) return null;

            if (provider == null) return null;

            if (provider.SkuCategoryCollection == null) return null;

            return provider.SkuCategoryCollection.FirstOrDefault(x => x.Id == skuCategoryId);
        }
        

        #endregion
    }
}