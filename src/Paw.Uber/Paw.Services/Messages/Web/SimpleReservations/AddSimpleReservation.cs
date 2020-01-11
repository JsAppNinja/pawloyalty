using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.SimpleReservations
{
    public class AddSimpleReservation
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.NewGuid();

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        [DataType(DataType.DateTime)]
        public string DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
        private string _DateTime = String.Empty;
        
        [UIHint("OwnerIdLookup")]
        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;
        
        [UIHint("List")]
        public List<AddPetService> AddPetServiceList
        {
            get { return _AddPetServiceList; }
            set { _AddPetServiceList = value; }
        }
        private List<AddPetService> _AddPetServiceList = new List<AddPetService>();


    }
}
