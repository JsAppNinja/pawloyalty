using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Invoices
{
    public class GetInvoice : IGet<Invoice>
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Invoice ExecuteItem()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.InvoiceSet
                    .Include("InvoiceItemCollection.Pet")
                    .Include("Owner")
                    .Where(x => x.Id == this.Id && x.ProviderId == this.ProviderId).SingleOrDefault();
            }
        }
    }
}
