using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Owners;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Invoices
{
    public class UpdateInvoice : IUpdateProvider<Invoice>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [StartRow]
        [Display(Name = "Owner")]
        [UIHint("OwnerId")]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [StartRow]
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private DateTime _Date = DateTime.UtcNow;
    }
}
