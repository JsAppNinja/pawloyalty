using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class SchedulerEvent : ISchedulerEvent, IProviderId, IId, IAudit
    {
        [JsonProperty("id")]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [JsonProperty("providerId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [JsonIgnore]
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;
        
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set;}

        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        

        [JsonProperty("title")]
        [MaxLength(250)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = String.Empty;

        [JsonProperty("description")]
        [MaxLength(1000)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [JsonProperty("isAllDay")]
        public bool IsAllDay
        {
            get { return _IsAllDay; }
            set { _IsAllDay = value; }
        }
        private bool _IsAllDay = false;

        [JsonProperty("start")]
        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime _Start = DateTime.UtcNow;

        [JsonProperty("end")]
        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime _End = DateTime.UtcNow;

        [JsonProperty("startTimezone")]
        [MaxLength(50)]
        public string StartTimezone
        {
            get { return _StartTimezone; }
            set { _StartTimezone = value; }
        }
        private string _StartTimezone = String.Empty;

        [JsonProperty("endTimezone")]
        [MaxLength(50)]
        public string EndTimezone
        {
            get { return _EndTimezone; }
            set { _EndTimezone = value; }
        }
        private string _EndTimezone = String.Empty;

        [JsonProperty("recurrenceRule")]
        [MaxLength(1000)]
        public string RecurrenceRule
        {
            get { return _RecurrenceRule; }
            set { _RecurrenceRule = value; }
        }
        private string _RecurrenceRule = String.Empty;

        [JsonProperty("recurrenceException")]
        [MaxLength(1000)]
        public string RecurrenceException
        {
            get { return _RecurrenceException; }
            set { _RecurrenceException = value; }
        }
        private string _RecurrenceException = String.Empty;

        [JsonIgnore]
        public virtual ICollection<SchedulerEventPet> SchedulerEventPetCollection { get; set; }

        public Guid SchedulerEventTypeId
        {
            get { return _SchedulerEventTypeId; }
            set { _SchedulerEventTypeId = value; }
        }
        private Guid _SchedulerEventTypeId = SchedulerEventType.AppointmentId;

        [JsonIgnore]
        [ForeignKey("SchedulerEventTypeId")]
        public virtual SchedulerEventType SchedulerEventType { get; set; }

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        [JsonIgnore]
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;
        
        [JsonIgnore]
        [ForeignKey("SkuCategoryId")]
        public virtual SkuCategory SkuCategory { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        private string _Color = "#DDD";

        public Guid? ResourceId
        {
            get { return _ResourceId; }
            set { _ResourceId = value; }
        }
        private Guid? _ResourceId = null;

        [JsonIgnore]
        [ForeignKey("ResourceId")]
        public virtual Resource Resource { get; set; }


        #region ...

        public int DurationInMinutes
        {
            get
            {
                return (int)(this.End - this.Start).TotalMinutes;
            }
        }

        #endregion

        #region Audit ...

        public Guid MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }
        private Guid _MessageId = Muid.Comb();

        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime _Created = DateTime.UtcNow;

        public Guid? CreatedById
        {
            get { return _CreatedById; }
            set { _CreatedById = value; }
        }
        private Guid? _CreatedById = null;

        [JsonIgnore]
        [ForeignKey("UpdatedById")]
        public virtual User CreatedBy { get; set; }

        public DateTime Updated
        {
            get { return _Updated; }
            set { _Updated = value; }
        }
        private DateTime _Updated = DateTime.UtcNow;

        public Guid? UpdatedById
        {
            get { return _UpdatedById; }
            set { _UpdatedById = value; }
        }
        private Guid? _UpdatedById = null;

        [JsonIgnore]
        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }

        [MaxLength(250)]
        public string MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }
        private string _MessageType = String.Empty;

        [MaxLength(250)]
        public string MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value; }
        }
        private string _MachineName = String.Empty;


        #endregion


    }
}
