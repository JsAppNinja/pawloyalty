using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test
{
    [TestClass]
    public class ChronExpressionTests
    {
        [TestMethod]
        public void ScenarioTest()
        {
            

        }

        // Every weekday - no end

        // Every weekday - 6 month end date

        // Every TR
        
        // 
    }

    public class CronExpression
    {
        public int? Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }
        private int? _Minute = null;

        public int? Hour
        {
            get { return _Hour; }
            set { _Hour = value; }
        }
        private int? _Hour = null;

        public int? DayOfMonth
        {
            get { return _DayOfMonth; }
            set { _DayOfMonth = value; }
        }
        private int? _DayOfMonth = null;

        public string DayOfWeek
        {
            get { return _DayOfWeek; }
            set { _DayOfWeek = value; }
        }
        private string _DayOfWeek = string.Empty; // "MTWRFSU"

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private DateTime? _EndDate = null;

        public DateTime? Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        private DateTime? _Start = null;

        public TimeSpan? Duration // store store as ticks (bigint)
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        private TimeSpan? _Duration = null;

        public bool IsAllDayEvent
        {
            get { return _IsAllDayEvent; }
            set { _IsAllDayEvent = value; }
        }
        private bool _IsAllDayEvent = false;


    }
}
