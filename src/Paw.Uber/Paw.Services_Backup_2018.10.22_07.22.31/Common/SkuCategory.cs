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
    public class SkuCategory : IId, IProviderId
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

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [MaxLength(250)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = String.Empty;

        public virtual ICollection<Sku> SkuCollection { get; set; }


        public Guid SchedulerTypeId
        {
            get { return _SchedulerTypeId; }
            set { _SchedulerTypeId = value; }
        }
        private Guid _SchedulerTypeId = SchedulerType.Appointment;
        
        [ForeignKey("SchedulerTypeId")]
        public virtual SchedulerType SchedulerType { get; set; }

        [MaxLength(50)]
        public string NavIcon
        {
            get { return _NavIcon; }
            set { _NavIcon = value; }
        }
        private string _NavIcon = String.Empty;

        public int NavDisplayOrder
        {
            get { return _NavDisplayOrder; }
            set { _NavDisplayOrder = value; }
        }
        private int _NavDisplayOrder = int.MaxValue;


    }
}
