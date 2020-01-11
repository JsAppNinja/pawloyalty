using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Util.Users;
using Paw.Services.Messages.Web.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Paw.Services.Identity
{
    public static class IdentityHelper
    {
        public static Guid? GetCurrentUserId()
        {
            Claim claim = GetClaim(UserIdClaimType);

            if (claim == null)
            {
                return BootstrapUser.UserId;
            }

            Guid currentUserId;
            if (!Guid.TryParse(claim.Value, out currentUserId)) return null;

            return currentUserId;
        }

        public static User GetUser()
        {
            Guid? userId = GetCurrentUserId();

            if (userId == null)
            {
                return null;
            }

            return new GetUser() { Id = (Guid)userId }.ExecuteItem();
        }


        public static ClaimsIdentity GetIdentity()
        {
            var principal = Thread.CurrentPrincipal;

            if (principal == null) return null;

            ClaimsIdentity claimsIdentity = principal.Identity as ClaimsIdentity;

            return claimsIdentity;
        }

        public static Claim GetClaim(string type)
        {
            ClaimsIdentity claimIdentity = GetIdentity() as ClaimsIdentity;
            if (claimIdentity == null) return null;

            return claimIdentity.FindFirst(type);
        }

        public static bool UserEdit()
        {
            // TODO: Create Test for Edit Permission
            return true;
        }

        public static string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public static string UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public static string UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    }
}
