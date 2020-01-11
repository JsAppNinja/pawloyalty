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
    public class GetEmployeeInfoList
    {
        [GetViewDataValue(ParameterName = "__ProviderId")]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public bool IncludeTerminated
        {
            get { return _IncludeTerminated; }
            set { _IncludeTerminated = value; }
        }
        private bool _IncludeTerminated = false;
        
        public List<EmployeeInfo> ExecuteList(bool useCache = true)
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                return context
                    .EmployeeSet
                    .Include("Provider")
                    .Where(x => x.ProviderId == this.ProviderId && (!this.IncludeTerminated || x.TerminationDate != null))
                    .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
                    .Select(x => new EmployeeInfo() { FirstName = x.FirstName, Id = x.Id, Initials = x.Initials, LastName = x.LastName  })
                    .ToList();
            }
        }

    }
}
