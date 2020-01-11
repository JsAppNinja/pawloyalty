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
        
        public int? Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }
        private int? _Capacity = null;
        
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
        
        [MaxLength(200)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;
        
        [MaxLength(500)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        [MaxLength(200)]
        public string LegacyId
        {
            get { return _LegacyId; }
            set { _LegacyId = value; }
        }
        private string _LegacyId = String.Empty;

        public int? ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private int? _ExternalId = null;
        
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [ForeignKey("SkuId")]
        public virtual Sku Sku { get; set; }
    }
}
