using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Invoice : IId, IProviderId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private DateTime _Date = DateTime.UtcNow;

        public double? Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        private double? _Total = null;
        
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { _IsOpen = value; }
        }
        private bool _IsOpen = false;
        
        #region Relationships ...

        public virtual ICollection<InvoiceItem> InvoiceItemCollection { get; set; }

        #endregion
        
        #region Audit ...

        public Guid MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }
        private Guid _MessageId = Guid.Empty;

        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime _Created = DateTime.UtcNow;

        public Guid? CreatedById
        {
            get { return _CreatedById; }
            set { _CreatedById = value; }
        }
        private Guid? _CreatedById = null;

        [ForeignKey("UpdatedById")]
        public virtual User CreatedBy { get; set; }

        public DateTime Updated
        {
            get { return _Updated; }
            set { _Updated = value; }
        }
        private DateTime _Updated = DateTime.UtcNow;

        public Guid? UpdatedById
        {
            get { return _UpdatedById; }
            set { _UpdatedById = value; }
        }
        private Guid? _UpdatedById = null;

        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }

        [MaxLength(250)]
        public string MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }
        private string _MessageType = String.Empty;

        [MaxLength(250)]
        public string MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value; }
        }
        private string _MachineName = String.Empty;


        #endregion

        public List<InvoiceItem> GetParentInvoiceItemList()
        {
            return this.GetInvoiceItemList().FindAll(x => x.ParentId == null);
        }

        public List<InvoiceItem> GetInvoiceItemList()
        {
            if (this.InvoiceItemCollection == null) return new List<InvoiceItem>();

            return new List<InvoiceItem>(this.InvoiceItemCollection);
        }

        public decimal CalcualteTotal()
        {
            decimal total = 0;

            foreach (InvoiceItem invoiceItem in GetInvoiceItemList())
            {
                total = total + invoiceItem.Total ?? 0;
            }

            return total;
        }
    }
}
