using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class OwnerProjection // Owner projection
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }


        public List<PetProjection> PetList
        {
            get { return _PetList; }
            set { _PetList = value; }
        }
        private List<PetProjection> _PetList = new List<PetProjection>();

    }
}
