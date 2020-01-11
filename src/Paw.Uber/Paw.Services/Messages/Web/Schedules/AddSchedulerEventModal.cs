using Paw.Services.Util;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Pets;

namespace Paw.Services.Messages.Web.Schedules
{
    public class AddSchedulerEventModal :IAdd<SchedulerEvent>
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

        [StartRow]
        [Width(10)]
        [Display(Name = "Owner")]
        [UIHint("OwnerIdLookup")]
        [Required]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [StartRow]
        [Width(10)]
        [Display(Name = "Pet")]
        [UIHint("RadioButtonList")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetPetListByOwnerId))]
        [Required]
        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;

        [StartRow]
        [Width(10)]
        [Display(Name = "Provider")]
        [Required]
        [AddSelectList(DataTextField = "FullName", DataValueField = "Id", Type = typeof(GetEmployeeInfoList))]
        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;


        //[StartRow]
        //[Width(10)]
        //[MaxLength(250)]
        [ScaffoldColumn(false)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = String.Empty;

        [ScaffoldColumn(false)]
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

        [StartRow]
        [Required]
        [Width(10)]
        [DataType(DataType.DateTime)]
        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        [Required]
        [StartRow]
        [Width(10)]
        [DataType(DataType.DateTime)]
        public DateTime? End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime? _End = null;

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

        [ScaffoldColumn(false)]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;


    }
}
