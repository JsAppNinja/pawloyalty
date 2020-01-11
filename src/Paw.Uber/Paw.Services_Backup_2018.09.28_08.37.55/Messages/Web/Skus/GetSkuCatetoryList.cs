using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetSkuCatetoryList
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<SkuCategory> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.SkuCategorySet.Where(x => x.ProviderId == this.ProviderId).ToList();
            }
        }

    }
}
