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
    public class TagPet
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public Guid TagId
        {
            get { return _TagId; }
            set { _TagId = value; }
        }
        private Guid _TagId = Guid.Empty;

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }

        public Guid PetId
        {
            get { return _PetId; }
            set { _PetId = value; }
        }
        private Guid _PetId = Guid.Empty;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        
}
}
