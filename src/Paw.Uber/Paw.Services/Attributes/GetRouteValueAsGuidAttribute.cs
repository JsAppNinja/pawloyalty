using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class GetRouteValueAsGuidAttirbute : GetViewDataValueAttribute
    {
        public override object GetValue(object model, ControllerBase controller)
        {
            string value = controller.ControllerContext.RouteData.Values[this.ParameterName] as string;

            if (value == null)
                throw new Exception();

            Guid result;
            if (!Guid.TryParse(value, out result))
            {
                throw new Exception();
            }

            return result;
        }
    }
}
