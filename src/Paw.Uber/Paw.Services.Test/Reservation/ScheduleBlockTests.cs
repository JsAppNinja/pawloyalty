using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using Paw.Services.Messages.Util.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Paw.Services.Test.Reservation
{
    [TestClass]
    public class ScheduleBlockTest
    {
        [TestMethod]
        public void SetupScheduleBlockTest()
        {
            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.Database.Log = Log;

                context.ScheduleRuleSet.AddOrUpdate(
                    new ScheduleRule() { Id = Guid.NewGuid(), Capacity = 2, Duration = 120, EmployeeId = new Guid("77E15A40-A1C2-403F-9481-335B09DCA1EB"),  Action = "Appointment", Description = "Appointment", Name = "Appointment", Icon = "icon-clock" },
                    new ScheduleRule() { Id = Guid.NewGuid(), Capacity = 2, Duration = 120, EmployeeId = new Guid("77E15A40-A1C2-403F-9481-335B09DCA1EB"), Action = "Block", Description = "Block", Name = "Block", Icon = "icon-clock" },
                    new ScheduleRule() { Id = Guid.NewGuid(), Capacity = 2, Duration = 120, EmployeeId = new Guid("77E15A40-A1C2-403F-9481-335B09DCA1EB"), Action = "Resource", Description = "Resource", Name = "Resource", Icon = "icon-calendar" }
                    );
                

                context.SaveChanges();
            }


        }
        private void Log(string message)
        {
            Trace.TraceInformation(message);
        }

    }
}
