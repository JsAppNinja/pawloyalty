using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetSkuGroupSkuListBySkuGroupId
    {
        public Guid SkuGroupId
        {
            get { return _SkuGroupId; }
            set { _SkuGroupId = value; }
        }
        //private Guid _SkuGroupId = Guid.Empty;
        private Guid _SkuGroupId = new Guid("89DFECD5-EE7E-4374-8366-A988003040AD"); // Complete Groom upgrades

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


        public List<SkuGroupSku> ExecuteList(bool useCache = true)
        {
            Provider provider = new GetProvider() { Id = this.ProviderId }.ExecuteItem(useCache);

            List<SkuGroupSku> result = provider.SkuGroupCollection.ToList().Find(x => x.Id == this.SkuGroupId).SkuGroupSkuCollection.ToList().FindAll(x => x.Type == this.Type);
            result.Sort((x, y) => x.Sku.Name.CompareTo(y.Sku.Name));
            return result;
            
        }
    }
}
