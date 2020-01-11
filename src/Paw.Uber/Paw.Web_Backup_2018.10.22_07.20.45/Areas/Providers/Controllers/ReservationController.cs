using Paw.Services.Messages;
using Paw.Services.Messages.Web.Reservations;
using Paw.Services.UI;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Providers.Controllers
{
    [ProviderActionFilter]
    public class ReservationController : Controller
    {
        public ActionResult Index(GetReservationListByProviderId getReservationListByProviderId)
        {
            return View(getReservationListByProviderId.ExecuteList());
        }

        public ActionResult View(GetReservation getReservation)
        {
            return View(getReservation.ExecuteItem());
        }

        [HttpGet]
        public ActionResult _AddReservation(Guid providerId, Guid? ownerId, Guid? petId)
        {
            // Step 1. 
            List<Guid> petList = new List<Guid>();
            if (petId != null)
            {
                petList.Add(petId.Value);
            }

            // Step 2. Get AddReservation
            AddReservation addReservation = new AddReservation() { ProviderId = providerId, OwnerId = ownerId, PetList = petList };
            
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = $"/providers/reservation/_AddReservation?PetId={petId}&OwnerId={ownerId}", FormTitle = "New Reservation", SubmitLabel = "Next", HttpMethod = "POST", OnSuccess = "app.onAjaxFormSuccess" };

            this.ViewData["SubmitLabel"] = "Next";

            return View("FormModal", addReservation);
        }

        [HttpPost]
        public ActionResult _AddReservation(AddReservation addReservation)
        {
            if (this.ModelState.IsValid)
            {
                addReservation.ExecuteNonQuery();
                return Json(new { Result = "Success", Url = "" });
            }

            Guid? petId = addReservation.PetList?.FirstOrDefault();

            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = $"/providers/reservation/_AddReservation?PetId={petId}&OwnerId={addReservation.OwnerId}", FormTitle = "New Reservation", SubmitLabel = "Next", HttpMethod = "POST", OnSuccess = "app.onAjaxFormSuccess" };

            this.ViewData["SubmitLabel"] = "Next";

            return View("FormModel", addReservation);
        }

        [HttpGet]
        public ActionResult _UpdateReservation(GetUpdateReservation getUpdateReservation)
        {
            UpdateReservation updateReservation = getUpdateReservation.ExecuteItem();
            
            this.ViewData["SubmitLabel"] = "Next";
            return View("FormPage", updateReservation);
        }

        [HttpPost]
        public ActionResult _UpdateReservation(UpdateReservation updateReservation)
        {
            if (this.ModelState.IsValid)
            {
                updateReservation.ExecuteNonQuery();
                return Redirect($"/providers/reservation/_UpdateReservation/{updateReservation.Id}");
            }

            this.ViewData["SubmitLabel"] = "Next";
            return View("FormPage", updateReservation);
        }
    }
}