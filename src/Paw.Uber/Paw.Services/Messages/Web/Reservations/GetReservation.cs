using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Reservations
{
    public class GetReservation : IGetProvider<Reservation>
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Reservation ExecuteItem()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.ReservationSet.Include("Pet.Owner").Where(x => x.ProviderId == this.ProviderId).SingleOrDefault();
            }
        }
    }
}
