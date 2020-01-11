using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Paw.Services.Messages.Web.Providers
{
    public class GetProviderList
    {
        public List<Provider> ExecuteList()
        {
            using (DataContext dataContext = DataContext.Create())
            {
                return dataContext
                    .ProviderSet
                    .Include(x => x.ProviderGroup)
                    .Include("SkuCategoryCollection.SchedulerType")
                    .OrderBy(x => x.Name).ToList();
            }
        }
    }
}
