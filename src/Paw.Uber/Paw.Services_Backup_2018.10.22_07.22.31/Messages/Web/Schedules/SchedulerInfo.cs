using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules
{

    public class SchedulerInfo : ISchedulerEvent
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
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
        
        [MaxLength(50)]
        public string StartTimezone
        {
            get { return _StartTimezone; }
            set { _StartTimezone = value; }
        }
        private string _StartTimezone = String.Empty;
        
        [MaxLength(50)]
        public string EndTimezone
        {
            get { return _EndTimezone; }
            set { _EndTimezone = value; }
        }
        private string _EndTimezone = String.Empty;
        
        [MaxLength(1000)]
        public string RecurrenceRule
        {
            get { return _RecurrenceRule; }
            set { _RecurrenceRule = value; }
        }
        private string _RecurrenceRule = String.Empty;
        
        [MaxLength(1000)]
        public string RecurrenceException
        {
            get { return _RecurrenceException; }
            set { _RecurrenceException = value; }
        }
        private string _RecurrenceException = String.Empty;
        
        public Guid SchedulerEventTypeId
        {
            get { return _SchedulerEventTypeId; }
            set { _SchedulerEventTypeId = value; }
        }
        private Guid _SchedulerEventTypeId = SchedulerEventType.AppointmentId;

        // Exteneded properties ...

        public int DurationInMinutes
        {
            get { return _DurationInMinutes; }
            set { _DurationInMinutes = value; }
        }
        private int _DurationInMinutes = 0;

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;
        
        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }
        private string _EmployeeName = String.Empty;

        public string Initials
        {
            get { return _Initials; }
            set { _Initials = value; }
        }
        private string _Initials = String.Empty;

        public string PetName
        {
            get { return _PetName; }
            set { _PetName = value; }
        }
        private string _PetName = String.Empty;

        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }
        private string _ServiceName = String.Empty;


    }
}
