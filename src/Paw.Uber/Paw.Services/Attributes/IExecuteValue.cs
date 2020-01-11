using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public interface IExecuteValue
    {
        string Key { get; }

        bool Global { get; }
        
        object Execute(object property, object container, IController controller);
    }
}
