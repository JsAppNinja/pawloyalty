﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class AddSelectListAttribute : ExecuteValueAttribute
    {
        public string DataTextField
        {
            get { return _DataTextField; }
            set { _DataTextField = value; }
        }
        private string _DataTextField = string.Empty;

        public string DataValueField
        {
            get { return _DataValueField; }
            set { _DataValueField = value; }
        }
        private string _DataValueField = string.Empty;
        
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
        private string _Method = "ExecuteList";
        
        public override object Execute(object model, object container, IController controller)
        {
            // Step 1. Create the type
            object target = Activator.CreateInstance(this.Type);

            // Step 2. get the Method
            MethodInfo methodInfo = this.Type.GetMethod(this.Method);

            // Step 3. Set Controller Context Values and set properties on the target
            AttributeHelper.BindController(container, (ControllerBase)controller, target);

            // Step 4. Execute method
            List<ParameterInfo> parameterInfoList = methodInfo.GetParameters().ToList();
            List<object> paramList = new List<object>();
            if (parameterInfoList.Count > 0)
            {
                paramList = parameterInfoList.ConvertAll<object>(x => Type.Missing);
            }

            IEnumerable enumerable = methodInfo.Invoke(target, paramList.ToArray()) as IEnumerable;

            var selectList = new SelectList(enumerable, this.DataValueField, this.DataTextField);
            List<SelectListItem> result = new List<SelectListItem>(selectList.ToArray());

            return result;
        }
    }
}
