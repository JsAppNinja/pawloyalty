using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Controllers
{
    public class TesterController : AuthorizeController
    {
        // GET: Tester
        public ActionResult Search()
        {
            return View();

        }
    }
}