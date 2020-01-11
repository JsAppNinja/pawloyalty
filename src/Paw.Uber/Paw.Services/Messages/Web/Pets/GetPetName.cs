using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Pets
{
    public class GetPetName
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

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
                Pet pet = new GetPet() { Id = this.PetId.Value, ProviderGroupId = this.ProviderGroupId }.ExecuteItem(useCache);
                if (pet == null)
                {
                    return string.Empty;
                }

                return pet.Name;
            }
        }


    }
}
