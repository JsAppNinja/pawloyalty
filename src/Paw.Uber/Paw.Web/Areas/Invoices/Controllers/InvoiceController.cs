using Paw.Services.Messages;
using Paw.Services.Messages.Web.InvoiceItems;
using Paw.Services.Messages.Web.Invoices;
using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Invoices.Controllers
{
    [ProviderActionFilter]
    public class InvoiceController : AuthorizeController
    {
        // GET: Invoices/Invoice
        public ActionResult Index(GetInvoiceListByProviderId getInvoiceListByProviderId)
        {
            return View(getInvoiceListByProviderId.ExecuteList());
        }


        [HttpGet]
        public ActionResult View(GetInvoice getInvoice)
        {
            return View(getInvoice.ExecuteItem());
        }

        
        [HttpGet]
        public ActionResult _AddInvoiceItem(Guid providerId)
        {
            return View("FormPage", new AddInvoiceItem());
        }

        [HttpPost]
        public ActionResult _AddInvoiceItem(Guid providerId, AddInvoiceItem addInvoiceItem)
        {
            if (this.ModelState.IsValid)
            {
                addInvoiceItem.ExecuteNonQuery();
                return Redirect($"/Providers/Invoices/Invoice/View/{addInvoiceItem.InvoiceId}");
            }

            return View("FormPage", addInvoiceItem);
        }

        [HttpGet]
        public ActionResult _AddServiceSkuInvoiceItem(Guid providerId, Guid ownerId)
        {
            return View("FormPage", new AddServiceSkuInvoiceItem() { OwnerId = ownerId });
        }

        [HttpPost]
        public ActionResult _AddServiceSkuInvoiceItem(Guid providerId, AddServiceSkuInvoiceItem addServiceSkuInvoiceItem)
        {
            if (this.ModelState.IsValid)
            {
                addServiceSkuInvoiceItem.ExecuteNonQuery();
                return Redirect($"/Providers/Invoices/Invoice/View/{addServiceSkuInvoiceItem.InvoiceId}");
            }

            return View("FormPage", addServiceSkuInvoiceItem);
        }

        [HttpGet]
        public ActionResult _AddInvoice(Guid providerId)
        {
            return View("FormPage", new AddInvoice());
        }

        [HttpPost]
        public ActionResult _AddInvoice(AddInvoice addInvoice)
        {
            if (this.ModelState.IsValid)
            {
                addInvoice.ExecuteNonQuery();
                return Redirect($"/Providers/Invoices/Invoice/View/{addInvoice.Id}");
            }

            return View("FormPage", addInvoice);
        }

        [HttpPost]
        public ActionResult _DeleteInvoiceItem(DeleteInvoiceItem deleteInvoiceItem)
        {
            deleteInvoiceItem.ExecuteNonQuery();

            return Json(new { Result = "Success", Url = "" });
        }

        [HttpPost]
        public ActionResult _DeleteInvoice(DeleteInvoice deleteInvoice)
        {
            deleteInvoice.ExecuteNonQuery();

            return Json(new { Result = "Success", Url = "" });
        }

        [HttpGet]
        public ActionResult _UpdateInvoice(GetUpdateInvoice getUpdateInvoice)
        {
            UpdateInvoice updateInvoice = getUpdateInvoice.ExecuteItem();
            return View("FormPage", updateInvoice);
        }

        [HttpPost]
        public ActionResult _UpdateInvoice(UpdateInvoice updateInvoice)
        {
            if (this.ModelState.IsValid)
            {
                updateInvoice.ExecuteNonQuery();
                return Redirect($"/Providers/Invoices/Invoice/View/{updateInvoice.Id}");
            }

            return View("FormPage", updateInvoice);
        }





        public ActionResult OwnerMap(string values)
        {
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}