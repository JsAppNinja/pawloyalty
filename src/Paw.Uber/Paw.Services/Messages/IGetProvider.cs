using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public interface IGetProvider<T> : IId, IProviderId
        where T : class, IId, new()
    {

    }

    public interface IGetProvider<T, R> : IId, IProviderId
        where T : class, IId
        where R : class, new()
    {

    }
}
