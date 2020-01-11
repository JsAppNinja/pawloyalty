using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Schedules.Controllers
{
    [ProviderActionFilter]
    public class BlockController : AuthorizeController
    {
        // GET: Schedules/Block
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filter()
        {
            return View();
        }
    }
}