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
    public class SingleValueAttribute : ValidationAttribute
    {
        public string OtherProperty
        {
            get { return _OtherProperty; }
            set { _OtherProperty = value; }
        }
        private string _OtherProperty = String.Empty;

        public string OtherPropertyDisplayName
        {
            get { return _OtherPropertyDisplayName; }
            set { _OtherPropertyDisplayName = value; }
        }
        private string _OtherPropertyDisplayName = String.Empty;
        
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}.", this.OtherProperty));
            }

            object otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if ((value == null || string.IsNullOrEmpty(value.ToString())) || (otherPropertyValue == null || string.IsNullOrEmpty(otherPropertyValue.ToString())))
            {
                return null;
            }
            if (this.OtherPropertyDisplayName == null)
            {
                this.OtherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => validationContext.ObjectInstance, validationContext.ObjectType, this.OtherProperty).GetDisplayName();
            }
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
