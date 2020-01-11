using Paw.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
            filters.Add(new AddViewDataActionFilterAttribute());
            filters.Add(new ProviderActionFilterAttribute());
        }
    }
}
