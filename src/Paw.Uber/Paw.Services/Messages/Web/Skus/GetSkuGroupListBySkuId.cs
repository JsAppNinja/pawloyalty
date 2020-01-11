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
    public class GetSkuGroupListBySkuId
    {
        [GetModelPropertyAttribute(ParameterName = "SkuId")]
        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

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


        public List<SkuGroup> ExecuteList(bool useCache = true)
        {
            Provider provider = new GetProvider() { Id = this.ProviderId }.ExecuteItem(useCache);

            List<SkuGroup> result = provider.SkuGroupCollection.ToList().FindAll(x => x.Type == this.Type && x.SkuGroupSkuCollection.ToList().Any(y => y.SkuId == this.SkuId && y.Type == 0)).ToList();

            return result;
        }

        

    }
}
