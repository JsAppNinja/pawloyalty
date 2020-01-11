using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using System.Linq;

namespace Paw.Services.Test.ScheduleBlocks
{
    [TestClass]
    public class ScheduleBlockExpandTests
    {
        //[TestMethod]
        //public void ScheduleBlockExpandTest()
        //{
        //    // Step 1. Create schedule Blocks
        //    ScheduleRule scheduleRuleAM = new ScheduleRule() {
        //        RuleStartDate = new DateTime(2018, 1, 1),
        //        RuleEndDate = new DateTime(2018, 6, 1),
        //        Monday = true,
        //        Tuesday = true,
        //        Thursday = true,
        //        Friday = true,
        //        StartHour = 10,
        //        StartMinute = 0,
        //        EndHour = 12,
        //        EndMinute = 0
        //    };

        //    ScheduleRule scheduleRulePM = new ScheduleRule()
        //    {
        //        RuleStartDate = new DateTime(2018, 1, 1),
        //        RuleEndDate = null,
        //        Monday = true,
        //        Tuesday = true,
        //        Thursday = true,
        //        Friday = true,
        //        StartHour = 13,
        //        StartMinute = 0,
        //        EndHour = 15,
        //        EndMinute = 0
        //    };
            
        //    var x = scheduleRuleAM.GetScheduleBlockList(new DateTime(2018, 2, 4), new DateTime(2018, 2, 17), null);

        //    var list = x.Values.ToList();

        //    var y = scheduleRulePM.GetScheduleBlockList(new DateTime(2018, 2, 4), new DateTime(2018, 11, 17), null);
        //}

        //[TestMethod]
        //public void ScheduleRuleSmokeTest()
        //{
        //    // Step 1. Setup the schedule blocks
        //    ScheduleRule scheduleRuleAM = new ScheduleRule()
        //    {
        //        RuleStartDate = new DateTime(2018, 1, 1),
        //        RuleEndDate = new DateTime(2018, 6, 1),
        //        Monday = true,
        //        Tuesday = true,
        //        Thursday = true,
        //        Friday = true,
        //        StartHour = 10,
        //        StartMinute = 0,
        //        EndHour = 12,
        //        EndMinute = 0
        //    };

        //    ScheduleRule scheduleRulePM = new ScheduleRule()
        //    {
        //        RuleStartDate = new DateTime(2018, 1, 1),
        //        RuleEndDate = null,
        //        Monday = true,
        //        Tuesday = true,
        //        Thursday = true,
        //        Friday = true
        //    };

        //    List<ScheduleRule> scheduleRuleList = new List<ScheduleRule>();
        //    scheduleRuleList.Add(scheduleRuleAM);
        //    scheduleRuleList.Add(scheduleRulePM);

        //    // Step 2. Choose a slot
        //    foreach (var item in scheduleRuleList)
        //    {
        //        var scheduleBlockList = scheduleRulePM.GetScheduleBlockList(new DateTime(2018, 2, 4), new DateTime(2018, 11, 17), null);
        //    }

        //    // Step 3. 
        //}
    }
}
