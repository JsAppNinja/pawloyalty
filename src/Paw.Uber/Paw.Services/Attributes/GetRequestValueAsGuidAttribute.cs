using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class GetRequestValueAsGuidAttirbute : GetViewDataValueAttribute
    {
        public bool Required
        {
            get { return _Required; }
            set { _Required = value; }
        }
        private bool _Required = false;
        
        public override object GetValue(object model, ControllerBase controller)
        {
            string value = controller.ControllerContext.HttpContext.Request[this.ParameterName] as string;

            if (value == null)
            {
                return (Guid?)null;
            }

            Guid result;
            if (!Guid.TryParse(value, out result))
            {
                throw new Exception();
            }

            return result;
        }
    }
}
