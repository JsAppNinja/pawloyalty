using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetPrimaryOrExtraSkuList
    {
        
        [GetRequestValueAsGuidAttirbute(ParameterName ="SkuId")]
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [GetRequestValueAsGuidAttirbute(ParameterName = "SkuCategoryId")]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _Type = 1; // 1 == AddOn


        public List<Sku> ExecuteList(bool useCache = true)
        {
            Provider provider = new GetProvider() { Id = this.ProviderId }.ExecuteItem(useCache);

            if (this.SkuId != null)
            {
                List<SkuGroup> realtedSkuList = provider.SkuGroupCollection.ToList().FindAll(x => x.Type == this.Type && x.SkuGroupSkuCollection.ToList().Any(y => y.SkuId == this.SkuId && y.Type == 0)).ToList();
                SkuGroup skuGroup = realtedSkuList.Find(x => x.Type == 1);
                if (skuGroup == null)
                {
                    return new List<Sku>();
                }

                return skuGroup.SkuGroupSkuCollection.ToList().FindAll(x => x.Type == 1).ConvertAll(x => x.Sku);
            }
            else 
            {
                return provider.SkuCollection.ToList().FindAll(x => x.IsPrimary && x.SkuCategoryId == this.SkuCategoryId);
            }
            
        }
    }
}
