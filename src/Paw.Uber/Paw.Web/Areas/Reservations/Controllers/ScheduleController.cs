using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Reservations;
using Paw.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Reservations.Controllers
{
    public class ScheduleController : AuthorizeController
    {
        // GET: Reservations/Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(Guid ownerId, Guid petId)
        {
            return View(new AddReservation() { OwnerId = ownerId, PetId = new List<Guid>() { petId }});
        }

        [HttpGet]
        public ActionResult _PetCard(Guid providerGroupId, Guid id)
        {
            Pet pet = new GetPet() { Id = id, ProviderGroupId = providerGroupId }.ExecuteItem();
            return View(pet);
        }

        public ActionResult _RoomSchedule()
        {
            return View();
        }

        #region Scheduler Models ...

        [HttpGet]
        public ActionResult _SelectDate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _SelectDateTime()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _SelectDateRange()
        {
            return View();
        }

        #endregion
    }
}