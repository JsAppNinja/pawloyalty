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
    public class Payment : IId, IProviderId
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

        [MaxLength(200)]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private string _TypeName = string.Empty; // CreditCard | Cash

        public double? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private double? _Amount = null;

        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime _Created = DateTime.UtcNow;
        
    }
}
