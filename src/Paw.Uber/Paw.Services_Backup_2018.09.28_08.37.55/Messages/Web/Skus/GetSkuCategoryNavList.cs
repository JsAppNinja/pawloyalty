using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetSkuCategoryNavList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public virtual List<SkuCategory> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.SkuCategorySet.Include("SchedulerType").Where(x => x.ProviderId == this.ProviderId).OrderBy(x => x.NavDisplayOrder).ToList();
            }
        }
    }
}
