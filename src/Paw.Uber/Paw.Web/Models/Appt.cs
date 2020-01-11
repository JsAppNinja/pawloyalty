using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paw.Web.Models
{
    public class Appt : ISchedulerEvent
    {
        public int MeetingID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }
        private DateTime start;

        public string StartTimezone { get; set; }


        [Required]
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }
        private DateTime end;

        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public string Timezone { get; set; }
        public int? RoomID { get; set; }
        public IEnumerable<int> Attendees { get; set; }


        public static List<Appt> Init()
        {
            List = new List<Appt>() {
                new Appt(){ Title ="Tester1", Start = new DateTime(2017,12,18, 12, 30, 00), End = new DateTime(2017,12,18, 13, 30, 00), MeetingID = 1 },
                new Appt(){ Title ="Tester2", Start = new DateTime(2017,12,18, 14, 30, 00), End = new DateTime(2017,12,18, 15, 30, 00), MeetingID = 2 },
                new Appt(){ Title ="Tester3", Start = new DateTime(2017,12,18, 17, 30, 00), End = new DateTime(2017,12,18, 18, 30, 00), MeetingID = 3 }
            };
            return List;
            
        }
        public static List<Appt> List
        {
            get { return _List; }
            set { _List = value; }
        }
        private static List<Appt> _List = new List<Appt>();

        public static void Insert(Appt appt, object viewState)
        {
            List.Add(appt);
        }

        public static void Update(Appt appt, object viewState)
        {
            Appt target = List.Find(x => x.MeetingID == appt.MeetingID);

            if (target == null) return;

            target.Title = appt.Title;
            target.Start = appt.Start;
            target.End = appt.End;
        }

        public static void Delete(Appt appt, object viewState)
        {
            Appt target = List.Find(x => x.MeetingID == appt.MeetingID);

            if (target == null) return;

            List.Remove(target);
        }
    }
}