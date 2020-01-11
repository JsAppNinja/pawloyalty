using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Employees
{
    public class GetEmployeeList
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<EmployeeInfo> ExecuteList(bool useCache = true)
        {

            Provider cacheResult = null;
            if (useCache && CacheHelper.TryGetResult<Provider>(this.ProviderId.ToString(), out cacheResult))
            {
                List<EmployeeInfo> cacheItem = cacheResult.EmployeeCollection.Select(x => new EmployeeInfo() { FirstName = x.FirstName, Id = x.Id, Initials = x.Initials, LastName = x.LastName }).ToList();
                if (cacheItem != null)
                {
                    Logger.Info($"Returned [{typeof(List<EmployeeInfo>)}] from cache graph.");
                    return cacheItem;
                }
            }

            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context
                    .EmployeeSet
                    .Include("Provider")
                    .Where(x => x.ProviderId == this.ProviderId)
                    .Select(x => new EmployeeInfo() { FirstName = x.FirstName, Id = x.Id, Initials = x.Initials, LastName = x.LastName  })
                    .ToList();
            }
        }

    }
}
