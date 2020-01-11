using Paw.Services.Common;
using Paw.Services.Messages.Web.Owners;
using Paw.Services.Messages.Web.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Owners
{
    public class GetOwnerName
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;
        
        public string ExecuteString(bool useCache = true)
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                Owner owner = null;

                if (this.OwnerId != null)
                {
                    owner = new GetOwner() { Id = this.OwnerId.Value, ProviderGroupId = this.ProviderGroupId }.ExecuteItem(useCache);
                }
                else if (this.PetId != null)
                {
                    Pet pet = new GetPet() { Id = this.PetId.Value, ProviderGroupId = this.ProviderGroupId }.ExecuteItem(useCache);
                    if (pet != null && pet.Owner != null)
                    {
                        owner = pet.Owner;
                    }
                }

                if (owner == null) return string.Empty;

                return owner.FullName;
            }
        }


    }
}
