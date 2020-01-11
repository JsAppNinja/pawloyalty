using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using System.ComponentModel.DataAnnotations;
using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Messages.Web.Pets;

namespace Paw.Services.Messages.Web.Reservations
{
    public class AddReservation : IAdd<Reservation>
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
        [UIHint("ReadOnlyOwnerId")]
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
        [Required]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        public int ExecuteNonQuery()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                MessageExtensions.ExecuteNonQuery(this, context);

                foreach (Guid petId in this.PetList)
                {
                    PetReservation petReservation = new PetReservation() { ReservationId = this.Id, PetId = petId, ProviderId = this.ProviderId };
                    context.PetReservationSet.Add(petReservation);
                }

                return context.SaveChanges();
            }
        }

    }


}
