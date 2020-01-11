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
    public class SkuGroup : IId, IProviderId, IExternalId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [Index("UK_SkuGroup_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 10)]
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        [Index("UK_SkuGroup_ProviderId_ExternalId", IsClustered = false, IsUnique = true, Order = 20)]
        [MaxLength(100)]
        public string ExternalId
        {
            get { return _ExternalId; }
            set { _ExternalId = value; }
        }
        private string _ExternalId = null;

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _Type = 1; // 1 == AddOn, 2 == SubSku

        

        public virtual ICollection<SkuGroupSku> SkuGroupSkuCollection { get; set; }
    }
}
