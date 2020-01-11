using Kendo.Mvc.UI;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules
{
    public class GetSchedulerInfoList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        public Guid? SchedulerEventTypeId
        {
            get { return _SchedulerEventTypeId; }
            set { _SchedulerEventTypeId = value; }
        }
        private Guid? _SchedulerEventTypeId = null;

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;
        
        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        public DateTime? End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime? _End = null;
        
        public List<SchedulerInfo> ExecuteList(bool useCache = true)
        {
            // Correct day
            if (this.Start != null && this.End != null & this.Start == this.End)
            {
                this.End = this.Start.Value.AddDays(1);
            }


            using (DataContext context = DataContext.CreateForMessage(this))
            {
                List<SchedulerInfo> result = context
                    .SchedulerEventSet
                    .Include("Provider")
                    .Include("Employee")
                    .Include("Pet")
                    .Where(x => (this.EmployeeId == null || x.EmployeeId == this.EmployeeId) 
                        && (this.SkuCategoryId == null || x.SkuCategoryId == this.SkuCategoryId)
                        && (this.Start == null || x.Start >= this.Start)
                        && (this.End == null || x.End <= this.End)
                        && x.ProviderId == this.ProviderId)
                        .ToList().ConvertAll(x => new SchedulerInfo() { Id = x.Id,
                            Description = x.Description,
                            EmployeeId = x.EmployeeId,
                            End = x.End,
                            EndTimezone = x.EndTimezone,
                            IsAllDay = x.IsAllDay,
                            ProviderId = x.ProviderId,
                            RecurrenceException = x.RecurrenceException,
                            RecurrenceRule = x.RecurrenceRule,
                            SchedulerEventTypeId = x.SchedulerEventTypeId,
                            Start = x.Start,
                            StartTimezone = x.StartTimezone,
                            Title = x.Title,
                            Initials = x.Employee.Initials,
                            DurationInMinutes = x.DurationInMinutes,
                            EmployeeName = x.Employee.FullName,
                            PetName = x.Pet.Name,
                            ServiceName = x.SkuCategory.Name});
                
                return result;
            }
        }


    }
}
