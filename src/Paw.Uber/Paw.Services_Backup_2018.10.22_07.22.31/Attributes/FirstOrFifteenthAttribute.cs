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
    public class FirstOrFifthteenthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            string valueAsString = value.ToString();
            if (string.IsNullOrWhiteSpace(valueAsString))
            {
                return null;
            }
            
            DateTime dateTime;
            if (!DateTime.TryParse(valueAsString, out dateTime))
            {
                return null;
            }

            if (dateTime.Day == 1 || dateTime.Day == 15)
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
