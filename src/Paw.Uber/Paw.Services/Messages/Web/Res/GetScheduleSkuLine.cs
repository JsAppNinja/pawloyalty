using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class GetScheduleSkuLine
    {
        public Guid Id // ParentSkuLine
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

        public ScheduleSkuLine ExecuteItem()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                SkuLine skuLine = context
                    .SkuLineSet
                    .Include("ChildCollection")
                    .Include("ScheduleRule")
                    .Where(x => x.Id == this.Id).SingleOrDefault();

                if (skuLine == null)
                {
                    throw new InvalidOperationException("SkuLine not found.");
                }

                return new ScheduleSkuLine()
                {
                    Id = this.Id,
                    ProviderId = skuLine.ProviderId,
                    OwnerId = skuLine.Reservation.OwnerId,
                    PetId = skuLine.PetId.Value,
                    ReservationId = skuLine.ReservationId.Value,
                    EmployeeId = skuLine.ScheduleRule?.EmployeeId,
                    ScheduleRuleId = skuLine.ScheduleRuleId, 
                    StartDate = skuLine.StartDate
                };
            }

        }
    }
}
