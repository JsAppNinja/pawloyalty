using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Paw.Services.Messages.Web.Providers
{
    public class GetUserProviderGroupList
    {
        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private Guid _UserId = Guid.Empty;
        

        public List<ProviderGroup> ExecuteList()
        {
            using (DataContext dataContext = DataContext.Create())
            {
                return dataContext.ProviderGroupSet.Include(x => x.ProviderCollection).ToList();
            }
        }

    }
}
