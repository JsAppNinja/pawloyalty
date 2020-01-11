using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public interface IDeleteProviderGroup<T> : IId, IProviderGroupId
        where T : class, IId, IProviderGroupId
    {

    }
}
