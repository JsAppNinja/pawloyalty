using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Util.Users
{
    public class LegacyUser
    {
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Guid.NewGuid();

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _UserName = String.Empty;

        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        private string _EmailAddress = String.Empty;
        
        public string PasswordHash
        {
            get { return _PasswordHash; }
            set { _PasswordHash = value; }
        }
        private string _PasswordHash = String.Empty;

        public string PasswordSalt
        {
            get { return _PasswordSalt; }
            set { _PasswordSalt = value; }
        }
        private string _PasswordSalt = String.Empty;

        public DateTime? Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime? _Created = null;

        public DateTime? LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }
        private DateTime? _LastLogin = null;

        public DateTime? PreviousLogin
        {
            get { return _PreviousLogin; }
            set { _PreviousLogin = value; }
        }
        private DateTime? _PreviousLogin = null;

        
        
    }
}
