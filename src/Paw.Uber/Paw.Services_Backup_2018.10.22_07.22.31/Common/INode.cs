using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public interface INode<T> : IParentId, IId where T : class, IId, IParentId
    {
        ICollection<T> ChildCollection { get; set; }
        
    }
}
