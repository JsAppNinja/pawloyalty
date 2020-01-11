using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleBlocks
{
    public class GetScheduleBlockList : IProviderId
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime _Start = DateTime.UtcNow.Date;

        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
        }
        private DateTime _End = DateTime.UtcNow.Date.AddDays(7);

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        public List<Guid?> EmployeeFilter
        {
            get { return _EmployeeFilter; }
            set { _EmployeeFilter = value; }
        }
        private List<Guid?> _EmployeeFilter = new List<Guid?>();


        public List<ScheduleBlock> ExecuteList()
        {
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                throw new NotImplementedException();

                //List<ScheduleBlock> scheduleBlock = dataContext
                //    .ScheduleBlockSet
                //    .Include("ScheduleRule")
                //    .Where(x => x.ProviderId == this.ProviderId).ToList();

                //return scheduleBlock.FindAll(x => (this.SkuCategoryId == null || x.ScheduleRule?.SkuCategoryId == this.SkuCategoryId) && (this.EmployeeFilter.Any(y => y.Value == x.ScheduleRule?.EmployeeId)));
            }
        }
    }
}
