using Paw.Web.Routes;
using System.Web.Mvc;

namespace Paw.Web.Areas.Reservations
{
    public class ReservationsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reservations";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reservations_default",
                "Reservations/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { provider = new ProviderRouteConstraint() }
            );
        }
    }
}