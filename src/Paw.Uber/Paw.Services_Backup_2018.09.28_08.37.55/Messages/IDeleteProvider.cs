using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public interface IDeleteProvider<T> : IId, IProviderId
        where T : class, IId, IProviderId
    {

    }
}
