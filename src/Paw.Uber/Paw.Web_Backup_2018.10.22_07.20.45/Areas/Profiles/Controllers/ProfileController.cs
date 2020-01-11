using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Profiles.Controllers
{
    [ProviderActionFilter]
    public class ProfileController : Controller
    {
        // GET: Profiles/Profile
        public ActionResult Index(Guid profileId)
        {
            Pet pet = new GetPet() { Id = profileId }.ExecuteItem();
            return View(pet);
        }
    }
}