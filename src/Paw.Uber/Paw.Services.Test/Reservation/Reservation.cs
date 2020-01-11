using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test.Reservation
{
    public class ReservationCX
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.NewGuid();

        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        public int? DurationInMinutes
        {
            get { return _DurationInMinutes; }
            set { _DurationInMinutes = value; }
        }
        private int? _DurationInMinutes = null;
        
        public string GetDescription()
        {
            return ""; // How do i get the description
        }

        #region ...
        


        #endregion
    }
}
