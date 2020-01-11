using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Paw.Services.Messages.Web.ProviderGroups;
using Paw.Services.Util;

namespace Paw.Services.Messages.Web.Pets
{
    public class GetPetLinkList
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public List<PetLink> ExecuteList(bool useCache = true)
        {
            ProviderGroup providerGroup = new GetProviderGroup() { Id = this.ProviderGroupId }.ExecuteItem(useCache);

            if (providerGroup == null)
            {
                return new List<PetLink>();
            }

            return providerGroup
                .PetCollection
                .OrderBy(x => x.Name)
                    .ThenBy(x => x.Owner.LastName)
                    .ThenBy(x => x.Owner.FirstName)
                    .Select(x => new PetLink() { Id = x.Id, Pet = x.Name, Owner = x.Owner.FirstName + " " + x.Owner.LastName, Breed = x.Breed.Name.ToTitleCase("Unknown") })
                    .ToList();
            

        }


    }
}
