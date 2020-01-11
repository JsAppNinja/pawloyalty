using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace Paw.Web.Areas.Profiles
{
    public class ProfilesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Profiles";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Profiles_default",
                "Providers/{providerId}/Profile/{profileId}/{controller}/{action}/{id}",
                new { controller = "Profile", action = "Index", id = UrlParameter.Optional },
                new { providerId = new GuidRouteConstraint(), profileId = new GuidRouteConstraint() }
            );
            
        }
    }
}