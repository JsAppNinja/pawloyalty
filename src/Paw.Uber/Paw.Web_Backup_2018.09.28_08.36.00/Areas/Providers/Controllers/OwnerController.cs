using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages.Web.Owners;
using Paw.Services.Messages;

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
            return View("FormPage", new AddOwner() { });
        }

        [HttpPost]
        public ActionResult Add(AddOwner addOwner)
        {
            if (this.ModelState.IsValid)
            {
                int result = addOwner.ExecuteNonQuery();
                return Redirect($"/Providers/Owner/Profile/{addOwner.Id}");
            }

            return View("FormPage", addOwner);
        }

        [HttpGet]
        public ActionResult Update(GetUpdateOwner getUpdateOwner)
        {
            this.ViewData["CancelUrl"] = $"/Providers/Owner/Profile/{getUpdateOwner.Id}";
 

            var updateOwner = getUpdateOwner.ExecuteItem();
            return View("FormPage", updateOwner);
        }

        [HttpPost]
        public ActionResult Update(UpdateOwner updateOwner)
        {
            this.ViewData["CancelUrl"] = $"/Providers/Owner/Profile/{updateOwner.Id}";

            if (this.ModelState.IsValid)
            {
                int result = updateOwner.ExecuteNonQuery();
                return Redirect($"/Providers/Owner/Profile/{updateOwner.Id}");
            }

            return View("FormPage", updateOwner);
        }
    }
}