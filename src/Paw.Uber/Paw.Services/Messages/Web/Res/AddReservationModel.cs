using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Res
{
    public class AddReservationModel
    {
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        public Guid SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid _SkuCategoryId = Guid.Empty;

        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        public Guid EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid _EmployeeId = Guid.Empty;

        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime _StartDate = DateTime.UtcNow.Date;

        public Guid ScheduleRuleId
        {
            get { return _ScheduleRuleId; }
            set { _ScheduleRuleId = value; }
        }
        private Guid _ScheduleRuleId = Guid.Empty;

        public List<Guid> RelatedSkuIdList
        {
            get { return _RelatedSkuIdList; }
            set { _RelatedSkuIdList = value; }
        }
        private List<Guid> _RelatedSkuIdList = new List<Guid>();

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }
    }
}
