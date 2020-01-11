
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Common
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>, IId, IUser<Guid>
    {
        public User()
        {
            
        }
        
        public override int AccessFailedCount { get; set; }

        [Required]
        [MaxLength(254)]
        public override string Email { get; set; }

        [Index("IX_User_UserName", IsUnique = true, Order = 0)]
        public override string UserName { get; set; }

        public override bool EmailConfirmed
        {
            get { return _EmailConfirmed; }
            set { _EmailConfirmed = value; }
        }
        private bool _EmailConfirmed = false;
        
        [MaxLength(100)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _FirstName = String.Empty;
        
        [Key]
        public override Guid Id 
        {
            get { return _Id; }
            set {_Id = value; } 
        }
        private Guid _Id = Muid.Comb();

        [MaxLength(100)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _LastName = String.Empty;

        #region Test ...

        public bool? TestFlag
        {
            get { return _TestFlag; }
            set { _TestFlag = value; }
        }
        private bool? _TestFlag = null;

        public Guid? TestGroupId
        {
            get { return _TestGroupId; }
            set { _TestGroupId = value; }
        }
        private Guid? _TestGroupId = null;

        #endregion

        #region Legacy ...

        public Guid? LegacyId
        {
            get { return _LegacyId; }
            set { _LegacyId = value; }
        }
        private Guid? _LegacyId = null;

        public DateTime? ImportDate
        {
            get { return _ImportDate; }
            set { _ImportDate = value; }
        }
        private DateTime? _ImportDate = null;

        #endregion

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        [NotMapped]
        public string LastFirst
        {
            get
            {
                string result = string.Empty;
                if (string.IsNullOrEmpty(this.LastName)) return string.Empty;

                if (string.IsNullOrEmpty(this.FirstName)) return this.LastName;

                return string.Format("{0}, {1}", this.LastName, this.FirstName).TrimEnd();
            }
        }

        public override bool LockoutEnabled { get; set; }

        public override DateTime? LockoutEndDateUtc { get; set; }

        public override string PasswordHash { get; set; }

        [MaxLength(20)]
        public override string PhoneNumber { get; set; }

        public override bool PhoneNumberConfirmed { get; set; }

        [MaxLength(200)]
        public override string SecurityStamp { get; set; }

        public override bool TwoFactorEnabled { get; set; }

        #region Identity ... 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // TODO: Add Claims
            // userIdentity.AddClaim(new Claim("Role", "User"));

            return userIdentity;
        }

        #endregion

        #region LegacyPassword ...

        public bool UseLegacyPassword
        {
            get { return _UseLegacyPassword; }
            set { _UseLegacyPassword = value; }
        }
        private bool _UseLegacyPassword = false;

        public string LegacyPasswordAndSalt
        {
            get { return _LegacyPasswordAndSalt; }
            set { _LegacyPasswordAndSalt = value; }
        }
        private string _LegacyPasswordAndSalt = String.Empty;



        #endregion


    }
}
