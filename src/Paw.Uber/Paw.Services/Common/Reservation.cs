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
    public class Reservation : IId, IProviderId, IAudit
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

        public Guid? ReservationGroupId
        {
            get { return _ReservationGroupId; }
            set { _ReservationGroupId = value; }
        }
        private Guid? _ReservationGroupId = null;
        
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        public virtual ICollection<SkuLine> SkuLineCollection { get; set; }

        [NotMapped]
        public DateTime? StartDateTime
        {
            get
            {
                DateTime? result = null;
                if (this.SkuLineCollection != null)
                {
                    foreach (var item in this.SkuLineCollection)
                    {
                        if (item.StartTime != null && (result == null || item.StartTime < result))
                        {
                            result = item.StartTime;
                        }
                    }
                }

                return result;
            }
        }

        #region Schedule ...
        

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



    }
}
