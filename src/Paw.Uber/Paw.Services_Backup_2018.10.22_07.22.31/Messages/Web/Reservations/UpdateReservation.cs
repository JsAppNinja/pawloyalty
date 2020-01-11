using Paw.Services.Attributes;
using Paw.Services.Attributes.ClientData;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Owners;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Reservations
{
    public class UpdateReservation : IUpdateProvider<Reservation>
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
        [Required]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [StartRow]
        // TODO: At least one
        [Display(Name = "Choose Pets")]
        [UIHint("CheckboxList")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetPetListByOwnerId))]
        [Required]
        public List<Guid> PetList
        {
            get { return _PetList; }
            set { _PetList = value; }
        }
        private List<Guid> _PetList = new List<Guid>();

        [StartRow]
        [Display(Name = "Service Type")]
        [UIHint("RadioButtonList")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetSkuCatetoryList))]
        [AddAttribute(Key ="class", Value = "onChangeContainerUpdate")]
        [AddDataAttribute(Name = "SkuCategoryId", Expression = "input[name='SkuCategoryId']:checked")]
        [AddDataAttribute(PropertyName = "ProviderId")]
        [AddDataAttribute(Name = "Container", Value = "#PrimarySkuId-container")]
        [AddDataAttribute(Name = "Action", Value = "PrimarySkuId")]
        [Required]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        [StartRow]
        [Display(Name = "Service")]
        [UIHint("RadioButtonList")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetSkuList))]
        [AddDataAttribute(PropertyName = "Id")]
        [AddDataAttribute(PropertyName = "ProviderId")]
        [AddDataAttribute(PropertyName = "OwnerId", Expression = "#OwnerId")]
        [Required]
        public Guid? PrimarySkuId
        {
            get { return _PrimarySkuId; }
            set { _PrimarySkuId = value; }
        }
        private Guid? _PrimarySkuId = null;

    }
}
