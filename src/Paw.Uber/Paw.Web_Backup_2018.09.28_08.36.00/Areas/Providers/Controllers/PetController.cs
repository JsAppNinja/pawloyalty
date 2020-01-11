using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Messages;

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
            return View("FormPage", new AddPet() { OwnerId=ownerId });
        }

        [HttpPost]
        public ActionResult Add(AddPet addPet)
        {
            if (this.ModelState.IsValid)
            {
                int result = addPet.ExecuteNonQuery();
                return Redirect($"/Providers/Pet/Profile/{addPet.Id}");
            }

            return View("FormPage", addPet);
        }

        [HttpGet]
        public ActionResult Update(GetUpdatePet getUpdatePet)
        {
            this.ViewData["CancelUrl"] = $"/Providers/Pet/Profile/{getUpdatePet.Id}";


            var updatePet = getUpdatePet.ExecuteItem();
            return View("FormPage", updatePet);
        }

        [HttpPost]
        public ActionResult Update(UpdatePet updatePet)
        {
            this.ViewData["CancelUrl"] = $"/Providers/Pet/Profile/{updatePet.Id}";

            if (this.ModelState.IsValid)
            {
                int result = updatePet.ExecuteNonQuery();
                return Redirect($"/Providers/Pet/Profile/{updatePet.Id}");
            }

            return View("FormPage", updatePet);
        }
    }
}