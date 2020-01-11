using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paw.Web.Helpers
{
    public static class TextHelpers
    {
        public static MvcHtmlString DisplayDate(this DateTime? expression)
        {
            return MvcHtmlString.Create(string.Format("{0:MM/dd/yy hh:mm tt}", expression));
        }
        
    }
}