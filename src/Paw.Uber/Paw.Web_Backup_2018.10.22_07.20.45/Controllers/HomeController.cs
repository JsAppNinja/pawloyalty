using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Paw.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Appt.Init();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }

        #region ...

        public virtual JsonResult Meetings_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Appt.List.ToDataSourceResult(request));
        }

        public virtual JsonResult Meetings_Destroy([DataSourceRequest] DataSourceRequest request, Appt meeting)
        {
            if (ModelState.IsValid)
            {
                Appt.Delete(meeting, ModelState);
            }

            return Json(new[] { meeting }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Meetings_Create([DataSourceRequest] DataSourceRequest request, Appt meeting)
        {
            if (ModelState.IsValid)
            {
                Appt.Insert(meeting, ModelState);
            }

            return Json(new[] { meeting }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Meetings_Update([DataSourceRequest] DataSourceRequest request, Appt meeting)
        {
            if (ModelState.IsValid)
            {
                Appt.Update(meeting, ModelState);
            }

            return Json(new[] { meeting }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}