using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Paw.Services.Attributes;

namespace Paw.Services.Messages.Web.Pets
{
    public class GetPetListByOwnerId
    {
        [GetModelProperty(ParameterName = "OwnerId")]
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        [GetViewDataValue(ParameterName = "__ProviderGroupId")]
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public bool IncludeDeceased
        {
            get { return _IncludeDeceased; }
            set { _IncludeDeceased = value; }
        }
        private bool _IncludeDeceased = false;


        public List<Pet> ExecuteList()
        {
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                return dataContext.PetSet
                    .Include("Owner.PetCollection.Breed")
                    .Where(x => x.OwnerId == this.OwnerId && x.ProviderGroupId == this.ProviderGroupId && (this.IncludeDeceased || x.Deceased == false))
                    .ToList();
            }
        }
    }
}
