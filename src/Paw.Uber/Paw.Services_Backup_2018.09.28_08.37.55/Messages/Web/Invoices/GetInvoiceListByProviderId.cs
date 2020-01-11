using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Invoices
{
    public class GetInvoiceListByProviderId
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<Invoice> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.InvoiceSet
                    .Include("Provider")
                    .Include("Owner")
                    .Where(x => x.ProviderId == this.ProviderId)
                    .ToList();
            }
        }
    }
}
