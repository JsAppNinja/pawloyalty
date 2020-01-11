using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class AddScheduleRule : IAdd<ScheduleRule>, IProviderId
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
        
        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

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
        
    }
}
