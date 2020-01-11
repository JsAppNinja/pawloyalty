using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public interface IProviderGroupId : IId
    {
        Guid ProviderGroupId { get; set; }
    }
}
