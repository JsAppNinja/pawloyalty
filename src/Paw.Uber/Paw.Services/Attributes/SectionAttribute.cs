using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class SectionAttribute : Attribute
    {
        public SectionAttribute() { }

        public SectionAttribute(string text)
        {
            this.Text = text;
        }

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private string _Text = String.Empty;

        public string CSS
        {
            get { return _CSS; }
            set { _CSS = value; }
        }
        private string _CSS = "section";

        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        private string _Tag = "h4";

        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        private int _DisplayOrder = 0;

        public override object TypeId
        {
            get
            {
                return this;
            }
        }
        
    }
}
