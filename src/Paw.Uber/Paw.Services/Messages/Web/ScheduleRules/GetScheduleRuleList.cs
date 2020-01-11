using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.ScheduleRules
{
    public class GetScheduleRuleList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        public List<ScheduleRule> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context.ScheduleRuleSet.Where(x => x.ProviderId == this.ProviderId).ToList();
            }
        }
    }
}
