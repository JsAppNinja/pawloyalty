using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class GetScheduleRuleListByDate
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<Guid> EmployeeIdFilterList
        {
            get { return _EmployeeIdFilterList; }
            set { _EmployeeIdFilterList = value; }
        }
        private List<Guid> _EmployeeIdFilterList = new List<Guid>();

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private DateTime _Date = DateTime.UtcNow.Date;
        
        public List<ScheduleRule> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return this.ExecuteList(context);
            }
        }

        public List<ScheduleRule> ExecuteList(DataContext context)
        {
            List<ScheduleRule> scheduleRuleList = new GetFilteredScheduleRuleList() { EmployeeIdFilterList = this.EmployeeIdFilterList, Start = this.Date, End = this.Date.AddDays(1), ProviderId = this.ProviderId }.ExecuteList(context);
            return scheduleRuleList;

        }
    }
}
