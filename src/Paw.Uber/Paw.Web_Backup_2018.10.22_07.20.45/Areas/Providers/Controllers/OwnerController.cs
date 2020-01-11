using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages.Web.Owners;
using Paw.Services.Messages;
using Paw.Services.UI;
using Paw.Web.Helpers;

namespace Paw.Web.Areas.Providers.Controllers
{
    public class OwnerController : Controller
    {
        // GET: Providers/Owner
        [HttpGet]
        public ActionResult Profile(GetOwner getOwner)
        {
            Owner owner = getOwner.ExecuteItem();
            return View(owner);
        }

        [HttpGet]
        public ActionResult Add()
        {
            this.AddAjaxPost("/Providers/Owner/Add", "Add Owner", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            
            return View("FormModal", new AddOwner(){ });
        }

        [HttpPost]
        public ActionResult Add(AddOwner addOwner)
        {
            if (this.ModelState.IsValid)
            {
                int result = addOwner.ExecuteNonQuery();
                return new JsonResult() { Data = new { Result = "Success", Url = $"/Providers/Owner/Profile/{addOwner.Id}" } };
            }

            this.AddAjaxPost("/Providers/Owner/Add", "Add Owner", x => x.OnSuccess = "paw.onAjaxFormSuccess");

            return View("FormModal", addOwner);
        }

        [HttpGet]
        public ActionResult Update(GetUpdateOwner getUpdateOwner)
        {
            this.AddAjaxPost($"/Providers/Owner/Update/{getUpdateOwner.Id}", "Update Owner", x => x.OnSuccess = "paw.onAjaxFormSuccess");

            var updateOwner = getUpdateOwner.ExecuteItem();
            return View("FormModal", updateOwner);
        }

        [HttpPost]
        public ActionResult Update(UpdateOwner updateOwner)
        {
            if (this.ModelState.IsValid)
            {
                int result = updateOwner.ExecuteNonQuery();
                return new JsonResult() { Data = new { Result = "Success" } };
            }

            this.AddAjaxPost($"/Providers/Owner/Update/{updateOwner.Id}", "Update Owner", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            return View("FormModal", updateOwner);
        }
    }
}