using Paw.Web.Routes;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace Paw.Web.Areas.Providers
{
    public class ProvidersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Providers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "ProvidersLongScheduler_default",
            //    "Providers/{providerId}/Scheduler/{skuCategoryId}/{action}/{id}",
            //    new { controller = "Scheduler", action = "Overview", id = UrlParameter.Optional },
            //    constraints: new { providerId = new GuidRouteConstraint(), skuCategoryId = new GuidRouteConstraint(), provider = new ProviderRouteConstraint() }
            //);

            //context.MapRoute(
            //    "ProvidersLong_default",
            //    "Providers/{providerId}/{controller}/{action}/{id}",
            //    new { controller = "Provider", action = "Overview", id = UrlParameter.Optional },
            //    constraints: new { providerId = new GuidRouteConstraint(), provider = new ProviderRouteConstraint() }
            //);

            context.MapRoute(
                "ProvidersLongScheduler_default",
                "Providers/Scheduler/{skuCategoryId}/{action}/{id}",
                new { controller = "Scheduler", action = "Overview", id = UrlParameter.Optional },
                constraints: new { skuCategoryId = new GuidRouteConstraint(),
                    provider = new ProviderRouteConstraint() }
            );

            context.MapRoute(
                "ProvidersLong_default",
                "Providers/{controller}/{action}/{id}",
                new { controller = "Provider", action = "Overview", id = UrlParameter.Optional },
                constraints: new { provider = new ProviderRouteConstraint() }
            );

        }
    }
}