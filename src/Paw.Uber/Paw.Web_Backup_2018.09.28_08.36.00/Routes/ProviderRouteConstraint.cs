using Paw.Services.Common;
using Paw.Services.Messages.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Paw.Web.Routes
{
    public class ProviderRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Step 1. 
            var host = httpContext.Request.Headers["Host"].Split('.');
            var subdomain = host[0];

            // Step 2. Handle localhost
            if (subdomain.ToLower().StartsWith("localhost"))
            {
                string localhostDomain = System.Configuration.ConfigurationManager.AppSettings["TestDomain"];
                if (string.IsNullOrEmpty(localhostDomain))
                {
                    return false;
                }
                else
                {
                    subdomain = localhostDomain;
                }
            }

            // Step 3.
            Provider provider = new GetProviderByDomain() { Domain = subdomain }.ExecuteItem();
            if (provider == null)
            {
                return false;
            }

            // Step 4.
            if (!values.ContainsKey("Provider"))
            {
                values.Add("Provider", provider);
                values.Add("ProviderId", provider.Id);
                values.Add("ProviderGroupId", provider.ProviderGroupId);
            }

            return true;
        }
    }
}