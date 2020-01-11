using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public interface IGetProviderGroup<T> : IId, IProviderGroupId
        where T : class, IId, new()
    {

    }

    public interface IGetProviderGroup<T, R> : IId, IProviderGroupId
        where T : class, IId
        where R : class, new()
    {

    }
}
