﻿using Paw.Services.Attributes;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class Peek : IId
    {
        [ScaffoldColumn(false)]
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name = String.Empty;

        [SetForeignKey]
        [ScaffoldColumn(false)]
        public Guid PeepId
        {
            get { return _PeepId; }
            set { _PeepId = value; }
        }
        private Guid _PeepId = Guid.Empty;

    }
}
