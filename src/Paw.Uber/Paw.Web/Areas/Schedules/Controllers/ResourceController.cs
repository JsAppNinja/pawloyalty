using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Newtonsoft.Json;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Resouces;
using Paw.Web.Controllers;
using Paw.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Schedules.Controllers
{
    [ProviderActionFilter]
    public class ResourceController : AuthorizeController
    {
        // GET: Schedules/Resource
        public ActionResult Index(GetResourceList getResourceList)
        {
            var resourceList = getResourceList.ExecuteList();
            return View(ConfigureScheduler(resourceList));
        }

        private DHXScheduler ConfigureScheduler(List<Resource> resourceList)
        {
            var scheduler = new DHXScheduler();

            ViewBag.RoomTypes = "[{ Id:1, Title: 'Room Type 1'}]"; // JsonConvert.SerializeObject(new Repository<RoomType>().ReadAll());

            ViewBag.RoomStatuses = "[{ Id:1, Title: 'Room Status 1'}]"; // JsonConvert.SerializeObject(new Repository<RoomStatus>().ReadAll());
            ViewBag.BookingStatuses = "[{ Id:1, Title: 'Booking Status 1'}]"; // JsonConvert.SerializeObject(new Repository<BookingStatus>().ReadAll());

            scheduler.Extensions.Add(SchedulerExtensions.Extension.Limit);
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Collision);
            scheduler.Config.collision_limit = 1;

            var rooms = resourceList.Select(
                r => new
                {
                    key = r.Id,
                    room_number = r.Name,
                    label = r.Name,
                    type = 1,
                    status = 1
                }).OrderBy(r => r.room_number).ToList();

            scheduler.Skin = DHXScheduler.Skins.Flat;

            var timeLine = ConfigureTimelineView(rooms);
            scheduler.Views.Clear();
            scheduler.Views.Add(timeLine);
            scheduler.InitialView = timeLine.Name;
            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);
            scheduler.Config.show_loading = true;
            scheduler.LoadData = true;
            scheduler.DataAction = Url.Action("Bookings", "Resource");

            scheduler.EnableDataprocessor = true;
            scheduler.SaveAction = Url.Action("Save", "Resource");

            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);

            ConfigureLightBox(scheduler, rooms);

            MarkWeekends(scheduler);

            ViewBag.DataAction = scheduler.DataAction;
            scheduler.BeforeInit.Add("pre_init();");
            //scheduler.AfterInit.Add("post_init();");

            return scheduler;
        }

        private void MarkWeekends(DHXScheduler scheduler)
        {
            string cssClass = "timeline_weekend";
            scheduler.TimeSpans.Add(new DHXMarkTime { Day = DayOfWeek.Saturday, CssClass = cssClass });
            scheduler.TimeSpans.Add(new DHXMarkTime { Day = DayOfWeek.Sunday, CssClass = cssClass });
        }

        private TimelineView ConfigureTimelineView(IEnumerable<object> rooms)
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

            timeline.FullEventDy = true;

            timeline.ServerList = "Rooms";

            timeline.AddOptions(rooms);

            return timeline;
        }

        private void ConfigureLightBox(DHXScheduler scheduler, IEnumerable<object> rooms)
        {
            //var name = new LightboxText("text", "Name");
            //name.Height = 24;
            //scheduler.Lightbox.Add(name);

            //var roomsSelect = new LightboxSelect("room_number", "Room");
            //roomsSelect.AddOptions(rooms);
            //scheduler.Lightbox.Add(roomsSelect);

            //var status = new LightboxRadio("status", "Status");
            //var statuses = new Repository<BookingStatus>().ReadAll().Select(s => new { key = s.Id, label = s.Title });
            //status.AddOptions(statuses);
            //scheduler.Lightbox.Add(status);
            //scheduler.InitialValues.Add("status", statuses.First().key);

            //var isPaid = new LightboxCheckbox("is_paid", "Paid");
            //scheduler.Lightbox.Add(isPaid);
            //scheduler.InitialValues.Add("is_paid", false);

            //var date = new LightboxMiniCalendar("time", "Time");
            //scheduler.Lightbox.Add(date);


            //scheduler.InitialValues.Add("text", String.Empty);
        }

        public ActionResult Bookings()
        {
            var dateFrom = DateTime.ParseExact(this.Request.QueryString["from"], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var dateTo = DateTime.ParseExact(this.Request.QueryString["to"], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            var bookings = new List<Booking>();
            return new SchedulerAjaxData(bookings);
        }

        public class Booking
        {
            [DHXJson(Alias = "id")]
            public int id { get; set; }

            [DHXJson(Alias = "text")]
            public string text { get; set; }

            [DHXJson(Alias = "is_paid")]
            public bool is_paid { get; set; }

            [DHXJson(Alias = "start_date")]
            public DateTime start_date { get; set; }

            [DHXJson(Alias = "end_date")]
            public DateTime end_date { get; set; }

            [DHXJson(Alias = "room_number")]
            public int? room_number { get; set; }

            //[DHXJson(Ignore = true)]
            //public virtual Room Room { get; set; }

            [DHXJson(Alias = "status")]
            public int? status { get; set; }

            //[DHXJson(Ignore = true)]
            //public BookingStatus BookingStatus { get; set; }
        }
    }
}