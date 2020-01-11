using Paw.Services.Common;
using Paw.Services.Messages.Web.DynamicControls;
using Paw.Services.Messages.Web.Pets;
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
    public class UIControlController : AuthorizeController
    {

        public ActionResult PrimarySkuId(PrimarySkuIdDynamicControl primarySkuIdDynamicControl)
        {
            return View("~/Views/Shared/DynamicControl.cshtml", primarySkuIdDynamicControl);
        }

        public ActionResult PetRadioButtonList(Guid providerGroupId, Guid providerId, Guid? ownerId)
        {
            List<Pet> petList = new List<Pet>();

            if (ownerId != null)
            {
               petList = new GetPetListByOwnerId() { OwnerId = ownerId.Value, ProviderGroupId = providerGroupId }.ExecuteList();
            }

            return View(petList);
        }
    }
}