using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Styles.Controllers
{
    public class BaseController : Controller
    {
        // GET: Styles/Base
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Colours()
        {
            return View();
        }

        public ActionResult Componenets()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return View();
        }

        public ActionResult Patterns()
        {
            return View();
        }

        public ActionResult Typography()
        {
            return View();
        }
    }
}