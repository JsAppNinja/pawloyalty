using Kendo.Mvc;
using Kendo.Mvc.UI;
using Newtonsoft.Json.Linq;
using Paw.Services.Common;
using Paw.Services.Messages.Web.PetLinks;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Searches;
using Paw.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Paw.Web.Areas.ProviderGroups.Controllers
{
    public class SearchController : AuthorizeController
    {
        // GET: Providers/Search
        public ActionResult Index()
        {
            return View();
        }

        public JsonStringResult PetOwner(Guid providerGroupId, [DataSourceRequest] DataSourceRequest request)
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

            var result = new GetPetLinkStringList() { ProviderGroupId = providerGroupId, Query = filterValue }.ExecuteScalar();
            return new JsonStringResult(result.AsJson());
        }

        [Obsolete]
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

        public class JsonStringResult : JsonResult
        {
            public JsonStringResult(string json) : base()
            {
                this.Json = json;
            }

            public string Json
            {
                get { return _Json; }
                set { _Json = value; }
            }
            private string _Json = String.Empty;
            
            
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Json GET not allowed.");
                }
                HttpResponseBase response = context.HttpContext.Response;
                if (string.IsNullOrEmpty(this.ContentType))
                {
                    response.ContentType = "application/json";
                }
                else
                {
                    response.ContentType = this.ContentType;
                }
                if (this.ContentEncoding != null)
                {
                    response.ContentEncoding = this.ContentEncoding;
                }
                if (this.Json != null)
                {
                    response.Write(this.Json);
                }
            }
        }
    }
}