using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Owners;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Res;
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

namespace Paw.Web.Areas.Reservations.Controllers
{
    [ProviderActionFilter]
    public class LookupController : AuthorizeController
    {

        public JsonResult GetPetList(GetPetListByOwnerId getPetListByOwnerId)
        {
            var result = getPetListByOwnerId.ExecuteList().Select(x => new { Id = x.Id, Name = x.Name, OwnerId= x.OwnerId, Weght = x.Weight, DOB = x.DOB, Breed = x.Breed.Name });
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetPrimarySkuList(GetPrimarySkuList getPrimarySku)
        {
            var result = getPrimarySku.ExecuteList().Select(x => new { Id = x.Id, Name = x.Name, Amount = x.Amount, NameAndPrice = x.NameAndAmount });
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRelatedSkuList(GetRelatedSkuList getRelatedSkuList) // Note: this method could leak all skus
        {
            getRelatedSkuList.Type = 1; // Extras
            var result = getRelatedSkuList.ExecuteList().Select(x => new { Id = x.Id, Name = x.Name, Amount = x.Amount, NameAndAmount = x.NameAndAmount });
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeList(GetActiveEmployeeInfoList getActiveEmployeeInfoList)
        {
            var result = getActiveEmployeeInfoList.ExecuteList().Select(x => new { Id = x.Id, FullName = x.FullName });
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetStartTimeList(GetScheduleRuleListByDate getScheduleRuleListByDate)
        {
            var result = getScheduleRuleListByDate.ExecuteList().GetStartTimeList(getScheduleRuleListByDate.Date);
            return this.Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOwnerInfo(GetOwner getOwner)
        {
            var result = getOwner.ExecuteItem();
            return this.Json(new { result = new { Id = result.Id, FullName = result.FullName } }, JsonRequestBehavior.AllowGet);
        }
        
    }
}