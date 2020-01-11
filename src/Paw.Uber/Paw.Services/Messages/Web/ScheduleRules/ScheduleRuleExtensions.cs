using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public static class ScheduleRuleExtensions
    {
        public static List<StartTime> GetStartTimeList(this List<ScheduleRule> list, DateTime? referenceDate = null)
        {
            // Set reference date if necessary
            if (referenceDate == null)
            {
                referenceDate = DateTime.Now.Date;
            }
            else
            {
                referenceDate = referenceDate.Value.Date;
            }

            // Return if empty
            if (list == null || list.Count == 0)
            {
                return new List<StartTime>();
            }

            // Get Distrinct StartTimes for ReferenceDate
            List<StartTime> result = list
                .FindAll(x => x.StartTime != null && x.ValidDate(referenceDate.Value))
                .OrderBy(x => x.StartTime)
                .Select(x => new StartTime() { Id = x.Id, Time = new DateTime(referenceDate.Value.Year, referenceDate.Value.Month, referenceDate.Value.Day, x.StartTime.Value.Hour, x.StartTime.Value.Minute, 0) })
                .Distinct()
                .ToList();
            
            return result;
        }

        public static List<ScheduleRule> FilterByEmployeeId(this List<ScheduleRule> list, Guid employeeId)
        {
            return list.FindAll(x => x.EmployeeId == employeeId);
        }
    }
}
