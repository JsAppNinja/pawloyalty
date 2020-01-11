using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Paw.Services.Messages.Web.Providers
{
    public class GetUserProviderList
    {
        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private Guid _UserId = Guid.Empty;

        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public List<Provider> ExecuteList()
        {
            using (DataContext dataContext = DataContext.Create())
            {
                // return dataContext.ProviderSet.Where(x => x.ProviderGroupId == this.ProviderGroupId).OrderBy(x => x.Name).ToList();
                return dataContext
                    .ProviderSet
                    .Include(x=> x.ProviderGroup)
                    .OrderBy(x => x.Name).ToList();
            }
        }


    }
}
