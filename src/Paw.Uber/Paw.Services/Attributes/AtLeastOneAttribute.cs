using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class AtLeastOneAttribute : ValidationAttribute
    {
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        private string _GroupName = string.Empty;
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Step 1. Get all properties in group
            List<PropertyInfo> propertyList = new List<PropertyInfo>();
            foreach (PropertyInfo propertyInfo in validationContext.ObjectType.GetProperties())
            {
                AtLeastOneAttribute atLeastOneAttribute = propertyInfo.GetCustomAttribute<AtLeastOneAttribute>();
                if (atLeastOneAttribute != null && atLeastOneAttribute.GroupName == this.GroupName)
                {
                    propertyList.Add(propertyInfo);
                }
            }

            // Step 2. Determine if at least one is true
            bool atLeastOne = false;
            foreach (var propertyInfo in propertyList)
            {
                object propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
                if (propertyValue != null && propertyValue is bool && (bool)propertyValue == true)
                {
                    atLeastOne = true;
                    break;
                }
            }

            if (atLeastOne)
            {
                return null;
            }
            else
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            
        }
    }
}
