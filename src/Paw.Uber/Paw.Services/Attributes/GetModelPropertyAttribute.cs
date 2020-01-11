using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class GetModelPropertyAttribute : GetViewDataValueAttribute
    {

        public override object GetValue(object model, ControllerBase controller)
        {
            if (model == null) return null;

            PropertyInfo propertyInfo = model.GetType().GetProperty(this.ParameterName);
            if (propertyInfo == null) return null;

            return propertyInfo.GetValue(model);
        }

    }
}
