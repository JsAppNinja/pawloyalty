using Kendo.Mvc;
using Kendo.Mvc.UI;
using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Schedules;
using Paw.Services.UI;
using Paw.Web.Areas.Providers.Views.Shared;
using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Providers.Controllers
{
    [Nav("Scheduler")]
    [ProviderActionFilter]
    public class SchedulerController : AuthorizeController
    { 
        public ActionResult Appointment(GetEmployeeList getEmployeeList)
        {
            this.ViewData["employeeList"] = getEmployeeList.ExecuteList();
            return View(new List<SchedulerInfo>());
        }

        public ActionResult Resource()
        {
            return View();
        }



        public ActionResult _EmployeeFilter(GetEmployeeList getEmployeeList)
        {
            FilterModel filterModel = new FilterModel() { Id = "FilterEmployeeId", SelectAllCaption = "All Providers", SelectListItemList = new SelectList(getEmployeeList.ExecuteList(), "Id", "FullName").ToList() };

            return View("_Filter", filterModel);
        }

        public ActionResult Read(GetSchedulerInfoList getSchedulerEventList)
        {
            //if (request != null && request.Filters != null && request.Filters.Count > 0)
            //{
            //    CompositeFilterDescriptor compositeFilterDescriptor = request.Filters[0] as CompositeFilterDescriptor;
            //    if (compositeFilterDescriptor != null)
            //    {
            //        foreach (FilterDescriptor filterDescriptor in compositeFilterDescriptor.FilterDescriptors)
            //        {
            //            if (filterDescriptor.Member == "Start")
            //            {
            //                getSchedulerEventList.Start = (DateTime)filterDescriptor.Value;
            //            }
            //            else if (filterDescriptor.Member == "End")
            //            {
            //                getSchedulerEventList.End = (DateTime)filterDescriptor.Value;
            //            }
            //        }
            //    }
            //}

            //Guid? employeeId = null;
            //Guid providerId = new Guid("1543dd05-83d2-484d-9d59-16278995d4f1");
            //DateTime start = new DateTime(2018, 2, 10);
            //DateTime end = new DateTime(2018, 2, 17);

            //List<SchedulerInfo> schedulerEventList = new GetSchedulerInfoList() { EmployeeId = employeeId, Start = start, End = end, ProviderId = providerId }.ExecuteList(); ;

            List<SchedulerInfo> schedulerEventList = getSchedulerEventList.ExecuteList();
            foreach (SchedulerInfo schedulerInfo in schedulerEventList)
            {
                schedulerInfo.StartTimezone = null;
                schedulerInfo.EndTimezone = null;

                // schedulerInfo.Start = new DateTime(schedulerInfo.Start.Ticks, DateTimeKind.Utc);
                // schedulerInfo.End = new DateTime(schedulerInfo.End.Ticks, DateTimeKind.Utc);

                schedulerInfo.Start = schedulerInfo.Start;
                schedulerInfo.End = schedulerInfo.End;
            }

            return Json(schedulerEventList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy(List<SchedulerEvent> models)
        {
            foreach (var item in models)
            {
                int result = new DeleteSchedulerEvent() { Id = item.Id, ProviderId = (Guid)this.ViewData["__ProviderId"] }.ExecuteNonQuery();
            }

            return new EmptyResult();
        }

        public ActionResult Update(List<SchedulerEvent> models)
        {
            foreach (var item in models)
            {
                int result = new UpdateSchedulerEventStartEnd() { Id = item.Id, Start = item.Start, End = item.End, ProviderId = (Guid)this.ViewData["__ProviderId"] }.ExecuteNonQuery();
            }

            return new EmptyResult();
        }

        public ActionResult Create(AddSchedulerEvent addSchedulerEvent)
        {
            if (this.ModelState.IsValid)
            {
                addSchedulerEvent.ExecuteNonQuery();
                return Json(new { Result = "Success", Url = "" });
            }

            return View("", addSchedulerEvent);
        }

        #region AddSchedulerEvent ...

        [HttpGet]
        public ActionResult _AddSchedulerEventModal(Guid providerId, Guid? ownerId, Guid? petId, Guid? employeeId, Guid? skuCategoryId, DateTime? eventStart)
        {
            string action = $"/Providers/scheduler/_AddSchedulerEventModal?skuCategoryId={skuCategoryId}&employeeId={employeeId}&initStart={eventStart}";
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = action, FormTitle = "New Appointment", SubmitLabel = "Save", HttpMethod = "POST", OnSuccess = "paw.onAjaxFormSuccess" };
            
            AddSchedulerEventModal addSchedulerEventModal = new AddSchedulerEventModal() { ProviderId = providerId, OwnerId = ownerId, SkuCategoryId = skuCategoryId, EmployeeId = employeeId };
            
            // Set default duration
            if (eventStart != null)
            {
                addSchedulerEventModal.Start = eventStart.Value.AddHours(0);
                addSchedulerEventModal.End = eventStart.Value.AddHours(1);
            }

            return View("FormModal", addSchedulerEventModal);
        }

        [HttpPost]
        public ActionResult _AddSchedulerEventModal(AddSchedulerEventModal addSchedulerEvent)
        {
            if (this.ModelState.IsValid)
            {
                // string uri = $"/Providers/scheduler/{addSchedulerEvent.SkuCategoryId}/AddSchedulerEvent?ownerId={addSchedulerEvent.OwnerId}&PetId={addSchedulerEvent.PetId}&employeeId={addSchedulerEvent.EmployeeId}&start={addSchedulerEvent.Start}&end={addSchedulerEvent.End}";
                int result = addSchedulerEvent.ExecuteNonQuery();
                return Json(new { Result = "Success", Url = "" });
            }

            string action = $"/Providers/scheduler/{addSchedulerEvent.SkuCategoryId}/_AddSchedulerEventModal?ownerId={addSchedulerEvent.OwnerId}&PetId={addSchedulerEvent.PetId}&employeeId={addSchedulerEvent.EmployeeId}&start={addSchedulerEvent.Start}&end={addSchedulerEvent.End}";
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = action, FormTitle = "New Appointment", SubmitLabel = "Save", HttpMethod = "POST", OnSuccess = "paw.onAjaxFormSuccess" };

            return View("FormModal", addSchedulerEvent);
        }

        [HttpGet]
        public ActionResult AddSchedulerEvent(Guid providerId, Guid? ownerId, Guid? petId, Guid? employeeId, Guid? skuCategoryId, DateTime? start, DateTime? end)
        {
            string cancelUrl = $"/Providers/Scheduler/{skuCategoryId}/Appointment?employeeId={employeeId}";
            return View("FormPage", new AddSchedulerEvent() { ProviderId = providerId, OwnerId = ownerId, PetId = petId, SkuCategoryId = skuCategoryId, EmployeeId = employeeId, Start = start, End = end });
        }

        [HttpPost]
        public ActionResult AddSchedulerEvent(AddSchedulerEvent addSchedulerEvent)
        {
            string cancelUrl = $"/Providers/Scheduler/{addSchedulerEvent.SkuCategoryId}/Appointment/{addSchedulerEvent.SkuCategoryId}?employeeId={addSchedulerEvent.EmployeeId}";
            if (this.ModelState.IsValid)
            {
                addSchedulerEvent.ExecuteNonQuery();
                return Redirect(cancelUrl);
            }
            
            
            return View("FormPage", addSchedulerEvent);
        }

        #endregion

        #region UpdateSchedulerEvent ...

        [HttpGet]
        public ActionResult _UpdateSchedulerEventModal(GetUpdateSchedulerEventModal getUpdateSchedulerEventModal, Guid? ownerId, Guid? employeeId, Guid? skuCategoryId)
        {
            string action = $"/Providers/scheduler/_UpdateSchedulerEventModal/{getUpdateSchedulerEventModal.Id}?skuCategoryId={skuCategoryId}&employeeId={employeeId}";
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = action, FormTitle = "Update Appointment", SubmitLabel = "Save", HttpMethod = "POST", OnSuccess = "paw.onAjaxFormSuccess" };

            var updateSchedulerEventModal = getUpdateSchedulerEventModal.ExecuteItem();

            return View("FormModal", updateSchedulerEventModal);
        }

        [HttpPost]
        public ActionResult _UpdateSchedulerEventModal(UpdateSchedulerEventModal updateSchedulerEventModal)
        {
            if (this.ModelState.IsValid)
            {
                int result = updateSchedulerEventModal.ExecuteNonQuery();
                return Json(new { Result = "Success", Url = "" });
            }

            string action = $"/Providers/scheduler/{updateSchedulerEventModal.SkuCategoryId}/_UpdateSchedulerEventModal?ownerId={updateSchedulerEventModal.OwnerId}&PetId={updateSchedulerEventModal.PetId}&employeeId={updateSchedulerEventModal.EmployeeId}&start={updateSchedulerEventModal.Start}&end={updateSchedulerEventModal.End}";
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = action, FormTitle = "New Appointment", SubmitLabel = "Save", HttpMethod = "POST", OnSuccess = "paw.onAjaxFormSuccess" };

            return View("FormModal", updateSchedulerEventModal);
        }


        [HttpGet]
        public ActionResult UpdateSchedulerEvent(GetUpdateSchedulerEvent getUpdateSchedulerEvent)
        {
            UpdateSchedulerEvent updateSchedulerEvent = getUpdateSchedulerEvent.ExecuteItem();
            string calendarEventUrl = $"/Providers/Scheduler/{updateSchedulerEvent.SkuCategoryId}/Appointment/{updateSchedulerEvent.SkuCategoryId}?employeeId={updateSchedulerEvent.EmployeeId}";
            this.ViewData["CancelUrl"] = calendarEventUrl;
            return View("FormPage", updateSchedulerEvent);
        }

        [HttpPost]
        public ActionResult UpdateSchedulerEvent(UpdateSchedulerEvent updateSchedulerEvent)
        {
            string calendarEventUrl = $"/Providers/Scheduler/{updateSchedulerEvent.SkuCategoryId}/Appointment/{updateSchedulerEvent.SkuCategoryId}?employeeId={updateSchedulerEvent.EmployeeId}";
            if (this.ModelState.IsValid)
            {
                updateSchedulerEvent.ExecuteNonQuery();
                return Redirect(calendarEventUrl);
            }

            this.ViewData["CancelUrl"] = calendarEventUrl;
            return View("FormPage", updateSchedulerEvent);
        }

        #endregion

        #region Block ...

        public ActionResult Block(GetSchedulerInfoList getSchedulerEventList)
        {
            // Step 1. Get the list
            List<SchedulerInfo> schedulerEventList = getSchedulerEventList.ExecuteList();
            foreach (SchedulerInfo schedulerInfo in schedulerEventList)
            {
                schedulerInfo.StartTimezone = null;
                schedulerInfo.EndTimezone = null;

                schedulerInfo.Start = schedulerInfo.Start;
                schedulerInfo.End = schedulerInfo.End;
            }

            return View(schedulerEventList);
        }
    }

        #endregion

    
}