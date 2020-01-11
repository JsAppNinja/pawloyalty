using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Skus
{
    public class GetAddOnSkuList
    {
        public Guid ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        private Guid _ProviderId = Guid.Empty;

        public List<KeyValuePair<Guid, string>> ExecuteList()
        {
            List<KeyValuePair<Guid, string>> result = new List<KeyValuePair<Guid, string>>();
            
            result.Add(new KeyValuePair<Guid,string>(new Guid("{AF3D1E42-3926-4D96-8AE5-3B376D093734}"), "Ear Cleaning $10.00"));
            result.Add(new KeyValuePair<Guid,string>(new Guid("{C9B4C89D-38CA-4960-B14D-7E535EED32D1}"), "Nail Trim $20.00"));
            result.Add(new KeyValuePair<Guid,string>(new Guid("{577D412F-1A4D-42F4-84EA-1B37F3EA509A}"), "Teeth Brusing $15.00"));

            return result;
        }

        

    }
}
