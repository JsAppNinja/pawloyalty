using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paw.Web.Helpers
{
    public static class RouteHelpers
    {

        public static RouteValueDictionary ExecuteSet(this RouteValueDictionary @this, string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return @this;
            }

            if (@this.ContainsKey(key))
            {
                @this[key] = value;
            }
            else
            {
                @this.Add(key, value);
            }

            return @this;
        }

        public static RouteValueDictionary ExecuteAddExistingQuerystringValues(this RouteValueDictionary @this, HtmlHelper html)
        {
            var querystring = html.ViewContext.RequestContext.HttpContext.Request.QueryString;
            foreach (string key in querystring.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                @this.ExecuteSet(key, querystring[key]);
            }

            return @this;
        }

        public static Guid? GetRouteValueAsGuid(this HtmlHelper @this, string key)
        {
            string valueAsString = @this.ViewContext.RouteData.Values[key] as string;

            Guid result;
            if (Guid.TryParse(valueAsString, out result))
            {
                return result;
            }

            return (Guid?)null;
        }

        public static PawContext GetPawContext(this HtmlHelper @this)
        {
            return PawContext.Create(@this);
        }

    }
}