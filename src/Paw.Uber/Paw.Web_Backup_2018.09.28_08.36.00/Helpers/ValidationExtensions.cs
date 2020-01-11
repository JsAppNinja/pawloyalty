using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Helpers
{
    public static class ValidationExtensions
    {
        public static string ValidationClasses(this HtmlHelper helper, string modelName, string classes)
        {
            if (helper.ViewData.ModelState[modelName] == null || helper.ViewData.ModelState[modelName].Errors == null || helper.ViewData.ModelState[modelName].Errors.Count == 0)
            {
                return String.Empty;
            }

            return classes;
        }
    }
}