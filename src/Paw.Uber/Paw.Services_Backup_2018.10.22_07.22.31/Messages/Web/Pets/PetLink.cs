using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Pets
{
    public class PetLink
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;
        
        public string Pet
        {
            get { return _Pet; }
            set { _Pet = value; }
        }
        private string _Pet = String.Empty;

        public string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        private string _Owner = String.Empty;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _PhoneNumber = String.Empty;
        
        public string Breed
        {
            get { return _Breed; }
            set { _Breed = value; }
        }
        private string _Breed = String.Empty;
        
        // ProviderGroupId
    }
}
