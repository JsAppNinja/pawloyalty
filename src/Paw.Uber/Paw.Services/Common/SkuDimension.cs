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
    public class SkuDimension : IId, IProviderId, IExternalId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [Index("UK_SkuDimension_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 10)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        
        [Index("UK_SkuDimension_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 20)]
        [MaxLength(100)]
        public string ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private string _ExternalId = null;
        
        [Index("IX_SkuDimension", IsClustered = false, IsUnique=true, Order=0)]
        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

        [ForeignKey("SkuId")]
        public virtual Sku Sku { get; set; }

        [Index("IX_SkuDimension", IsClustered = false, IsUnique = true, Order = 1)]
        public Guid DimensionId
        {
            get { return _DimensionId; }
            set { _DimensionId = value; }
        }
        private Guid _DimensionId = Guid.Empty;

        [ForeignKey("DimensionId")]
        public virtual Dimension Dimension { get; set; }


    }
}
