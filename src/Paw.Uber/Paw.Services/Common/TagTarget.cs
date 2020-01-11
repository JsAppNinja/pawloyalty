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
    public class TagTarget
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
        public Tag Tag { get; set; }

        [MaxLength(50)]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private string _TypeName = string.Empty;
    }
}
