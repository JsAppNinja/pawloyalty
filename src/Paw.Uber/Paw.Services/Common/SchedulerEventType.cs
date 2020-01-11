using Paw.Services.Messages.Web.Schedules;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class SchedulerEventType
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        public static readonly Guid AppointmentId = new Guid("{35531CB2-A08E-4FD8-9025-910EA6DBFEE0}");

        public static readonly Guid BlockId = new Guid("{FDA72880-2807-41D2-A553-891FF87774B0}");

        public static readonly Guid BoardingId = new Guid("{DEFE2DEC-4B6A-4254-9D8D-9D4555C96262}");
    }
}
