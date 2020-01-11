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
    public class SkuCategory : IId, IProviderId, IIsDeleted, IDisplayOrder, IExternalId, IAudit
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [Index("UK_SkuCategory_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 10)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        [Index("UK_SkuCategory_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 20)]
        [MaxLength(100)]
        public string ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private string _ExternalId = null;
        
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

        public virtual ICollection<Sku> SkuCollection { get; set; }
        
        public Guid SchedulerTypeId
        {
            get { return _SchedulerTypeId; }
            set { _SchedulerTypeId = value; }
        }
        private Guid _SchedulerTypeId = SchedulerType.Appointment;
        
        [ForeignKey("SchedulerTypeId")]
        public virtual SchedulerType SchedulerType { get; set; }

        [MaxLength(60)]
        public string ScheduleModel
        {
            get { return _ScheduleModel; }
            set { _ScheduleModel = value; }
        }
        private string _ScheduleModel = string.Empty; // Date || DateTime || DateRange

        [MaxLength(50)]
        public string NavIcon
        {
            get { return _NavIcon; }
            set { _NavIcon = value; }
        }
        private string _NavIcon = String.Empty;

        public int NavDisplayOrder
        {
            get { return _NavDisplayOrder; }
            set { _NavDisplayOrder = value; }
        }
        private int _NavDisplayOrder = int.MaxValue;

        public bool IsPrimary
        {
            get { return _IsPrimary; }
            set { _IsPrimary = value; }
        }
        private bool _IsPrimary = false;
        
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        private int _DisplayOrder = 0;
        
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        private bool _IsDeleted = false;
        
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



    }
}
