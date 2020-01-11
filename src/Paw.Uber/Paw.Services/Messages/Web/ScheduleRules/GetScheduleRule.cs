using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class GetScheduleRule : IGetProvider<ScheduleRule>
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

        public ScheduleRule ExecuteItem(DataContext dataContext)
        {
            return dataContext.ScheduleRuleSet.Where(x => x.Id == this.Id && x.ProviderId == this.ProviderId).SingleOrDefault();
        }

        public ScheduleRule ExecuteItem()
        {
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                return this.ExecuteItem(dataContext);
            }
        }
    }
}
