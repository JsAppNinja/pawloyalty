using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Messages.Web.SimpleReservations
{
    public class AddPetService : IId
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

        [ScaffoldColumn(false)]
        public bool Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; }
        }
        private bool _Deleted = false;
        
        [StartRow]
        [Width(6)]
        [HiddenInput(DisplayValue = false)]
        [AddItem(Method ="ExecuteItem", Type=typeof(GetPet) )]
        [Display(Name = "Pet")]
        [Required]
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [StartRow]
        [Width(6)]
        [HiddenInput(DisplayValue = true)]
        [AddItem(Method = "ExecuteItem", Type = typeof(GetSku))]
        [Display(Name = "Service")]
        
        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

        [StartRow]
        [Width(6)]
        [Display(Name = "Extras")]
        [UIHint("CheckboxList")]
        [AddSelectList(DataTextField ="Sku.Name", DataValueField = "SkuId", Method = "ExecuteList", Type = typeof(GetSkuGroupSkuListBySkuGroupId))]
        public List<Guid> SkuList
        {
            get { return _SkuList; }
            set { _SkuList = value; }
        }
        private List<Guid> _SkuList = new List<Guid>();




    }
}
