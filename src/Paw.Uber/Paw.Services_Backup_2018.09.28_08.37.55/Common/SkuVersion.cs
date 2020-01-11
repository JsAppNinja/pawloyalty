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
    public class SkuVersion : IId, IVersion<Sku>
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        #region Version ...

        public Guid Version_CurrentId
        {
            get { return _Version_CurrentId; }
            set { _Version_CurrentId = value; }
        }
        private Guid _Version_CurrentId = Muid.Comb();

        [ForeignKey("Version_Id")]
        public Sku Version_Current { get; set; }

        public string Version_Operation
        {
            get { return _Version_Operation; }
            set { _Version_Operation = value; }
        }
        private string _Version_Operation = String.Empty; // INSERT | UPDATE | DELETE
        
        #endregion

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [MaxLength(50)]
        public string Original_Name
        {
            get { return _Original_Name; }
            set { _Original_Name = value; }
        }
        private string _Original_Name = String.Empty;

        [MaxLength(250)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [MaxLength(250)]
        public string Original_Description
        {
            get { return _Original_Description; }
            set { _Original_Description = value; }
        }
        private string _Original_Description = String.Empty;

        public double? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private double? _Amount = null;

        public double? Original_Amount
        {
            get { return _Original_Amount; }
            set { _Original_Amount = value; }
        }
        private double? _Original_Amount = null;

        public Guid? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        private Guid? _ParentId = null;

        [ForeignKey("ParentId")]
        public virtual Sku ParentSku { get; set; }

        public Guid? Original_ParentId
        {
            get { return _Original_ParentId; }
            set { _Original_ParentId = value; }
        }
        private Guid? _Original_ParentId = null;

        [ForeignKey("Original_ParentId")]
        public virtual Sku Original_ParentSku { get; set; }

        // Parent Sku Versionsku taga

        public Guid? RootId
        {
            get { return _RootId; }
            set { _RootId = value; }
        }
        private Guid? _RootId = null;

        [ForeignKey("RootId")]
        public virtual Sku RootSku { get; set; }

        // Root Sku Version

        public Guid? Original_RootId
        {
            get { return _Original_RootId; }
            set { _Original_RootId = value; }
        }
        private Guid? _Original_RootId = null;

        [ForeignKey("Original_RootId")]
        public virtual Sku Original_RootSku { get; set; }

        #region Audit ...

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

        public DateTime Original_Updated
        {
            get { return _Original_Updated; }
            set { _Original_Updated = value; }
        }
        private DateTime _Original_Updated = DateTime.UtcNow;

        public Guid? UpdatedById
        {
            get { return _UpdatedById; }
            set { _UpdatedById = value; }
        }
        private Guid? _UpdatedById = null;

        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }

        public Guid? Original_UpdatedById
        {
            get { return _Original_UpdatedById; }
            set { _Original_UpdatedById = value; }
        }
        private Guid? _Original_UpdatedById = null;

        [ForeignKey("UpdatedById")]
        public User Original_UpdatedBy { get; set; }

        #endregion
    }
}
