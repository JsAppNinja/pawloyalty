using Paw.Services.Messages.Web.ProviderGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Controllers
{
    public class AppInstanceController : Controller
    {
        // GET: AppInstance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RefreshProviderGroup(GetProviderGroup getProviderGroup)
        {
            var result = getProviderGroup.ExecuteItem(false);
            return new EmptyResult();
        }

        public ActionResult RefreshActiveProvider()
        {
            // Step 1. Read Providers


            return new EmptyResult();
        }
    }
}