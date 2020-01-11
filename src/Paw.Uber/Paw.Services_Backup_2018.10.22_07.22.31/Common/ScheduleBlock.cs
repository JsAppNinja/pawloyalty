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
    public class ScheduleBlock : IId, IAudit, ISaved
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

        public Guid? ScheduleRuleId
        {
            get { return _ScheduleRuleId; }
            set { _ScheduleRuleId = value; }
        }
        private Guid? _ScheduleRuleId = null;

        [ForeignKey("ScheduleRuleId")]
        public virtual ScheduleRule ScheduleRule { get; set; }


        #region Start/End Date Time ..

        public int StartHour
        {
            get { return _StartHour; }
            set { _StartHour = value; }
        }
        private int _StartHour = 0;

        public int StartMinute
        {
            get { return _StartMinute; }
            set { _StartMinute = value; }
        }
        private int _StartMinute = 0;

        public int EndHour
        {
            get { return _EndHour; }
            set { _EndHour = value; }
        }
        private int _EndHour = 0;

        public int EndMinute
        {
            get { return _EndMinute; }
            set { _EndMinute = value; }
        }
        private int _EndMinute = 0;

        public int StartDay
        {
            get { return _StartDay; }
            set { _StartDay = value; }
        }
        private int _StartDay = 0;

        public int StartMonth
        {
            get { return _StartMonth; }
            set { _StartMonth = value; }
        }
        private int _StartMonth = 0;

        public int StartYear
        {
            get { return _StartYear; }
            set { _StartYear = value; }
        }
        private int _StartYear = 0;

        public DateTime Start
        {
            get
            {
                return new DateTime(this.StartYear, this.StartMonth, this.StartDay, this.StartHour, this.StartMinute, 0);
            }
            set
            {
                if (value != null)
                {
                    this.StartYear = value.Year;
                    this.StartMonth = value.Month;
                    this.StartDay = value.Day;
                    this.StartHour = value.Hour;
                    this.StartMinute = value.Minute;
                }
            }
        }

        public DateTime End
        {
            get
            {
                return new DateTime(this.EndYear, this.EndMonth, this.EndDay, this.EndHour, this.EndMinute, 0);
            }
            set
            {
                if (value != null)
                {
                    this.EndYear = value.Year;
                    this.EndMonth = value.Month;
                    this.EndDay = value.Day;
                    this.EndHour = value.Hour;
                    this.EndMinute = value.Minute;
                }
            }
        }

        public int EndDay
        {
            get { return _EndDay; }
            set { _EndDay = value; }
        }
        private int _EndDay = 0;

        public int EndMonth
        {
            get { return _EndMonth; }
            set { _EndMonth = value; }
        }
        private int _EndMonth = 0;

        public int EndYear
        {
            get { return _EndYear; }
            set { _EndYear = value; }
        }
        private int _EndYear = 0;
        
        public bool IsAllDay
        {
            get { return _IsAllDay; }
            set { _IsAllDay = value; }
        }
        private bool _IsAllDay = false;

        public long StartKey
        {
            get
            {
                //long.Parse($"{this.StartYear}{this.StartMonth.ToString().PadLeft(2, '0')}{this.StartDay.ToString().PadLeft(2, '0')}{this.StartHour.ToString().PadLeft(2, '0')}{this.StartMinute.ToString().PadLeft(2, '0')}")
                //return long.Parse($"{this.StartYear}{this.StartMonth}{this.StartDay}{this.StartHour}{this.StartMinute}");
                return (long)this.StartYear * 100000000 + this.StartMonth * 1000000 + this.StartDay * 10000 + this.StartHour * 100 + this.StartMinute;
            }
        }

        public long EndKey
        {
            get
            {
                return (long)this.EndYear * 100000000 + this.EndMonth * 1000000 + this.EndDay * 10000 + this.EndHour * 100 + this.EndMinute;
            }
        }

        #endregion

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
       
        public DateTime? Saved
        {
            get { return _Saved; }
            set { _Saved = value; }
        }
        private DateTime? _Saved = null;
        
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


    }
}
