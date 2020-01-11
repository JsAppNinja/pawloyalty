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
    public class VaccinationRecord : IId, IProviderGroupId
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        [ForeignKey("ProviderGroupId")]
        public virtual ProviderGroup ProviderGroup { get; set; }

        public Guid VaccineId
        {
            get { return _VaccineId; }
            set { _VaccineId = value; }
        }
        private Guid _VaccineId = Guid.Empty;

        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }

        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }

        public DateTime? Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private DateTime? _Date = null;

        public DateTime? Expiration
        {
            get { return _Expiration; }
            set { _Expiration = value; }
        }
        private DateTime? _Expiration = null;



    }
}
