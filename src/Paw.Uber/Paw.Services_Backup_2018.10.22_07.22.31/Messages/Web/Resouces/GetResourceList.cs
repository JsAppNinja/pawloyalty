using Paw.Services.Common;
using Paw.Services.Messages.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Resouces
{
    public class GetResourceList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<Resource> ExecuteList(bool useCache = true)
        {
            Provider provider = new GetProvider() { Id = this.ProviderId }.ExecuteItem(useCache);

            if (provider == null)
            {
                return new List<Resource>();
            }

            return provider
                .ResourceCollection
                .OrderBy(x => x.ShortDescription)
                    .ToList();
        }

    }
}
