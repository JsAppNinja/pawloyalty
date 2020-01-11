using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules.Blocks
{
    public class GetBlockEventList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        public DateTime? From
        {
            get { return _From; }
            set { _From = value; }
        }
        private DateTime? _From = null;

        public DateTime? To
        {
            get { return _To; }
            set { _To = value; }
        }
        private DateTime? _To = null;

        public List<SchedulerEvent> ExecuteList(bool useCache = true)
        {

            using (DataContext context = DataContext.CreateForMessage(this))
            {
                List<SchedulerEvent> result = context
                    .SchedulerEventSet
                    .Include("Provider")
                    .Include("Pet")
                    .Where(x => (this.SkuCategoryId == null || x.SkuCategoryId == this.SkuCategoryId)
                        && (this.From == null || x.Start >= this.From)
                        && (this.To == null || x.End <= this.To)
                        && x.ProviderId == this.ProviderId)
                        .ToList();

                return result;
            }
        }
    }
}
