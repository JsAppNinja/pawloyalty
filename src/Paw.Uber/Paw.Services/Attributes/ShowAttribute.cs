using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Attributes
{
    public class ShowAttribute : Attribute
    {
        public virtual bool Show
        {
            get { return _Show; }
            set { _Show = value; }
        }
        private bool _Show = true;

    }
}
