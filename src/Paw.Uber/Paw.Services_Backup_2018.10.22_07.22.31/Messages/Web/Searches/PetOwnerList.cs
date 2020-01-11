using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class PetOwnerList
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;
        
        public List<PetProjection> PetList
        {
            get { return _PetList; }
            set { _PetList = value; }
        }
        private List<PetProjection> _PetList = new List<PetProjection>();

        public List<OwnerProjection> OwnerList
        {
            get { return _OwnerList; }
            set { _OwnerList = value; }
        }
        private List<OwnerProjection> _OwnerList = new List<Searches.OwnerProjection>();


    }
}
