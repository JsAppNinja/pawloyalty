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
    public class UpdateSchedulerEventStartEnd :IUpdateProvider<SchedulerEvent>
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
        
        [Required]
        [Width(10)]
        [DataType(DataType.DateTime)]
        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;
        
        [StartRow]
        [Width(10)]
        [DataType(DataType.DateTime)]
        public DateTime? End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime? _End = null;

        public int ExecuteNonQuery()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                SchedulerEvent schedulerEvent = context.SchedulerEventSet.Where(x => x.Id == this.Id && x.ProviderId == this.ProviderId).SingleOrDefault();

                if (schedulerEvent == null) return 0;

                if ((this.Start != null || this.End != null) && (schedulerEvent.Start != this.Start || schedulerEvent.End != this.End))
                {
                    schedulerEvent.Start = this.Start.Value;
                    schedulerEvent.End = this.End.Value;
                }

                return context.SaveChanges();
            }
        }
        

    }
}
