using Paw.Services.Messages.Web.Schedules.Appointments;
using Paw.Services.Messages.Web.SimpleReservations;
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

        [HttpPost]
        public ActionResult Add(AddSimpleReservation addSimpleReservation)
        {
            return View("FormPage", addSimpleReservation);
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test2(PetService petService)
        {
            return View("FormPage", petService);
        }
    }
}