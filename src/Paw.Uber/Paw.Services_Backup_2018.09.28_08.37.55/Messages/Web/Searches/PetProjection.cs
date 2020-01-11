using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Searches
{
    public class PetProjection // Pet projection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public OwnerProjection Owner { get; set; }
    }
}
