using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paw.Services.Util;

namespace Paw.Web.Controllers
{
    public class PeepController : Controller
    {
        // GET: Peep
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update(Guid? id)
        {
            Peep peep = this.Session[(id ?? Guid.Empty).ToString()] as Peep;

            if (peep == null)
            {
                peep = new Peep() { Name = "Unknown_1", ChildCollection = new List<Peep>() { new Peep() { Name = "Unknown_1_1", ChildCollection = new List<Peep>() { new Peep() { Name="Unknown_1_1_1", Peek = new Peek() { Name = "Peek" } } } } } };
            }

            return View(peep);
        }

        [HttpPost]
        public ActionResult Update(Peep peep, string addChild)
        {
            //
            peep.SetParentId();

            if (!string.IsNullOrWhiteSpace(addChild))
            {
                Guid parentId = new Guid(addChild);
                Peep parent = peep.Get(parentId);
                parent.ChildCollection.Add(new Peep() { Name = "New Child" });
            }
            else
            {
                // Save
                this.Session[peep.Id.ToString()] = peep;
            }

            return View(peep);
        }
    }
}