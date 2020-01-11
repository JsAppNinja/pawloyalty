using Paw.Services.Common;
using Paw.Services.Messages.Web.ScheduleRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleBlocks
{
    public class GetVirtualScheduleBlockList : IProviderId
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime _Start = DateTime.UtcNow.Date;

        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime _End = DateTime.UtcNow.Date.AddDays(7);

        public Guid SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid _SkuCategoryId = Guid.Empty;

        public List<Guid?> EmployeeFilter
        {
            get { return _EmployeeFilter; }
            set { _EmployeeFilter = value; }
        }
        private List<Guid?> _EmployeeFilter = new List<Guid?>();


        public List<ScheduleBlock> ExecuteList()
        {
            // Step 1. Assign local
            DateTime startDate = this.Start;
            DateTime endDate = this.End;

            // Step 2. Get Daycount
            int dayCount = Convert.ToInt32((Start - End).TotalDays);
            if (dayCount < 1)
            {
                dayCount = 1;
            }

            // Step 3. Get existing schedule blocks
            List<ScheduleBlock> scheduleBlockList = new GetScheduleBlockList() { Start = this.Start, End = this.End, EmployeeFilter = this.EmployeeFilter, ProviderId = this.ProviderId, SkuCategoryId = this.SkuCategoryId }.ExecuteList();

            // Step 4. Get schedule rule
            List<ScheduleRule> scheduleRuleList = new GetScheduleRuleList() { ProviderId = this.ProviderId }.ExecuteList();

            // Step 5. Create result
            List<ScheduleBlock> result = new List<ScheduleBlock>();

            // Step 6. Add rules for period
            for (int i = 0; i < dayCount; i++)
            {
                DateTime date = startDate.AddDays(0);
                foreach (ScheduleRule scheduleRule in scheduleRuleList.FindAll(x => x.SkuCategoryId == this.SkuCategoryId && ((this.EmployeeFilter == null || this.EmployeeFilter.Count == 0) || this.EmployeeFilter.Any(y => y == x.EmployeeId))))
                {
                    ScheduleBlock scheduleBlock;
                    if (scheduleRule.TryGetDate(date, out scheduleBlock))
                    {
                        result.Add(scheduleBlock);
                    }
                }
            }

            // Step 7. Return order
            result = result.OrderBy(x => x.Start).ThenBy(x => x.ScheduleRule.EmployeeId).ToList();

            return result;
        }
    }
}
