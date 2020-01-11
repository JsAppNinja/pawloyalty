
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
    public class ProviderGroup : IId, IAudit
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(200)]
        [Index("IX_ProviderGroup_Name", IsUnique = true, Order = 0)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;


        public virtual ICollection<Provider> ProviderCollection { get; set; }

        public virtual ICollection<Pet> PetCollection { get; set; }

        public virtual ICollection<Owner> OwnerCollection { get; set; }

        public virtual ICollection<Breed> BreedCollection { get; set; }

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

    }
}
