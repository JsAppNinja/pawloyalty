using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Newtonsoft.Json;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Resouces;
using Paw.Services.Messages.Web.Schedules;
using Paw.Services.Messages.Web.Schedules.Blocks;
using Paw.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Providers.Controllers
{
    public class BlockController : Controller
    {
        // GET: Calendar
        public ActionResult Index(Guid providerId)
        {
            return View(ConfigureScheduler(providerId));
        }

        private TimelineView ConfigureTimelineView(IEnumerable<object> resources)
        {
            var timeline = new TimelineView("Timeline", "room_number");
            timeline.RenderMode = TimelineView.RenderModes.Bar;
            timeline.X_Unit = TimelineView.XScaleUnits.Day;
            timeline.X_Date = "%d";
            timeline.X_Size = 45;
            timeline.AddSecondScale(TimelineView.XScaleUnits.Month, "%F %Y");
            timeline.Dy = 51;
            timeline.SectionAutoheight = false;
            timeline.RoundPosition = true;

            timeline.FullEventDy = false;

            timeline.ServerList = "Rooms";

            timeline.AddOptions(resources);

            return timeline;
        }

        private void MarkWeekends(DHXScheduler scheduler)
        {
            string cssClass = "timeline_weekend";
            scheduler.TimeSpans.Add(new DHXMarkTime { Day = DayOfWeek.Saturday, CssClass = cssClass });
            scheduler.TimeSpans.Add(new DHXMarkTime { Day = DayOfWeek.Sunday, CssClass = cssClass });
        }

        private void ConfigureLightBox(DHXScheduler scheduler, List<ResourceViewModel> rooms)
        {
            var name = new LightboxText("text", "Name");
            name.Height = 24;
            scheduler.Lightbox.Add(name);

            var roomsSelect = new LightboxSelect("room_number", "Room");
            roomsSelect.AddOptions(rooms.ToList());
            scheduler.Lightbox.Add(roomsSelect);

            //var status = new LightboxRadio("status", "Status");
            //var statuses = new List<object>(); // new Repository<BookingStatus>().ReadAll().Select(s => new { key = s.Id, label = s.Title });
            //status.AddOptions(statuses);
            //scheduler.Lightbox.Add(status);
            //scheduler.InitialValues.Add("status", statuses.First().key);

            var isPaid = new LightboxCheckbox("is_paid", "Paid");
            scheduler.Lightbox.Add(isPaid);
            scheduler.InitialValues.Add("is_paid", false);

            var date = new LightboxMiniCalendar("time", "Time");
            scheduler.Lightbox.Add(date);


            scheduler.InitialValues.Add("text", String.Empty);
        }

        private DHXScheduler ConfigureScheduler(Guid providerId)
        {
            DHXScheduler scheduler = new DHXScheduler();
            
            ViewBag.RoomTypes = JsonConvert.SerializeObject(new List<object>());
            ViewBag.RoomStatuses = JsonConvert.SerializeObject(new List<object>());
            ViewBag.BookingStatuses = JsonConvert.SerializeObject(new List<object>());

            scheduler.Extensions.Add(SchedulerExtensions.Extension.Limit);
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Collision);
            scheduler.Config.collision_limit = 1;

            var rooms = new GetResourceList() { ProviderId = providerId }.ExecuteList().AsResourceViewModel();

            scheduler.Skin = DHXScheduler.Skins.Flat;

            var timeLine = ConfigureTimelineView(rooms);
            scheduler.Views.Clear();
            scheduler.Views.Add(timeLine);
            scheduler.InitialView = timeLine.Name;
            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);
            scheduler.Config.show_loading = true;
            scheduler.LoadData = true;
            scheduler.DataAction = Url.Action("Bookings", "DataAccess");

            scheduler.EnableDataprocessor = true;
            scheduler.SaveAction = Url.Action("Save", "DataAccess");

            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);

            this.ConfigureLightBox(scheduler, rooms);

            MarkWeekends(scheduler);

            ViewBag.DataAction = scheduler.DataAction;
            //scheduler.BeforeInit.Add("pre_init();");
            scheduler.AfterInit.Add("post_init();");

            return scheduler;
        }

        public ActionResult FilteringSelect()
        {
            return PartialView(new List<object>());
        }

        public ActionResult GetSchedulerEvents(GetBlockEventList getBlockEventList)
        {
            var result = getBlockEventList.ExecuteList().AsBlockViewModel();
            return new SchedulerAjaxData(result);
        }
    }
}