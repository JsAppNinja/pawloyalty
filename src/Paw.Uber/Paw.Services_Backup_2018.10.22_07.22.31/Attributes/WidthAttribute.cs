using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Attributes
{
    public class WidthAttribute : Attribute
    {
        public WidthAttribute(int columns = 3)
        {
            this.Columns = columns;
        }

        public int Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }
        private int _Columns = 3;

        
        
    }
}
