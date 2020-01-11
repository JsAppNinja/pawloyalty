
using Paw.Services.Common;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Identity
{
    public class PawSignInManager : SignInManager<User, Guid>
    {
        public PawSignInManager(PawUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((PawUserManager)UserManager);
        }

        public static PawSignInManager Create(IdentityFactoryOptions<PawSignInManager> options, IOwinContext context)
        {
            return new PawSignInManager(context.GetUserManager<PawUserManager>(), context.Authentication);
        }
    }
}
