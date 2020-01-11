using Paw.Services.Common;
using Paw.Services.Messages.Web.ScheduleRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleBlocks
{
    public class GetVirtualScheduleBlock
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
        
        public Guid RuleId
        {
            get { return _RuleId; }
            set { _RuleId = value; }
        }
        private Guid _RuleId = Guid.Empty;

        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime _Start = DateTime.UtcNow;

        public ScheduleBlock ExecuteItem()
        {
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                return ExecuteItem(dataContext);
            }
        }

        public ScheduleBlock ExecuteItem(DataContext dataContext)
        {
            ScheduleBlock scheduleBlock = dataContext.ScheduleBlockSet.Where(x => x.Id == this.Id).SingleOrDefault();

            if (scheduleBlock != null)
            {
                return scheduleBlock;
            }

            ScheduleRule scheduleRule = new GetScheduleRule() { Id = this.RuleId, ProviderId = this.ProviderId }.ExecuteItem(dataContext);

            if (scheduleRule == null)
            {
                return null;
            }

            return new ScheduleBlock()
            {
                Id = this.Id,
                EmployeeId = scheduleRule.EmployeeId,
                Start = this.Start,
                End = new DateTime(this.Start.Year, this.Start.Month, this.Start.Day, scheduleRule.EndHour.Value, scheduleRule.EndMinute.Value, 0),
            };
        }
    }
}
