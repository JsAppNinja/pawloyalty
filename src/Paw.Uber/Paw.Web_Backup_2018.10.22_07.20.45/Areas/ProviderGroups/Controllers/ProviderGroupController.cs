using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.ProviderGroups.Controllers
{
    [ProviderActionFilter]
    public class ProviderGroupController : AuthorizeController
    {
        // GET: ProviderGroups/ProviderGroup
        public ActionResult Index()
        {
            return View();
        }
    }
}