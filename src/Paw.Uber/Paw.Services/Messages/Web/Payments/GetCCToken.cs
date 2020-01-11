using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.Payments
{
    public class GetCCToken
    {
        public Guid OwnerId
        {
            get { return _OwnerId; }
            set { _OwnerId = value; }
        }
        private Guid _OwnerId = Guid.Empty;

        public CCToken ExecuteItem()
        {
            throw new NotImplementedException();
        }
    }
}
