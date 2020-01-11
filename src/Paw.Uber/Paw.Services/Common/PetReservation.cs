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
    public class PetReservation
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

        public Guid ReservationId
        {
            get { return _ReservationId; }
            set { _ReservationId = value; }
        }
        private Guid _ReservationId = Guid.Empty;

        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }
        
        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }

        public DateTime? ScheduledCheckin
        {
            get { return _ScheduledCheckin; }
            set { _ScheduledCheckin = value; }
        }
        private DateTime? _ScheduledCheckin = null;

        public DateTime? ScheduledCheckout
        {
            get { return _ScheduledCheckout; }
            set { _ScheduledCheckout = value; }
        }
        private DateTime? _ScheduledCheckout = null;

        public DateTime? ActualCheckin
        {
            get { return _ActualCheckin; }
            set { _ActualCheckin = value; }
        }
        private DateTime? _ActualCheckin = null;

        public DateTime? ActualCheckout
        {
            get { return _ActualCheckout; }
            set { _ActualCheckout = value; }
        }
        private DateTime? _ActualCheckout = null;

        public DateTime? Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        private DateTime? _Cancelled = null;

        public virtual ICollection<Service> ServiceCollection { get; set; }

        public virtual ICollection<SkuLine> SkuLineCollection { get; set; }

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
