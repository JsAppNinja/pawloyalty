using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetSku : IGetProvider<Sku>
    {
        [GetModelPropertyAttribute(ParameterName = "SkuId")]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Sku ExecuteItem()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.SkuSet.Where(x => x.ProviderId == this.ProviderId && x.Id == this.Id).SingleOrDefault();
            }
        }
    }
}
