using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages
{
    public interface IGet<T> : IId
        where T : class, IId, new()
    {

    }

    public interface IGet<T, R> : IId
        where T : class, IId
        where R : class, new()
    {

    }
}
