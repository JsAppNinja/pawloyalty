using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class ScheduleSkuLine
    {

        [ScaffoldColumn(false)]
        public Guid Id // PrimarySkuLine
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid _ReservationId = Guid.Empty;

        [ScaffoldColumn(false)]
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        public Guid? EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid? _EmployeeId = null;

        [DataType(DataType.Date)]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime? _StartDate = null;
        
        [Required]
        public Guid? ScheduleRuleId
        {
            get { return _ScheduleRuleId; }
            set { _ScheduleRuleId = value; }
        }
        private Guid? _ScheduleRuleId = null;

        public int ExecuteNonQuery()
        {
            using (DataContext context = DataContext.CreateForMessage(this))
            {
                // Step 1. Get SkuLine
                SkuLine skuLine = context.SkuLineSet.Where(x => x.Id == this.Id).SingleOrDefault();

                if (skuLine == null)
                {
                    throw new InvalidOperationException("SkuLine not found.");
                }

                // Step 2. Set ScheduleRuleId
                skuLine.ScheduleRuleId = this.ScheduleRuleId;
                skuLine.StartDate = this.StartDate;

                return context.SaveChanges();
            }
        }

    }
}
