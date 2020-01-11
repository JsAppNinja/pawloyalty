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
    public class InvoiceItem : IId, INode<InvoiceItem>, IProviderId, IParentId
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
        
        public Guid InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }
        private Guid _InvoiceId = Guid.Empty;

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [ForeignKey("SkuId")]
        public virtual Sku Sku { get; set; }

        #region Sku Info ...

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [MaxLength(250)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [DataType(DataType.Currency)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private decimal? _Amount = null;

        #endregion
        
        public int? Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private int? _Quantity = 1;

        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        
        #region INode ...

        public Guid? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        private Guid? _ParentId = null;

        [ForeignKey("ParentId")]
        public virtual InvoiceItem Parent { get; set; }

        public ICollection<InvoiceItem> ChildCollection { get; set; }
        
        #endregion

        #region Calcs ...

        [NotMapped]
        public decimal? Total
        {
            get
            {
                if (this.Amount == null)
                {
                    return null;
                }
                return this.Amount * this.Quantity;
            }
        }

        public List<InvoiceItem> GetChildList()
        {
            if (this.ChildCollection == null) return new List<InvoiceItem>();

            return new List<InvoiceItem>(this.ChildCollection);
        }

        #endregion

    }
}
