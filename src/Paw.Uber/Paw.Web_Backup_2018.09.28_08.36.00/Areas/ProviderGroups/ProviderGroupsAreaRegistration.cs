using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace Paw.Web.Areas.ProviderGroups
{
    public class ProviderGroupsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProviderGroups";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProviderGroups_default",
                "ProviderGroups/{providerGroupId}/{controller}/{action}/{id}",
                new { controller = "Invoice", action = "Index", id = UrlParameter.Optional },
                new { providerGroupId = new GuidRouteConstraint() }
            );
        }
    }
}