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
    public class Pet : IId, IProviderGroupId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [DataType(DataType.Date)]
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        private DateTime? _DOB = null;
        
        public Guid? GenderId
        {       
            get { return _GenderId; }
            set { _GenderId = value; }
        }
        private Guid? _GenderId = null;

        public virtual Gender Gender { get; set; }

        public double? Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        private double? _Weight = null;

        public bool IsDeceased
        {
            get { return _IsDeceased; }
            set { _IsDeceased = value; }
        }
        private bool _IsDeceased = false;

        public bool IsRescue
        {
            get { return _IsRescue; }
            set { _IsRescue = value; }
        }
        private bool _IsRescue = false;
        
        public Guid? BreedId
        {
            get { return _BreedId; }
            set { _BreedId = value; }
        }
        private Guid? _BreedId = null;

        [ForeignKey("BreedId")]
        public Breed Breed { get; set; }

        public bool Deceased
        {
            get { return _Deceased; }
            set { _Deceased = value; }
        }
        private bool _Deceased = false;

        public bool Blacklisted
        {
            get { return _Blacklisted; }
            set { _Blacklisted = value; }
        }
        private bool _Blacklisted = false;
        
        public Guid? VetId
        {
            get { return _VetId; }
            set { _VetId = value; }
        }
        private Guid? _VetId = null;

        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        [ForeignKey("ProviderGroupId")]
        public virtual ProviderGroup ProviderGroup { get; set; }

        public Guid? PetClassId
        {
            get { return _PetClassId; }
            set { _PetClassId = value; }
        }
        private Guid? _PetClassId = null;

        [ForeignKey("PetClassId")]
        public virtual PetClass PetClass { get; set; }

        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        public ICollection<VaccinationRecord> VaccinationRecordCollection { get; set; }

        #region Test ...

        public bool TestFlag
        {
            get { return _TestFlag; }
            set { _TestFlag = value; }
        }
        private bool _TestFlag = false;

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

        #region Audit ...

        public Guid MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }
        private Guid _MessageId = Guid.Empty;

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

        public static string GetDOB(DateTime? dob)
        {
            if (dob == null) return string.Empty;

            var today = DateTime.Today;

            TimeSpan timeSpan = today - dob.Value;

            int monthCount = ((today.Year - dob.Value.Year) * 12) + today.Month - dob.Value.Month;
            
            if (monthCount < 25)
            {
                return string.Format("{0} month old", monthCount); 
            }

            // Calculate the age.
            int age = today.Year - dob.Value.Year;

            // Go back to the year the person was born in case of a leap year
            if (dob > today.AddYears(-age))
            {
                age--;
            }

            return string.Format("{0:d} ({1} year old)", dob, age);
        }

        public static string GetWeight(Double? weight)
        {
            if (weight == null) return string.Empty;

            return string.Format("{0} lbs", weight);
        }
    }
}
