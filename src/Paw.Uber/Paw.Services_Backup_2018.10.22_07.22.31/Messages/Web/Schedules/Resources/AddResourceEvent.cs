using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Schedules.Resources
{
    public class AddResourceEvent : IAdd<SchedulerEvent>
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        [StartRow]
        [Width(10)]
        [Display(Name = "Owner")]
        [UIHint("ReadOnlyOwnerId")]
        [Required]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [StartRow]
        [Width(10)]
        [Display(Name = "Pet")]
        [UIHint("RadioButtonList")]
        [AddSelectList(DataTextField = "Name", DataValueField = "Id", Type = typeof(GetPetListByOwnerId))]
        [Required]
        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;


        [StartRow]
        [Width(10)]
        [Display(Name = "Provider")]
        [Required]
        [AddSelectList(DataTextField = "FullName", DataValueField = "Id", Type = typeof(GetEmployeeList))]
        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;
    }
}
