using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public interface IChild
    {
        Guid? ParentId { get; set; }

        Guid Root { get; set; }
    }
}
