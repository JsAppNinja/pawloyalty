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
    // TODO: Add Unique Constraint for rule & time

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
        

        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime _Start = DateTime.Now;
        
        public DateTime? End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime? _End = null;
        
        public bool IsAllDay
        {
            get { return _IsAllDay; }
            set { _IsAllDay = value; }
        }
        private bool _IsAllDay = false;
        
        #endregion
        
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
