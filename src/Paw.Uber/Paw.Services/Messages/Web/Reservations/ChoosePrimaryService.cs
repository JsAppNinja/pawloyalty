using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Reservations
{
    public class ChoosePrimaryService
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

    }
}
