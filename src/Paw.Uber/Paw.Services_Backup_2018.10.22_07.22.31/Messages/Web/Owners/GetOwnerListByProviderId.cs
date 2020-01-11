using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Owners
{
    public class GetOwnerListByProviderId
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }

        private Guid _ProviderId = Guid.Empty;

        public List<Owner> ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                Provider provider = context.ProviderSet.Where(x => x.Id == this.ProviderId).SingleOrDefault();

                if (provider == null) return null;

                return context.OwnerSet
                    .Include("ProviderGroup")
                    .Where(x => x.ProviderGroupId == provider.ProviderGroupId)
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .ToList();
            }
        }
    }
}
