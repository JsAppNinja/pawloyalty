using Paw.Services.Attributes;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.SkuLines;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Messages.Web.PetReservations
{
    public class PetReservation : ISkuCategoryId
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid ReservationGroupId
        {
            get { return _RerservationGroupId; }
            set { _RerservationGroupId = value; }
        }
        private Guid _RerservationGroupId = Guid.Empty;
           
        [HiddenInput(DisplayValue = false)]
        public Guid SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid _SkuCategoryId = Guid.Empty;

        [StartRow]
        [Width(12)]
        [HiddenInput(DisplayValue=false)]
        [Required]
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [StartRow]
        [Section(Text ="Services")]
        [Width(12)]
        [Display(Name = "Primary Service")]
        [AddItemList(Method ="ExecuteList", Type = typeof(GetPrimarySkuList))]
        [UIHint("SkuChooser")]
        [Required]
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [StartRow]
        [Width(12)]
        [Display(Name = "Extras")]
        [UIHint("SkuListChooser")]
        public List<Guid> Extras
        {
            get { return _Extras; }
            set { _Extras = value; }
        }
        private List<Guid> _Extras = new List<Guid>();
        
        [StartRow]
        [Width(12)]
        [AddSelectList(DataTextField="FullName", DataValueField="Id", Method = "ExecuteList", Type = typeof(GetEmployeeInfoList))]
        [Display(Name = "Provider")]
        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        [StartRow]
        [Width(12)]
        [UIHint("StartDate")]
        [DataType(DataType.Date)]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime? _StartDate = null;

        [StartRow]
        [Width(12)]
        [UIHint("StartTime")]
        [DataType(DataType.Time)]
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        private DateTime? _StartTime = null;
        
        [ScaffoldColumn(false)]
        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }
        private bool _IsNew = true;


    }
}
