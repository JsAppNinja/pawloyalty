using Paw.Web.Routes;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace Paw.Web.Areas.Blocks
{
    public class BlocksAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Blocks";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Blocks_Category",
                "Blocks/{SkuCategoryId}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { skuCategoryId = new GuidRouteConstraint() }
            );


            context.MapRoute(
                "Blocks_default",
                "Blocks/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { provider = new ProviderRouteConstraint() }
            );
        }
    }
}