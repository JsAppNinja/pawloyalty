using Paw.Services.Attributes;
using Paw.Services.Messages.Web.PetReservations;
using Paw.Services.Messages.Web.ScheduleRules;
using Paw.Services.Messages.Web.Schedules.Appointments;
using Paw.Services.Messages.Web.SimpleReservations;
using Paw.Services.Messages.Web.SkuLines;
using Paw.Services.Messages.Web.Skus;
using Paw.Services.UI;
using Paw.Web.Controllers;
using Paw.Web.Filters;
using Paw.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Providers.Controllers
{
    [ProviderActionFilter]
    public class SimpleReservationController : AuthorizeController
    {
        // It's always one page, use agjax or javascript to add elements

        [HttpGet]
        public ActionResult Add()
        {
            var addSimpleReservation = new AddSimpleReservation();

            addSimpleReservation.AddPetServiceList.Add(new AddPetService());
            addSimpleReservation.AddPetServiceList.Add(new AddPetService());
            addSimpleReservation.AddPetServiceList.Add(new AddPetService());
            addSimpleReservation.AddPetServiceList.Add(new AddPetService());

            return View("FormPage", addSimpleReservation);
        }

        [HttpGet]
        public ActionResult AddAppointment()
        {
            this.AddAjaxPost("/Providers/SimpleReservation/AddAppointment", "Add Appointment", x => x.OnSuccess = "paw.onAjaxFormSuccess");
            return View("FormModal", new AddAppointmentReservation());
        }

        [HttpGet]
        public ActionResult Test3(AddAppointmentReservation addAppointmentReservation)
        {
            return View("FormPage", addAppointmentReservation);
        }

        [HttpGet]
        public ActionResult Test(Guid skuCategoryId, Guid? employeeId)
        {
            return View(new AddAppointmentReservation() { SkuCategoryId = skuCategoryId, EmployeeId = employeeId });
        }

        public ActionResult AddService(Guid petId, Guid skuId)
        {
            return View("ServiceAccordionCard", new AddPetService() { PetId = petId, SkuId = skuId });
        }

        public ActionResult StartTest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPetReservation(Guid skuCategoryId, Guid petId)
        {
            ReservationGroup reservationGroup = new ReservationGroup();
            reservationGroup.PetReservationList.Add(new PetReservation() { SkuCategoryId = skuCategoryId, PetId = petId });
            
            this.ViewData["__Pet"] = new Paw.Services.Messages.Web.Pets.GetPet() { Id = petId, ProviderGroupId = (Guid)this.ViewData["__ProviderGroupId"] }.ExecuteItem();

            return View(reservationGroup);
        }

        [HttpPost]
        public ActionResult AddPetReservation(ReservationGroup reservationGroup, Guid skuCategoryId, Guid? addPetId)
        {
            // Step 1. Sync ReservationGroupId
            reservationGroup.PetReservationList.ForEach(x => x.ReservationGroupId = reservationGroup.Id);

            // Step 2. Get First
            PetReservation petReservation = reservationGroup.PetReservationList.FirstOrDefault();

            if (petReservation == null)
            {
                throw new InvalidOperationException("Pet Reservation Required");
            }

            // Step 3. Add Pet
            if (addPetId != null)
            {
                reservationGroup.PetReservationList.Add(new PetReservation() { SkuCategoryId = skuCategoryId, PetId = addPetId.Value });                
            }

            // Step 4. Add viewData
            this.ViewData["__Pet"] = new Paw.Services.Messages.Web.Pets.GetPet() { Id = petReservation.PetId, ProviderGroupId = (Guid)this.ViewData["__ProviderGroupId"] }.ExecuteItem();

            return View(reservationGroup);
        }

        [HttpGet]
        public ActionResult _AddPetReservation(Guid skuCategoryId, Guid petId)
        {
            this.ViewData["__Show"] = false;
            return View("EditorTemplates/PetReservation", new PetReservation() { SkuCategoryId = skuCategoryId, PetId = petId });
        }

        [HttpGet]
        public ActionResult _SkuButtonList(Guid? skuId, string nameForModel, string idForModel)
        {
            // Step 1. Get skuList
            List<Paw.Services.Common.Sku> skuList = new List<Paw.Services.Common.Sku>();

            if (skuId != null)
            {
                skuList = new GetRelatedSkuList() { ProviderId = (Guid)this.ViewData["__ProviderId"], SkuId = skuId.Value, Type = 1 }.ExecuteList();                
            }
            this.ViewData["SkuList"] = skuList;

            // Step 2. Set Name and Id for model
            this.ViewData["NameForModel"] = nameForModel;
            this.ViewData["IdForModel"] = idForModel;
            
            return View("_SkuButtonList", new List<Guid>());
        }

        [HttpGet]
        public ActionResult UpdatePetReservation(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult UpdatePetReservation(ReservationGroup reservationGroup)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public ActionResult Test2(PetService petService)
        {
            petService.PetList = new List<Guid>() { new Guid("3eef11fa-1b7b-419c-b490-17bf47e9968b") };
            return View("FormPage", petService);
        }

        //[HttpGet]
        //public ActionResult AddPetReservation(Guid skuCategoryId, Guid petId)
        //{
        //    return View(new PetReservation() { SkuCategoryId = skuCategoryId, PetId = petId });
        //}

        [HttpGet]
        public ActionResult _EditSkuLine(Guid? skuCategoryId, Guid? parentSkuId)
        {
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = $"/Providers/simplereservation/_EditSkuLine?skuCategoryId={skuCategoryId}&parentSkuId={parentSkuId}", FormTitle = "Add Pet Service", SubmitLabel = "Add", HttpMethod = "POST", OnSuccess = "app.onAjaxFormSuccess" };
            return View("FormModal", new EditSkuLine() { SkuCategoryId = skuCategoryId, ParentSkuId = parentSkuId });
        }

        [HttpGet]
        public ActionResult PopConfig()
        {
            return View();
        }

        public ActionResult _AddPetService(Guid petId, Guid skuId)
        {
            this.ViewData["AjaxFormModel"] = new AjaxFormModel() { Action = $"/providers/simplereservation/popconfig", FormTitle = "Add Pet Service", SubmitLabel = "Add", HttpMethod = "POST", OnSuccess = "app.onAjaxFormSuccess" };
            return View("FormModal", new AddPetService() { PetId = petId, SkuId = skuId });
        }

        public ActionResult _GetStartTimeList(GetScheduleRuleListByDate getStartTimeList)
        {
            List<StartTime> dateTimeList = getStartTimeList.ExecuteList().GetStartTimeList();
            return Json(dateTimeList.Select(x => new { value = x, text = x.Time.ToString("hh:mm tt") }));
        }
    }
}