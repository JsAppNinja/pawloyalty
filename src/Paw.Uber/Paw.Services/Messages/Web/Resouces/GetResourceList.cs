using Paw.Services.Common;
using Paw.Services.Messages.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Resouces
{
    public class GetResourceList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;


        public List<Resource> ExecuteList(bool useCache = true)
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context
                    .ResourceSet
                    .Include("Sku")
                    .Where(x => x.ProviderId == this.ProviderId && (this.SkuCategoryId == null || x.Sku.SkuCategoryId == this.SkuCategoryId))
                    .ToList();

            }
        }

    }
}
