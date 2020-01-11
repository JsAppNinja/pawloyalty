using Paw.Services.Attributes;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.SimpleReservations
{
    public class AddPetService
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [StartRow]
        public bool Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; }
        }
        private bool _Deleted = false;
        
        [StartRow]
        [Width(6)]
        [UIHint("ReadOnlyPetId")]
        [Display(Name = "Pet")]
        [Required]
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;


    }
}
