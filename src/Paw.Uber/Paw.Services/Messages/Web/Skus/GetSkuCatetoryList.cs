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

        public bool PrimaryOnly
        {
            get { return _PrimaryOnly; }
            set { _PrimaryOnly = value; }
        }
        private bool _PrimaryOnly = true;

        public bool ShowDeleted
        {
            get { return _ShowDeleted; }
            set { _ShowDeleted = value; }
        }
        private bool _ShowDeleted = false;
        
        public List<SkuCategory> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context
                    .SkuCategorySet
                    .Include("SkuCollection")
                    .Where(x => x.ProviderId == this.ProviderId && (!this.PrimaryOnly || x.IsPrimary == true) && (!this.ShowDeleted || x.IsDeleted == true)).ToList();
            }
        }
    }
}
