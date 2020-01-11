using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class GetPetOwnerList
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public PetOwnerList ExecuteList()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                PetOwnerList result = new PetOwnerList() { ProviderGroupId = this.ProviderGroupId };

                result.OwnerList = context.OwnerSet
                    .Include("PetCollection")
                    .Where(x => x.ProviderGroupId == this.ProviderGroupId)
                    .Select(x => new OwnerProjection() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, PhoneNumber = x.PhoneNumber ?? String.Empty, PetList = x.PetCollection.Select(y => new PetProjection() { Id = y.Id, Name = y.Name ?? string.Empty }).ToList() })
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ToList();

                // Assign owner, add pet list
                result.OwnerList.ForEach(x => x.PetList.ForEach(y => {
                    y.Owner = x;
                    result.PetList.Add(y);
                }));

                // Sort pet list
                result.PetList.Sort((x, y) => x.Name.CompareTo(y.Name));

                return result;
            }
        }
    }
}
