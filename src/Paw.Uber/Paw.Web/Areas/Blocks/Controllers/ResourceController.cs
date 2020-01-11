using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Newtonsoft.Json;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Resouces;
using Paw.Services.Messages.Web.Schedules.Blocks;
using Paw.Web.Controllers;
using Paw.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Blocks.Controllers
{
    public class ResourceController : AuthorizeController
    {
        public ActionResult Index()
        {
            return View(ConfigureScheduler(new Guid("1543DD05-83D2-484D-9D59-16278995D4F1")));
        }

        private TimelineView ConfigureTimelineView(IEnumerable<object> resources)
        {
            var timeline = new TimelineView("Timeline", "room_id");
            timeline.RenderMode = TimelineView.RenderModes.Bar;
            timeline.X_Unit = TimelineView.XScaleUnits.Day;
            timeline.X_Date = "%d";
            timeline.X_Size = 40;
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

            ViewBag.RoomTypes = JsonConvert.SerializeObject(this.GetRoomTypes());
            //ViewBag.RoomStatuses = JsonConvert.SerializeObject(new List<object>());
            //ViewBag.BookingStatuses = JsonConvert.SerializeObject(new List<object>());

            scheduler.Extensions.Add(SchedulerExtensions.Extension.Limit);
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Collision);
            scheduler.Config.collision_limit = 1;

            // var rooms = new GetResourceList() { ProviderId = providerId }.ExecuteList().AsResourceViewModel();

            var rooms = new List<object>() {
                new { key="1", label="Room 1", type=1 },
                new { key="2", label="Room 2", type=1 },
                new { key="3", label="Room 3", type=1 }
            };

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

            //this.ConfigureLightBox(scheduler, rooms);

            MarkWeekends(scheduler);

            ViewBag.DataAction = scheduler.DataAction;
            //scheduler.BeforeInit.Add("pre_init();");
            scheduler.AfterInit.Add("post_init();");

            return scheduler;
        }

        public ActionResult FilteringSelect()
        {
            List<RoomType> roomTypeList = new List<RoomType>() {
                new RoomType(){ Id=1, Title="Small" },
                new RoomType(){ Id=2, Title="Medium" },
                new RoomType(){ Id=3, Title="Large" },
                new RoomType(){ Id=3, Title="XLarge" }
            };

            return PartialView(this.GetRoomTypes());
        }

        public ActionResult GetSchedulerEvents(GetBlockEventList getBlockEventList)
        {
            var result = getBlockEventList.ExecuteList().AsBlockViewModel();
            return new SchedulerAjaxData(result);
        }

        #region ...

        public List<RoomType> GetRoomTypes()
        {
            List<RoomType> roomTypeList = new List<RoomType>() {
                new RoomType(){ Id=1, Title="Small" },
                new RoomType(){ Id=2, Title="Medium" },
                new RoomType(){ Id=3, Title="Large" },
                new RoomType(){ Id=3, Title="XLarge" }
            };

            return roomTypeList;
        }

        #endregion
    }
}