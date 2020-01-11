using Kendo.Mvc;
using Kendo.Mvc.UI;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.ProviderGroups.Controllers
{
    public class SearchController : Controller
    {
        // GET: Providers/Search
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult PetOwner(Guid providerGroupId, [DataSourceRequest] DataSourceRequest request)
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

            var result = new GetPetLinkSearch() { ProviderGroupId = providerGroupId, Query = filterValue }.ExecuteList();
            //var result = SearchService.QueryPetOwner(providerGroupId, filterValue);

            return Json(new { Data = result });
        }

        public JsonResult Owner(Guid providerGroupId, [DataSourceRequest] DataSourceRequest request)
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

            var result = SearchService.QueryOwner(providerGroupId, filterValue);
            return Json(new { Data = result });
        }
    }
}