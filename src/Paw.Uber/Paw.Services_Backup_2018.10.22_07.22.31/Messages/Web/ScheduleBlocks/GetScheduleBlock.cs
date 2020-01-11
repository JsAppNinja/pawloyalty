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

        public ScheduleBlock ExecuteItem(DataContext dataContext)
        {
            return dataContext.ScheduleBlockSet.Where(x => x.Id == this.Id).SingleOrDefault();
        
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
