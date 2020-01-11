using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public interface IBindModelValue : IPropertyInfoInit
    {
        void BindModelValue(object model, object item);
    }
}
