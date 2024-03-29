﻿using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class PetClass : IId
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

        [MaxLength(100)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;


    }
}
