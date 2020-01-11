using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Reservations
{
    public class GetReservationListByProviderId
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<Reservation> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.ReservationSet
                    .Include("Provider")
                    .Include("Pet.Owner")
                    .Where(x => x.ProviderId == this.ProviderId)
                    .ToList();
            }
        }
    }
}
