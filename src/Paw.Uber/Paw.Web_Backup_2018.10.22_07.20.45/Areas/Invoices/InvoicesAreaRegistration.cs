using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace Paw.Web.Areas.Invoices
{
    public class InvoicesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Invoices";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Invoices_default",
                "Providers/{providerId}/Invoices/{controller}/{action}/{id}",
                new { controller = "Invoice", action = "Index", id = UrlParameter.Optional },
                new { providerId = new GuidRouteConstraint() }
            );
        }
    }
}