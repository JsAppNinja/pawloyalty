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
    public class Provider : IId, IProviderGroupId, IAudit
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(100)]
        [Index("IX_Provider_Key", IsUnique = true, Order = 0)]
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;

        [MaxLength(100)]
        [Index("IX_Provider_Domain", IsUnique = true, Order = 0)]
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }
        private string _Domain = String.Empty;
        
        #region Profile ...

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        private string _Url = String.Empty;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _PhoneNumber = String.Empty;

        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private bool _Status = true;
        

        #endregion

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

        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        [ForeignKey("ProviderGroupId")]
        public virtual ProviderGroup ProviderGroup { get; set; }


        public Guid? TimezoneInfoId
        {
            get { return _TimezoneInfoId; }
            set { _TimezoneInfoId = value; }
        }
        private Guid? _TimezoneInfoId = null;



        #region Relationships ...

        public virtual ICollection<Employee> EmployeeCollection { get; set; }

        public virtual ICollection<Invoice> InvoiceCollection { get; set; }

        public virtual ICollection<Resource> ResourceCollection { get; set; }

        public virtual ICollection<SkuCategory> SkuCategoryCollection { get; set; }

        public virtual ICollection<Sku> SkuCollection { get; set; }

        public virtual ICollection<SkuGroup> SkuGroupCollection { get; set; }

        public virtual ICollection<SkuGroupSku> SkuGroupSkuCollection { get; set; }
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
