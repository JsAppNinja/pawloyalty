using Paw.Services.Messages.Web.Res;
using Paw.Services.UI;
using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Reservations.Controllers
{
    [ProviderActionFilter]
    public class BlockController : AuthorizeController
    {
        // GET: Reservations/Reservation
        public ActionResult Index()
        {
            return View();
        }



        //[HttpGet]
        //public ActionResult AddReservation(Guid skuCategoryId, Guid ownerId, Guid petId, Guid providerId)
        //{
        //    this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = $"/reservations/block/AddReservation?PetId={petId}&OwnerId={ownerId}&skuCategoryId", FormTitle = "New Reservation", SubmitLabel = "Next", HttpMethod = "POST", OnSuccess = "app.onAjaxFormSuccess" };

        //    return View("FormModal", new AddReservation() { ProviderId = providerId, SkuCategoryId = skuCategoryId, OwnerId = ownerId, PetId = petId });
        //}

        //[HttpPost]
        //public ActionResult AddReservation(AddReservation addReservation)
        //{
        //    if (this.ModelState.IsValid)
        //    { 
        //        addReservation.ExecuteNonQuery();
        //        return RedirectToAction("ChooseExtras", new { Id = addReservation.Id  });
        //    }

        //    this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = $"/reservations/block/AddReservation?PetId={addReservation.PetId}&OwnerId={addReservation.OwnerId}&SkuCategoryId={addReservation.SkuCategoryId}", FormTitle = "New Reservation", SubmitLabel = "Next", HttpMethod = "POST", OnSuccess = "app.onAjaxFormSuccess" };

        //    return View("FormModal", addReservation);
        //}

        [HttpPost]
        public JsonResult AddReservation(AddReservation addReservation)
        {
            if (this.ModelState.IsValid)
            {
                addReservation.ExecuteNonQuery();
                return new JsonResult() { Data = new { Result = "Success" } };
            }

            return new JsonResult() { Data = new { Result = "Failed" } };
        }

        [HttpGet]
        public ActionResult GetChooseExtras(GetChooseExtras getChooseExtras)
        {
            return View(getChooseExtras.ExecuteItem());
        }

        [HttpPost]
        public ActionResult ChooseExtras(ChooseExtras chooseExtras)
        {
            if (this.ModelState.IsValid)
            {
                chooseExtras.ExecuteNonQuery();
                return Redirect("");
            }

            return View(chooseExtras);
        }

        [HttpGet]
        public ActionResult GetScheduleSkuLine(GetScheduleSkuLine getScheduleSkuLine)
        {


            return View();
        }

        [HttpPost]
        public ActionResult ScheduleSkuLine(ScheduleSkuLine scheduleSkuLine)
        {
            if (this.ModelState.IsValid)
            {
                scheduleSkuLine.ExecuteNonQuery();
                return Redirect("");
            }

            return View(scheduleSkuLine);
        }
    }
}