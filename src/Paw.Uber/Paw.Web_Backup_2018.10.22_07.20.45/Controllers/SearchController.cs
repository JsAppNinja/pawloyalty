using Paw.Services.Messages.Web.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Kendo.Mvc;

namespace Paw.Web.Controllers
{
    public class SearchController : AuthorizeController
    {
        public SearchController()
        {
            
        }

        // GET: Search
        public ActionResult Index()
        {
            
            return View();
        }
        
        public JsonResult Result(Guid providerGroupId, [DataSourceRequest] DataSourceRequest request)
        {
            string filterValue = string.Empty;

            // Step 1. Get the filter
            if (request.Filters != null && request.Filters.Count > 0)
            {
                FilterDescriptor firstFilter = request.Filters[0] as FilterDescriptor;

                if (firstFilter != null)
                {
                    filterValue = firstFilter.Value as string;
                }
            }

            // TODO:Add minimum filter to mitigate data leak

            var result = PetSearchService.Search(providerGroupId, filterValue);
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}