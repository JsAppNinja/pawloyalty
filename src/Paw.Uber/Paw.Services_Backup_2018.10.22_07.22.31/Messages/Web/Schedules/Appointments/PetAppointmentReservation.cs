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
    public class PetAppointmentReservation : IId
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [UIHint("List")]
        public List<PetService> PetServiceList
        {
            get { return _PetServiceList; }
            set { _PetServiceList = value; }
        }
        private List<PetService> _PetServiceList = new List<PetService>();

        [DataType(DataType.MultilineText)]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        private string _Note = String.Empty;
        
    }
}
