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
    public class FileNameAttribute : ValidationAttribute
    {
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString())) return null;

            if(ContainsInvalidCharacters(value.ToString())) return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

            return null;
        }

        private static bool ContainsInvalidCharacters(string fileName)
        {
            for (var i = 0; i < fileName.Length; i++)
            {
                int c = fileName[i];

                if (c == '\"' || c == '/' || c == '\\' || c == '<' || c == '>' || c == '|' || c == ':' || c == '*' || c == '?' || c < 32)
                    return true;
            }

            return false;
        }
    }
}
