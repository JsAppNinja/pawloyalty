using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Paw.Services.Util;
using Paw.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Blocks.Controllers
{
    public class ScheduleController : AuthorizeController
    {
        public ActionResult Empty()
        {
            return new EmptyResult();
        }

        // GET: Blocks/Schedule
        public ActionResult Index()
        {
            DHXScheduler scheduler = new DHXScheduler(this);

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ActionResult Data()
        {
            return new SchedulerAjaxData(Block.GetList());
        }

        public ActionResult Save(Block block, FormCollection formData)
        {
            var action = new DataAction<Guid>(formData);

            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert: // your Insert logic
                        Block.Add(block);
                        break;
                    case DataActionTypes.Delete: // your Delete logic

                        Block.Delete(block.Id);
                        break;
                    default:// "update"   // your Update logic
                        Block.Delete(block.Id);
                        Block.Add(block);
                        break;
                }
                action.TargetId = block.Id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }
            return (new AjaxSaveResponse(action));
        }

        public ActionResult Boarding()
        {
            return new EmptyResult();
        }

    }
}