using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class GetPetOwnerLink
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        public PetOwnerLink ExecuteItem()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                Pet pet = context.PetSet.Include("Owner").Where(x => x.ProviderGroupId == this.ProviderGroupId && x.Id == this.Id).SingleOrDefault();
                if (pet == null) return null;

                return new PetOwnerLink() { Breed = "Unknown", Id = pet.Id, FirstName = pet.Owner.FirstName, LastName = pet.Owner.LastName, OwnerId = pet.Owner.Id, Pet = pet.Name };
            }
        }
    }
}
