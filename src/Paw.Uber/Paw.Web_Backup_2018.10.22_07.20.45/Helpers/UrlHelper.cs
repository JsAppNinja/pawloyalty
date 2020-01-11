using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Util;

namespace Paw.Web.Helpers
{
    public static class UrlHelper
    {

        public static QuerystringInfo GetQuerystringInfo(this HtmlHelper htmlHelper)
        {
            QuerystringInfo result = new QuerystringInfo(htmlHelper);

            return result;
        }
    }

    
}