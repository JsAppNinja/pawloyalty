using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleBlocks
{
    public class AddScheduleBlock : IAdd<ScheduleBlock>
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

        [ScaffoldColumn(false)]
        public Guid? ScheduleRuleId
        {
            get { return _ScheduleRuleId; }
            set { _ScheduleRuleId = value; }
        }
        private Guid? _ScheduleRuleId = null;

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;
        
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
        public bool IsAllDay
        {
            get { return _IsAllDay; }
            set { _IsAllDay = value; }
        }
        private bool _IsAllDay = false;


    }
}
