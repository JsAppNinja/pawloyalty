using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules
{
    public class AddSchedulerEventType1 : IAdd<SchedulerEvent>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [MaxLength(250)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = String.Empty;

        [MaxLength(1000)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [ScaffoldColumn(false)]
        public bool IsAllDay
        {
            get { return _IsAllDay; }
            set { _IsAllDay = value; }
        }
        private bool _IsAllDay = false;

        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime _Start = DateTime.UtcNow;

        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime _End = DateTime.UtcNow;

        [ScaffoldColumn(false)]
        public string StartTimezone
        {
            get { return _StartTimezone; }
            set { _StartTimezone = value; }
        }
        private string _StartTimezone = "Etc/UTC";

        [ScaffoldColumn(false)]
        public string EndTimezone
        {
            get { return _EndTimezone; }
            set { _EndTimezone = value; }
        }
        private string _EndTimezone = "Etc/UTC";

        [ScaffoldColumn(false)]
        public string RecurrenceRule
        {
            get { return _RecurrenceRule; }
            set { _RecurrenceRule = value; }
        }
        private string _RecurrenceRule = String.Empty;

        [ScaffoldColumn(false)]
        public string RecurrenceException
        {
            get { return _RecurrenceException; }
            set { _RecurrenceException = value; }
        }
        private string _RecurrenceException = String.Empty;

        [ScaffoldColumn(false)]
        public Guid SchedulerEventTypeId
        {
            get { return _SchedulerEventTypeId; }
            set { _SchedulerEventTypeId = value; }
        }
        private Guid _SchedulerEventTypeId = SchedulerEventType.AppointmentId;

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

    }
}
