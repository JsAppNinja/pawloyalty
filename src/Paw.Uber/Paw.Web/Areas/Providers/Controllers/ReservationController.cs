using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Reservations;
using Paw.Services.Messages.Web.ScheduleRules;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.UI;
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
    public class ReservationController : AuthorizeController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Block()
        {
            return View();
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
            AddReservation addReservation = new AddReservation() { ProviderId = providerId, OwnerId = ownerId };
            
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

            Guid? petId = null; //addReservation.PetList?.FirstOrDefault();

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

        #region Lookups ...

        [HttpPost]
        public JsonResult _GetPetListByOwner(GetPetListByOwnerId getPetListByOwner)
        {
            var petList = getPetListByOwner.ExecuteList();
            return this.Json(new { result = petList });
        }

        [HttpPost]
        public JsonResult _GetPrimarySkuList(GetPrimarySkuList getPrimarySkuList)
        {
            var skuList = getPrimarySkuList.ExecuteList();
            return this.Json(new { result = skuList });
        }

        [HttpPost]
        public JsonResult _GetRelatedSkuList(GetRelatedSkuList getRelatedSkuList)
        {
            List<Sku> skuList = getRelatedSkuList.ExecuteList();
            return this.Json(new { result = skuList });
        }

        [HttpPost]
        public JsonResult _GetStartTimeList(GetScheduleRuleListByDate getStartTimeList)
        {
            List<StartTime> scheduleRuleList = getStartTimeList.ExecuteList().GetStartTimeList(getStartTimeList.Date);
            return this.Json(scheduleRuleList);
        }

        [HttpPost]
        public JsonResult _GetEmployeeList(GetEmployeeInfoList getEmployeeInfoList)
        {
            List<EmployeeInfo> employeeInfoList = getEmployeeInfoList.ExecuteList();
            return this.Json(employeeInfoList);
        }

        [HttpPost]
        public JsonResult _GetFilteredScheduleRuleList(GetFilteredScheduleRuleList getFilteredScheduleRuleList)
        {
            List<ScheduleRule> scheduleRuleList = getFilteredScheduleRuleList.ExecuteList();
            return this.Json(scheduleRuleList);
        }

        #endregion
    }
}