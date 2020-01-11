using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.InvoiceItems
{
    public class AddInvoiceItem : IAdd<InvoiceItem>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;
        
        [ScaffoldColumn(false)]
        public Guid InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }
        private Guid _InvoiceId = Guid.Empty;

        [StartRow]
        [Display(Name = "Product")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetSkuList))]
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [ScaffoldColumn(false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [ScaffoldColumn(false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [StartRow]
        [DataType(DataType.Currency)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private decimal? _Amount = null;

        [StartRow]
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private int _Quantity = 1;

        [ScaffoldColumn(false)]
        public Guid? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        private Guid? _ParentId = null;


    }
}
