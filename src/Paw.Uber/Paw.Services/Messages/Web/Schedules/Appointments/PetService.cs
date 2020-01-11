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

namespace Paw.Services.Messages.Web.Schedules.Appointments
{
    public class PetService : IId
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [ScaffoldColumn(false)]
        public Guid PetAppointmentId
        {
            get { return _PetAppointmentId; }
            set { _PetAppointmentId = value; }
        }
        private Guid _PetAppointmentId = Guid.Empty;


        [UIHint("PetList")]
        public List<Guid> PetList
        {
            get { return _PetList; }
            set { _PetList = value; }
        }
        private List<Guid> _PetList = new List<Guid>();


        [UIHint("CheckboxList")]
        [AddSelectList(DataTextField = "Value", DataValueField = "Key", Method = "ExecuteList", Type = typeof(GetSkuGroupListBySkuId))]
        public List<Guid> SelectedServiceList
        {
            get { return _SelectedServiceList; }
            set { _SelectedServiceList = value; }
        }
        private List<Guid> _SelectedServiceList = new List<Guid>();


    }
}
