using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.PetReservations
{
    public class ReservationGroup : IId
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [UIHint("PetReservationList")]
        public List<PetReservation> PetReservationList
        {
            get { return _PetReservationList; }
            set { _PetReservationList = value; }
        }
        private List<PetReservation> _PetReservationList = new List<PetReservation>();

        [ScaffoldColumn(false)]
        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }
        private bool _IsNew = false;



    }
}
