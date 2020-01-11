using Paw.Services.Attributes;
using Paw.Services.Attributes.ClientData;
using Paw.Services.Messages.Web.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules
{
    public class ScheduleFilter
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;
        
        [UIHint("FilterList")]
        [Display(Name ="Providers:")]
        [AddAttribute(Key ="DefaultSelected", Value="true")]
        [AddSelectList(DataTextField = "FullName", DataValueField = "Id", Type = typeof(GetEmployeeList))]
        public List<Guid> EmployeeIdList
        {
            get { return _EmployeeIdList; }
            set { _EmployeeIdList = value; }
        }
        private List<Guid> _EmployeeIdList = new List<Guid>();


    }
}
