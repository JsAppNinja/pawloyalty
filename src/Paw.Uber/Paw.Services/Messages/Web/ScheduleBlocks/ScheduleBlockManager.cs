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
        
        public List<ScheduleRule> ScheduleRuleList
        {
            get { return _ScheduleRuleList; }
            set { _ScheduleRuleList = value; }
        }
        private List<ScheduleRule> _ScheduleRuleList = new List<ScheduleRule>();

        public List<Employee> EmployeeList
        {
            get { return _EmployeeList; }
            set { _EmployeeList = value; }
        }
        private List<Employee> _EmployeeList = new List<Employee>();
        
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
            
            // Step 2. Load Rules and Blocks
            result.ScheduleRuleList = result.DataContext
                .ScheduleRuleSet
                .Include("ScheduleBlockCollection")
                .Where(x => x.ProviderId == result.ProviderId && x.RuleEndDate < DateTime.UtcNow.Date.AddDays(-30)) // Only show rules that expired within the last 30 days
                .ToList();

            // Step 3. Get employee list
            List<Employee> employeeList = new List<Employee>();
            foreach (var item in result.ScheduleRuleList)
            {
                if (item.Employee != null)
                {
                    employeeList.Add(item.Employee);
                }
            }

            // Step 4. Get Distinct and Order by Employee
            result.EmployeeList = employeeList.GroupBy(item => item.Id).Select(group => group.First()).OrderBy(e => e.FullName).ToList();
            
            return result;
        }

        #region CRUD ...

        public List<ScheduleBlock> GetScheduleBlockList(DateTime startDate, DateTime endDate, Guid skuCategoryId, List<Guid?> employeeFilter = null)
        {
            // Step 1. Get daycount
            int dayCount = Convert.ToInt32((endDate - startDate).TotalDays);
            if (dayCount < 1)
            {
                dayCount = 1;
            }

            // Step 2. Create result
            List<ScheduleBlock> result = new List<ScheduleBlock>();

            // Step 3. Add rules for period
            for (int i = 0; i < dayCount; i++)
            {
                DateTime date = startDate.AddDays(0);
                foreach (ScheduleRule scheduleRule in this.ScheduleRuleList.FindAll(x => x.SkuCategoryId == skuCategoryId && ((employeeFilter == null || employeeFilter.Count == 0) || employeeFilter.Any(y => y == x.EmployeeId))))
                {
                    ScheduleBlock scheduleBlock;
                    if (scheduleRule.TryGetDate(date, out scheduleBlock))
                    {
                        result.Add(scheduleBlock);
                    }
                }
            }

            // Step 4. 
            result = result.OrderBy(x => x.Start).ThenBy(x => x.ScheduleRule.EmployeeId).ToList();

            return result;
        }

        public void UpsertScheduleBlock(ScheduleBlock scheduleBlock)
        {
            // Step 1. Find existing
            ScheduleBlock item = GetScheduleBlock(scheduleBlock.Start, scheduleBlock.ScheduleRule.EmployeeId);
            bool exists = item != null;

            if (!exists)
            {
                item = new ScheduleBlock() {    Id = scheduleBlock.Id, ProviderId = this.ProviderId };
                this.ScheduleRuleList.Find(x => x.Id == scheduleBlock.ScheduleRuleId).ScheduleBlockCollection.Add(item);
            }

            // Step 2. 
            this.Map(scheduleBlock, item);
            
            // Step 3. 
            using (DataContext dataContext = DataContext.Create())
            {
                //if (scheduleBlock.Saved == null)
                //{
                //    dataContext.ScheduleBlockSet.Add(item);
                //}
                //else
                //{
                //    var existingItem = dataContext.ScheduleBlockSet.Where(x => x.Start == scheduleBlock.Start && x.ScheduleRule.EmployeeId == scheduleBlock.ScheduleRule.EmployeeId);
                //    this.Map(scheduleBlock, item);
                //}
            }
        }

        private void Map(ScheduleBlock source, ScheduleBlock target)
        {
            target.End = source.End;
            target.IsAllDay = source.IsAllDay;
            target.ScheduleRuleId = source.ScheduleRuleId;
            target.Start = source.Start;
            target.Saved = source.Saved ?? DateTime.UtcNow;
        }

        #endregion

        #region Search ...

        public ScheduleBlock GetScheduleBlock(Predicate<ScheduleBlock> predicate)
        {
            foreach (ScheduleRule scheduleRule in this.ScheduleRuleList)
            {
                foreach (ScheduleBlock scheduleBlock in scheduleRule.ScheduleBlockCollection)
                {
                    if (predicate(scheduleBlock))
                    {
                        return scheduleBlock;
                    }
                }
            }
            return null;
        }

        public ScheduleBlock GetScheduleBlock(Guid id)
        {
            return this.GetScheduleBlock(x => x.Id == id);
        }

        public ScheduleBlock GetScheduleBlock(DateTime start, Guid? employeeId)
        {
            return this.GetScheduleBlock(x => x.Start == start && x.ScheduleRule.EmployeeId == employeeId);
        }

        #endregion
    }
}
