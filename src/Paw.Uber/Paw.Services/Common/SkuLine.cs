using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class SkuLine : IId, IProviderId, INode<SkuLine>, IAudit
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public Guid? ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid? _ReservationId = null;

        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }

        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;

        [ForeignKey("PetId")]
        public Pet Pet { get; set; }
        
        public int? Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private int? _Quantity = 1;

        #region Sku Memo ...

        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

        [ForeignKey("SkuId")]
        public virtual Sku Sku { get; set; }

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [MaxLength(250)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [DataType(DataType.Currency)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private decimal? _Amount = null;

        public int SkuLineType
        {
            get { return _SkuLineType; }
            set { _SkuLineType = value; }
        }
        private int _SkuLineType = 1; // 1 == Service|2 == Boarding|3 == Product|4 == discount

        #endregion

        #region Status ...

        public DateTime? Voided // Dropped before paid
        {
            get { return _Voided; }
            set { _Voided = value; }
        }
        private DateTime? _Voided = null;

        public DateTime? Cancelled // Cancelled after payment
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        private DateTime? _Cancelled = null;
        
        #endregion

        #region ScheduleRule Block Memo ...

        public Guid? ScheduleRuleId
        {
            get { return _ScheduleRuleId; }
            set { _ScheduleRuleId = value; }
        }
        private Guid? _ScheduleRuleId = null;

        [ForeignKey("ScheduleRuleId")]
        public virtual ScheduleRule ScheduleRule { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime? _StartDate = null;
        
        [DataType(DataType.Time)]
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        private DateTime? _StartTime = null;

        #endregion

        #region Boarding Schedule ...

        [DataType(DataType.Date)]
        public DateTime? In
        {
            get { return _In; }
            set { _In = value; }
        }
        private DateTime? _In = null;

        [DataType(DataType.Date)]
        public DateTime? Out
        {
            get { return _Out; }
            set { _Out = value; }
        }
        private DateTime? _Out = null;

        public Guid? ResourceId
        {
            get { return _ResourceId; }
            set { _ResourceId = value; }
        }
        private Guid? _ResourceId = null;

        [ForeignKey("ResourceId")]
        public virtual Resource Resource { get; set; }

        public int? Slot
        {
            get { return _Slot; }
            set { _Slot = value; }
        }
        private int? _Slot = null;
        
        #endregion

        #region INode ...

        public Guid? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        private Guid? _ParentId = null;

        [ForeignKey("ParentId")]
        public virtual SkuLine Parent { get; set; }

        public ICollection<SkuLine> ChildCollection { get; set; }

        #endregion
        
        #region Audit ...

        public Guid MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }
        private Guid _MessageId = Muid.Comb();

        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime _Created = DateTime.UtcNow;

        public Guid? CreatedById
        {
            get { return _CreatedById; }
            set { _CreatedById = value; }
        }
        private Guid? _CreatedById = null;

        [ForeignKey("UpdatedById")]
        public virtual User CreatedBy { get; set; }

        public DateTime Updated
        {
            get { return _Updated; }
            set { _Updated = value; }
        }
        private DateTime _Updated = DateTime.UtcNow;

        public Guid? UpdatedById
        {
            get { return _UpdatedById; }
            set { _UpdatedById = value; }
        }
        private Guid? _UpdatedById = null;

        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }

        [MaxLength(250)]
        public string MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }
        private string _MessageType = String.Empty;

        [MaxLength(250)]
        public string MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value; }
        }
        private string _MachineName = String.Empty;


        #endregion

        // Actions ...

        // Decrease Quantity

        // Cancel

        // Delete

        // Reschedule

        // Lock

        // Save

        // Total
    }
}
