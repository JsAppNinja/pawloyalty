using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetPrimarySkuList
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
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                return dataContext
                    .SkuSet
                    .Include("SkuCategory")
                    .Where(x => (this.SkuCategoryId == null || x.SkuCategoryId == this.SkuCategoryId) && x.ProviderId == this.ProviderId && x.IsDeleted == false)
                    .ToList();
            }
        }
    }
}
