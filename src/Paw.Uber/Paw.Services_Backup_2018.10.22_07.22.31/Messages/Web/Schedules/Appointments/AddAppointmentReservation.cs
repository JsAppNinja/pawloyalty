using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules.Appointments
{
    public class AddAppointmentReservation
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
        // AtLeadOne
        public List<PetAppointmentReservation> PetAppointmentReservationList
        {
            get { return _PetAppointmentReservationList; }
            set { _PetAppointmentReservationList = value; }
        }
        private List<PetAppointmentReservation> _PetAppointmentReservationList = new List<PetAppointmentReservation>();

        public Sku PrimarySku
        {
            get { return _PrimarySku; }
            set { _PrimarySku = value; }
        }
        private Sku _PrimarySku = null;




    }
}
