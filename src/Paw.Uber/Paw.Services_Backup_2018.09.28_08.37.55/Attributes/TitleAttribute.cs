using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Attributes
{
    public class TitleAttribute : Attribute
    {
        public TitleAttribute() { }

        public TitleAttribute(string title)
        {
            this.Title = title;
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Title = String.Empty;
    }
}
