using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages.Web.Providers;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.Messages;

namespace Paw.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ProviderActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            

            // Step 1. Get Provider
            Provider provider = filterContext.Controller.ControllerContext.RouteData.Values["Provider"] as Provider;

            if (provider == null)
            {
                return; // TODO: throw provider not found
            }

            filterContext.Controller.ViewData.Add("__Provider", provider);

            filterContext.Controller.ViewData.Add("__ProviderId", provider.Id);

            filterContext.Controller.ViewData.Add("__ProviderGroupId", provider.ProviderGroupId);

            // Step 2. Add ActiveProvider if authenticated
            if (HttpContext.Current?.User?.Identity?.IsAuthenticated == true)
            {
                CacheHelper.SetActiveProvider(provider.Id);
                CacheHelper.SetActiveProviderGroup(provider.ProviderGroupId);
            }

            base.OnActionExecuting(filterContext);
        }
        
    }
}