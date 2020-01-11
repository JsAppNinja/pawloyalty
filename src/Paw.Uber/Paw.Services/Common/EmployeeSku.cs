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
    public class EmployeeSku : IId, IProviderId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public Guid SkuId
        {
            get { return _SkuId; }
            set { _SkuId = value; }
        }
        private Guid _SkuId = Guid.Empty;

        [ForeignKey("SkuId")]
        public virtual Sku Source { get; set; }

        public Guid EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private Guid _EmployeeId = Guid.Empty;

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
