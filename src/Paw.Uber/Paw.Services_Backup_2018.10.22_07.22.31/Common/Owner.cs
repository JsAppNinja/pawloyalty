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
    public class Owner : IId, IProviderGroupId, IAudit
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(100)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName = String.Empty;

        [MaxLength(100)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName = String.Empty;

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _PhoneNumber = String.Empty;

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        public string AltPhoneNumber
        {
            get { return _AltPhoneNumber; }
            set { _AltPhoneNumber = value; }
        }
        private string _AltPhoneNumber = String.Empty;
        
        [Index("IX_Owner_Email", IsUnique=true)]
        [DataType(DataType.EmailAddress)]
        [MaxLength(300)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Email = String.Empty;

        #region Emergency Contact ...

        [MaxLength(100)]
        public string EmergencyContactName
        {
            get { return _EmergencyContactName; }
            set { _EmergencyContactName = value; }
        }
        private string _EmergencyContactName = String.Empty;

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string EmergencyContactPhoneNumber
        {
            get { return _EmergencyContactPhoneNumber; }
            set { _EmergencyContactPhoneNumber = value; }
        }
        private string _EmergencyContactPhoneNumber = String.Empty;
        
        #endregion


        #region Address ...

        [MaxLength(200)]
        public string StreetAddress
        {
            get { return _StreetAddress; }
            set { _StreetAddress = value; }
        }
        private string _StreetAddress = String.Empty;

        [MaxLength(50)]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _City = String.Empty;

        [MaxLength(20)]
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _State = String.Empty;

        [MaxLength(20)]
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }
        private string _PostalCode = String.Empty;
        
        #endregion

        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        [ForeignKey("ProviderGroupId")]
        public virtual ProviderGroup ProviderGroup { get; set; }
        
        public ICollection<Pet> PetCollection { get; set; }

        public ICollection<SchedulerEvent> SchedulerEventCollection { get; set; }

        #region Test ...

        public bool? TestFlag
        {
            get { return _TestFlag; }
            set { _TestFlag = value; }
        }
        private bool? _TestFlag = null;

        public Guid? TestGroupId
        {
            get { return _TestGroupId; }
            set { _TestGroupId = value; }
        }
        private Guid? _TestGroupId = null;

        #endregion

        #region Legacy ...

        public Guid? LegacyId
        {
            get { return _LegacyId; }
            set { _LegacyId = value; }
        }
        private Guid? _LegacyId = null;

        public DateTime? ImportDate
        {
            get { return _ImportDate; }
            set { _ImportDate = value; }
        }
        private DateTime? _ImportDate = null;
        
        #endregion

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        [NotMapped]
        public string LastFirst
        {
            get
            {
                string result = string.Empty;
                if (string.IsNullOrEmpty(this.LastName)) return string.Empty;

                if (string.IsNullOrEmpty(this.FirstName)) return this.LastName;

                return string.Format("{0}, {1}", this.LastName, this.FirstName).TrimEnd();
            }
        }


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
