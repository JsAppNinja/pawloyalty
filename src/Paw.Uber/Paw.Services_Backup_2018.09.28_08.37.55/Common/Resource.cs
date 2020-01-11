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
    public class Resource : IId, IProviderId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
        [Index("UK_Resource_ProviderId_Key", IsUnique = true, Order = 20 )]
        [MaxLength(55)]
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;
        
        public int? Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }
        private int? _Capacity = null;

        public Guid? SkuCategoryId
        {
            get { return _SkuCategoryId; }
            set { _SkuCategoryId = value; }
        }
        private Guid? _SkuCategoryId = null;

        [ForeignKey("SkuCategoryId")]
        public virtual SkuCategory SkuCategory { get; set; }

        public int? MaxPetWeight
        {
            get { return _MaxPetWeight; }
            set { _MaxPetWeight = value; }
        }
        private int? _MaxPetWeight = null;

        public bool IsShared
        {
            get { return _IsShared; }
            set { _IsShared = value; }
        }
        private bool _IsShared = true;
        
        [MaxLength(55)]
        public string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        private string _ShortDescription = String.Empty;
        
        [MaxLength(100)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [MaxLength(20)]
        public string LegacyId
        {
            get { return _LegacyId; }
            set { _LegacyId = value; }
        }
        private string _LegacyId = String.Empty;

        [Index("UK_Resource_ProviderId_Key", IsUnique = true, Order = 10)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }


    }
}
