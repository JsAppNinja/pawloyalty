using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Tag
    {
        [Key]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(50)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = string.Empty;

        [MaxLength(200)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Description = string.Empty;

        [MaxLength(50)]
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        private string _Icon = string.Empty;
}
}
