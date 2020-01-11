using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Payments
{
    public class CCToken
    {
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _Value = String.Empty;
    }
}
