using Paw.Services.Messages.Web.DynamicControls;
using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Providers.Controllers
{
    [ProviderActionFilter]
    public class ProviderController : AuthorizeController
    {
    
        public ActionResult Overview(Guid providerId)
        {
            return View();
        }

        
    }
}