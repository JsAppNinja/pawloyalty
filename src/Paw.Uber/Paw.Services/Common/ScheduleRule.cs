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
    // Supports single day, singble block weekly schedule
    public class ScheduleRule : IId, IProviderId, IAudit, ISaved
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

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        
        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        [ForeignKey("SkuCategoryId")]
        public virtual SkuCategory SkuCategory { get; set; }

        public int? Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }
        private int? _Capacity = null;


        #region StartEnd Date Time ..
        

        [DataType(DataType.Time)]
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        private DateTime? _StartTime = null;

        public int? Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        private int? _Duration = null;
        
        public bool IsAllDay
        {
            get { return _IsAllDay; }
            set { _IsAllDay = value; }
        }
        private bool _IsAllDay = false;

        #endregion
        
        public virtual ICollection<ScheduleBlock> ScheduleBlockCollection { get; set; }

        public DateTime? Saved
        {
            get { return _Saved; }
            set { _Saved = value; }
        }
        private DateTime? _Saved = null;
        
        #region Rule ...

        public bool Sunday
        {
            get { return _Sunday; }
            set { _Sunday = value; }
        }
        private bool _Sunday = false;
        
        public bool Monday
        {
            get { return _Monday; }
            set { _Monday = value; }
        }
        private bool _Monday = false;

        public bool Tuesday
        {
            get { return _Tuesday; }
            set { _Tuesday = value; }
        }
        private bool _Tuesday = false;
        
        public bool Wednesday
        {
            get { return _Wednesday; }
            set { _Wednesday = value; }
        }
        private bool _Wednesday = false;
        
        public bool Thursday
        {
            get { return _Thursday; }
            set { _Thursday = value; }
        }
        private bool _Thursday = false;
        
        public bool Friday
        {
            get { return _Friday; }
            set { _Friday = value; }
        }
        private bool _Friday = false;

        public bool Saturday
        {
            get { return _Saturday; }
            set { _Saturday = value; }
        }
        private bool _Saturday = false;

        public bool IsRule
        {
            get { return _IsRule; }
            set { _IsRule = value; }
        }
        private bool _IsRule = false;

        [DataType(DataType.Date)]
        public DateTime RuleStartDate
        {
            get { return _RuleStartDate; }
            set { _RuleStartDate = value; }
        }
        private DateTime _RuleStartDate = DateTime.Now.Date;

        [DataType(DataType.Date)]
        public DateTime? RuleEndDate
        {
            get { return _RuleEndDate; }
            set { _RuleEndDate = value; }
        }
        private DateTime? _RuleEndDate = null;


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

        private bool HasDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return this.Saturday;
                case DayOfWeek.Monday:
                    return this.Monday;
                case DayOfWeek.Tuesday:
                    return this.Tuesday;
                case DayOfWeek.Wednesday:
                    return this.Wednesday;
                case DayOfWeek.Thursday:
                    return this.Thursday;
                case DayOfWeek.Friday:
                    return this.Friday;
                case DayOfWeek.Saturday:
                    return this.Saturday;
                default:
                    break;
            }

            return false;
            
        }



        public bool TryGetDate(DateTime date, out ScheduleBlock scheduleBlock)
        {
            List<ScheduleBlock> existingList = this.ScheduleBlockCollection.ToList();

            scheduleBlock = existingList.Find(x => x.Start.Date == date);

            if (scheduleBlock != null)
            {
                return true;
            }

            if (!this.ValidDate(date))
            {
                return false;
            }

            scheduleBlock = this.CreateScheduleBlock(date);
            return true;
        }

        public bool ValidDate(DateTime date)
        {
            if (this.RuleStartDate > date || date > this.RuleEndDate)
            {
                return false;
            }

            if (!this.HasDayOfWeek(date.DayOfWeek))
            {
                return false;
            }

            // Add in vacation and cancellation

            return true;
        }

        public ScheduleBlock CreateScheduleBlock(DateTime date)
        {
            DateTime startTime = this.StartTime.Value;

            ScheduleBlock scheduleBlock = new ScheduleBlock()
            {
                IsAllDay = this.IsAllDay,
                Start = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0),
                End = startTime.AddMinutes(this.Duration.Value),
                ProviderId = this.ProviderId,
                ScheduleRuleId = this.Id,
                Saved = null
            };

            this.ScheduleBlockCollection.Add(scheduleBlock);

            return scheduleBlock;
        }

        //public SortedDictionary<long, ScheduleBlock> GetScheduleBlockList(DateTime start, DateTime end, List<ScheduleBlock> existingBlockList)
        //{
        //    DateTime endDate = this.RuleEndDate ?? DateTime.Now.AddMonths(20);
        //    endDate = endDate > end ? end : endDate;

        //    DateTime startDate = this.RuleStartDate > start ? this.RuleStartDate.Date : start.Date;

        //    // Step 1. Build
        //    SortedDictionary<long, ScheduleBlock> directory = new SortedDictionary<long, ScheduleBlock>();

        //    DateTime date = startDate.Date.AddHours(this.StartHour ?? 0).AddMinutes(this.StartMinute ?? 0);

        //    while (date <= endDate)
        //    {

        //        if (this.IsInstance(date))
        //        {
        //            var sb = new ScheduleBlock() {
        //                ScheduleRuleId = this.Id
        //            };

        //            directory.Add(sb.StartKey, sb);
        //        }
        //        date = date.AddDays(1);
        //    }

        //    // Step 2. Replace w/ existing
        //    if (existingBlockList == null)
        //    {
        //        existingBlockList = new List<ScheduleBlock>();
        //    }

        //    foreach (var item in existingBlockList)
        //    {
        //        if (this.IsInstance(new DateTime(item.StartYear, item.StartMonth, item.StartDay, item.StartHour, item.StartMinute, 0)))
        //        {
        //            directory[item.StartKey] = item;
        //        }

        //    }

        //    return directory;
        //}


    }
}
