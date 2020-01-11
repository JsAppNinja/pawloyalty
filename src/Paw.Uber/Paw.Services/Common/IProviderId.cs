using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public interface IProviderId 
    {
        Guid ProviderId { get; set; }
        
    }
}
