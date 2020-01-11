using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paw.Services.Common;

namespace Paw.Services.Messages.Web.ScheduleBlocks
{
    public class ScheduleBlockManager
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<ScheduleBlock> ScheduleBlockList
        {
            get { return _ScheduleBlockList; }
            set { _ScheduleBlockList = value; }
        }
        private List<ScheduleBlock> _ScheduleBlockList = new List<ScheduleBlock>();

        public List<ScheduleRule> ScheduleRuleList
        {
            get { return _ScheduleRuleList; }
            set { _ScheduleRuleList = value; }
        }
        private List<ScheduleRule> _ScheduleRuleList = new List<ScheduleRule>();

        public DataContext DataContext
        {
            get { return _DataContext; }
            set { _DataContext = value; }
        }
        private DataContext _DataContext = new DataContext();

        public static ScheduleBlockManager Create(Guid providerId)
        {
            ScheduleBlockManager result = new ScheduleBlockManager();

            result.ProviderId = providerId;

            // Step 1. Create DataContext
            result.DataContext = DataContext.Create();

            // Step 2. Load Blocks
            result.ScheduleBlockList = result.DataContext.ScheduleBlockSet.Where(x => x.ProviderId == result.ProviderId).ToList();

            // Step 3. Load Rules
            result.ScheduleRuleList = result.DataContext.ScheduleRuleSet.Where(x => x.ProviderId == result.ProviderId).ToList();

            // Step 3. Cache

            return result;
        }


        public ScheduleBlock GetScheduleBlock(Guid id)
        {
            return this.ScheduleBlockList.Find(x => x.Id == id);
        }

        public ScheduleBlock GetScheduleBlock(Guid id, Guid ruleId, DateTime start)
        {
            ScheduleBlock scheduleBlock = GetScheduleBlock(id);

            if (scheduleBlock != null)
            {
                return scheduleBlock;
            }

            //TODO: Attempt to get it from db

            // Create new
            ScheduleRule scheduleRule = this.ScheduleRuleList.Find(x => x.Id == ruleId);

            return new ScheduleBlock()
            {
                Id = id,
                EmployeeId = scheduleRule.EmployeeId,
                Start = start,
                End = new DateTime(start.Year, start.Month, start.Day, scheduleRule.EndHour.Value, scheduleRule.EndMinute.Value, 0),
            };
        }

        public List<ScheduleBlock> GetScheduleBlockList(DateTime startDate, DateTime endDate, Guid? employeeId)
        {
            // Step 1. Get materialized blocks for employee
            List<ScheduleBlock> materializedScheduleBlockList = this.ScheduleBlockList.FindAll(x => (x.Start >= startDate && x.End <= startDate) || (x.Start >= endDate && x.End <= endDate) && x.EmployeeId == employeeId);

            // Step 2. Get rules for employee
            List<ScheduleRule> scheduleRuleList = this.ScheduleRuleList.FindAll(x => (startDate >= x.RuleStartDate && (x.RuleEndDate == null || startDate <= x.RuleEndDate)) && x.EmployeeId == employeeId);
            
            // step 3. Expand Schedule Block
            List<ScheduleBlock> result = new List<ScheduleBlock>();

            foreach (ScheduleRule scheduleRule in scheduleRuleList)
            {
                result.AddRange(scheduleRule.GetScheduleBlockList(startDate, endDate, materializedScheduleBlockList).Values);
            }

            return result;

        }

        public int AddScheduleBlock(ScheduleBlock scheduleBlock)
        {
            this.DataContext.ScheduleBlockSet.Add(scheduleBlock);
            int result = this.DataContext.SaveChanges();
            this.ScheduleBlockList.Add(scheduleBlock);
            return result;
        }

        public int UpdateScheduleBlock(ScheduleBlock scheduleBlock) // todo make this the message
        {
            return this.DataContext.SaveChanges();
        }
    }
}
