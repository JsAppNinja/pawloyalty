using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class AddItemAttribute : ExecuteValueAttribute
    {
        public string DataTextField
        {
            get { return _DataTextField; }
            set { _DataTextField = value; }
        }
        private string _DataTextField = string.Empty;

        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;
        
        public Type Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private Type _Type = null;

        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        private string _Method = "ExecuteItem";
        
        public override object Execute(object property, object container, IController controller)
        {
            // Step 1. Create the type
            object target = Activator.CreateInstance(this.Type);

            // Step 2. get the Method
            MethodInfo methodInfo = this.Type.GetMethod(this.Method);

            // Step 3. Set Controller Context Values and set properties on the target
            AttributeHelper.BindController(container, (ControllerBase)controller, target);

            // Step 4. Get param list
            List<ParameterInfo> parameterInfoList = methodInfo.GetParameters().ToList();
            List<object> paramList = new List<object>();
            if (parameterInfoList.Count > 0)
            {
                paramList = parameterInfoList.ConvertAll<object>(x => Type.Missing);
            }

            // Step 5. Execute method
            object item = methodInfo.Invoke(target, paramList.ToArray()) as IEnumerable;
            if (item == null)
            {
                return null;
            }

            object value = item;

            // Step 6. Return property if necessary
            if (!string.IsNullOrEmpty(this.DataTextField))
            {
                PropertyInfo propertyInfo = item.GetType().GetProperty(this.DataTextField);
                if (propertyInfo == null)
                {
                    throw new InvalidOperationException($"Type [{item.GetType().Name}] does not include property [{this.DataTextField}].");
                }

                value = propertyInfo.GetValue(item);
            }

            return value;
        }
        
    }
}
