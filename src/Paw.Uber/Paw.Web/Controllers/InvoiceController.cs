using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Web.InvoiceItems;
using Paw.Services.Messages.Web.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Controllers
{
    public class InvoiceController : AuthorizeController
    {
        // GET: Test
        public ActionResult Index(GetInvoiceListByProviderId getInvoiceListByProviderId)
        {
            return View(getInvoiceListByProviderId.ExecuteList());
        }

        [HttpGet]
        public ActionResult _AddInvoiceItem()
        {
            return View("FormPage", new AddInvoiceItem());
        }

        [HttpPost]
        public ActionResult _AddInvoiceItem(AddInvoiceItem addInvoiceItem)
        {
            if (this.ModelState.IsValid)
            {
                addInvoiceItem.ExecuteNonQuery();
                return Redirect(string.Format("/Invoice/{0}", addInvoiceItem.Id));
            }

            return View("FormPage", addInvoiceItem);
        }

        [HttpGet]
        public ActionResult _AddInvoice()
        {
            return View("FormPage", new AddInvoice());
        }

        [HttpPost]
        public ActionResult _AddInvoice(AddInvoice addInvoice)
        {
            if (this.ModelState.IsValid)
            {
                addInvoice.ExecuteNonQuery();
                return Redirect(string.Format("/Invoice/{0}", addInvoice.Id));
            }

            return View("FormPage", addInvoice);
        }
    }
}