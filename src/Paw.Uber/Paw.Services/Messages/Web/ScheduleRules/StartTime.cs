using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class StartTime
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        public DateTime Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        private DateTime _Time = DateTime.Now.Date;


    }
}
