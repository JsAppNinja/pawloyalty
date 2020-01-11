using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleBlocks
{
    public class GetScheduleBlock
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        public ScheduleBlock ExecuteItem(DataContext dataContext)
        {
            throw new NotImplementedException();
            //return dataContext
            //    .ScheduleBlockSet
            //    .Include("ScheduleRule.Employee")
            //    .Where(x => x.Id == this.Id && x.ProviderId == this.ProviderId).SingleOrDefault();
        }

        public ScheduleBlock ExecuteItem()
        {
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                return this.ExecuteItem(dataContext);
            }
        }
    }
}
