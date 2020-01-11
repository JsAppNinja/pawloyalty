using Paw.Services.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Identity
{

    public class PawUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {

        public PawUserStore(DataContext context) : base(context)
        {
            
        }
    }
}
