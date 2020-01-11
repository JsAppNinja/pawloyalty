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
    public class SkuDimension
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();
        
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
