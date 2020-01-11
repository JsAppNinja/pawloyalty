using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Attributes.ClientData
{
    public class AddAttributeAttribute : Attribute
    {
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _Value = String.Empty;

        public virtual void AddAttribute(object model, Dictionary<string, object> attributes)
        {
            // Append value
            if (attributes.ContainsKey(this.Key))
            {
                attributes[this.Key] = attributes[this.Key] + " " + this.Value;
            }

            attributes.Add(this.Key, this.Value);
        }


    }
}
