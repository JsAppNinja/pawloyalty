using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public class ExecuteValueAttribute : Attribute, IExecuteValue
    {
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        private string _Key = String.Empty;

        public bool Global
        {
            get { return _Global; }
            set { _Global = value; }
        }
        private bool _Global = false;
        

        public virtual object Execute(object model, object container, IController controller)
        {
            throw new NotImplementedException();
        }
    }
}
