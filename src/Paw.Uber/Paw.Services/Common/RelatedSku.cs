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
    public class RelatedSku : IId, IProviderId, IExternalId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [Index("UK_RelatedSku_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 10)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        [Index("UK_RelatedSku_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 20)]
        [MaxLength(100)]
        public string ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private string _ExternalId = null;

        public Guid SourceSkuId
        {
            get { return _SourceSkuId; }
            set { _SourceSkuId = value; }
        }
        private Guid _SourceSkuId = Guid.Empty;

        [ForeignKey("SourceSkuId")]
        public virtual Sku Source { get; set; }

        public Guid TargetSkuId
        {
            get { return _TargetSkuId; }
            set { _TargetSkuId = value; }
        }
        private Guid _TargetSkuId = Guid.Empty;

        [ForeignKey("TargetSkuId")]
        public virtual Sku Target { get; set; }

        public int RelationShipType
        {
            get { return _RelationShipType; }
            set { _RelationShipType = value; }
        }
        private int _RelationShipType = 0; // 0 == AddOn source, 1 == SubSku


    }
}
