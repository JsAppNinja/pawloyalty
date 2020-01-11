using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Skus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.DynamicControls
{
    public class PrimarySkuIdDynamicControl
    {
        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;
        
        [Display(Name = "Service")]
        [UIHint("RadioButtonList")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetSkuList))]
        public Guid? PrimarySkuId
        {
            get { return _PrimarySkuId; }
            set { _PrimarySkuId = value; }
        }
        private Guid? _PrimarySkuId = null;
    }
}
