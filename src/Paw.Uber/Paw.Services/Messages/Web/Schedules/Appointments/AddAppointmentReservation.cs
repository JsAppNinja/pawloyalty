using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paw.Services.Messages.Web.Employees;
using System.Web.Mvc;

namespace Paw.Services.Messages.Web.Schedules.Appointments
{
    public class AddAppointmentReservation
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [HiddenInput(DisplayValue = false)]
        public Guid SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid _SkuCategoryId = Guid.Empty;

        [StartRow]
        [Width(12)]
        [Display(Name = "Primary Service")]
        [AddSelectList(DataTextField = "NameAndPrice", DataValueField = "Id", Method = "ExecuteList", Type = typeof(GetPrimarySkuList))]
        public Guid? PrimarySkuId
        {
            get { return _PrimarySkuId; }
            set { _PrimarySkuId = value; }
        }
        private Guid? _PrimarySkuId = null;

        [StartRow]
        [Width(12)]
        [Display(Name = "Provider")]
        [AddSelectList(DataTextField = "FullName", DataValueField = "Id", Method = "ExecuteList", Type = typeof(GetEmployeeInfoList))]
        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;


        [StartRow]
        [Width(12)]
        [DataType(DataType.DateTime)]
        [Display(Name ="Arrive")]
        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        [StartRow]

        [Width(12)]
        [Display(Name = "Pet(s)")]
        [UIHint("PetList")]
        public List<Guid> PetList
        {
            get { return _PetList; }
            set { _PetList = value; }
        }
        private List<Guid> _PetList = new List<Guid>();


        // Assign to employee

        


    }
}
