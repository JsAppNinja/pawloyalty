using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class GetReservation
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

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
                return context
                    .ReservationSet
                    .Include("SkuLineCollection")
                    .Where(x => x.Id == this.Id && x.ProviderId == this.ProviderId).SingleOrDefault();
            }
        }


    }
}
