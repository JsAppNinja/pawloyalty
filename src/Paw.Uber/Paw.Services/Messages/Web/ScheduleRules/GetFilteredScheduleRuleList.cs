using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class GetFilteredScheduleRuleList
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
        private DateTime _End = DateTime.UtcNow.Date.AddDays(30);
        
        public List<ScheduleRule> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return ExecuteList(context);
            }
        }

        public List<ScheduleRule> ExecuteList(DataContext context)
        {
            List<ScheduleRule> scheduleRuleList = context.ScheduleRuleSet.Where(x => x.ProviderId == this.ProviderId).ToList();

            // Step 1. Filter 
            List<ScheduleRule> result = scheduleRuleList.FindAll(x =>
                (this.EmployeeIdFilterList == null || this.EmployeeIdFilterList.Count == 0 || this.EmployeeIdFilterList.Any(y => y == x.EmployeeId)) && // Employees
                (this.Start > x.RuleStartDate && x.RuleStartDate < this.End) || (this.Start > x.RuleEndDate && x.RuleEndDate < this.End)); // Rules

            return result;
        }
    }
}
