using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetSkuList
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [GetModelProperty(ParameterName = "SkuCategoryId")]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;
        
        public List<Sku> ExecuteList()
        {
            using (DataContext context = new DataContext())
            {
                return context
                    .SkuSet
                    .Include("Parent")
                    .Include("ChildCollection")
                    .Where(x => x.ProviderId == this.ProviderId && (this.SkuCategoryId == null || x.SkuCategoryId == this.SkuCategoryId)).ToList();
            }
        }
    }
}
