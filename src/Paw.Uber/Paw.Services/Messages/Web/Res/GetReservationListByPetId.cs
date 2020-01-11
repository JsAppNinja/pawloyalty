using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class GetReservationListByPetId
    {
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

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
                var result = context
                    .ReservationSet
                    .Include("SkuLineCollection.Sku")
                    .Include("SkuLineCollection.ScheduleRule.Employee")
                    .Where(x => x.SkuLineCollection.Any(y => y.PetId == this.PetId)).ToList();

                result.Sort((x, y) =>  DateTime.Compare(x.StartDateTime ?? DateTime.MinValue, y.StartDateTime ?? DateTime.MinValue));

                return result;
            }
        }


    }
}
