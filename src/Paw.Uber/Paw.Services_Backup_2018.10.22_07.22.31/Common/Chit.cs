using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Chit : IId, IProviderId
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.Empty;
        
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;
        
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public Guid? OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid? _OwnerId = null;

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        public Guid? PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid? _PetId = null;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        
        public Guid? SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid? _SkuId = null;

        [ForeignKey("SkuId")]
        public Sku Sku { get; set; }



    }
}
