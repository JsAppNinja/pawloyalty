using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Paw.Services.Test.Bootstrap
{
    [TestClass]
    public class BootstrapSchedule
    {
        [TestMethod]
        public void ScheduleSetup()
        {
            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.Database.Log = Log;

                context.SchedulerTypeSet.AddOrUpdate(
                    new SchedulerType() { Id = SchedulerType.Appointment, Action = "Appointment", Description="Appointment", Name="Appointment", Icon= "icon-clock" },
                    new SchedulerType() { Id = SchedulerType.Block, Action = "Block", Description = "Block", Name = "Block", Icon = "icon-clock" },
                    new SchedulerType() { Id = SchedulerType.Resource, Action = "Resource", Description = "Resource", Name = "Resource", Icon = "icon-calendar" }
                    );

                context.ScheduerEventTypeSet.AddOrUpdate(
                    new SchedulerEventType() { Id = SchedulerEventType.AppointmentId, Name = "Appointment" },
                    new SchedulerEventType() { Id = SchedulerEventType.BlockId, Name = "Block" },
                    new SchedulerEventType() { Id = SchedulerEventType.BoardingId, Name= "Boarding" });

                context.SaveChanges();
            }
        }

        private void Log(string message)
        {
            Trace.TraceInformation(message);
        }
    }
}
