using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class GetViewDataValueAttribute : Attribute, IBindControllerValue
    {
        public string ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
        }
        private string _ParameterName = string.Empty;

        public void Init(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            if (string.IsNullOrEmpty(this.ParameterName))
                this.ParameterName = propertyInfo.Name;
        }

        public PropertyInfo PropertyInfo
        {
            get { return _PropertyInfo; }
            set { _PropertyInfo = value; }
        }
        private PropertyInfo _PropertyInfo = null;

        public virtual object GetValue(ControllerBase controller)
        {
            return controller.ViewData[this.ParameterName];
        }

        public virtual void BindControllerValue(ControllerBase controller, object item)
        {
            this.PropertyInfo.SetValue(item, this.GetValue(controller));
        }
    }
}
