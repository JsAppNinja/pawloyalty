using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages;
using Paw.Web.Helpers;

namespace Paw.Web.Areas.Providers.Controllers
{
    public class PetController : Controller
    {

        public ActionResult Profile(GetPet getPet)
        {
            Pet pet = getPet.ExecuteItem();
            return View(pet);
        }
        
        [HttpGet]
        public ActionResult Add(Guid ownerId)
        {
            this.AddAjaxPost("/Providers/Pet/Add", "Add Pet", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            return View("FormModal", new AddPet() { OwnerId=ownerId });
        }

        [HttpPost]
        public ActionResult Add(AddPet addPet)
        {
            if (this.ModelState.IsValid)
            {
                int result = addPet.ExecuteNonQuery();
                return new JsonResult() { Data = new { Result = "Success", Url = $"/Providers/Owner/Profile/{addPet.OwnerId}" } };
            }

            this.AddAjaxPost("/Providers/Pet/Add", "Add Pet", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            return View("FormModal", addPet);
        }

        [HttpGet]
        public ActionResult Update(GetUpdatePet getUpdatePet)
        {
            this.AddAjaxPost($"/Providers/Pet/Update/{getUpdatePet.Id}", "Update Pet", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            var updatePet = getUpdatePet.ExecuteItem();
            return View("FormModal", updatePet);
        }

        [HttpPost]
        public ActionResult Update(UpdatePet updatePet)
        {
            if (this.ModelState.IsValid)
            {
                int result = updatePet.ExecuteNonQuery();
                return new JsonResult() { Data = new { Result = "Success" } };
            }

            this.AddAjaxPost($"/Providers/Pet/Update/{updatePet.Id}", "Update Pet", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            return View("FormModal", updatePet);
        }
    }
}