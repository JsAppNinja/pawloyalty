using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Paw.Services.Common;
using System.Data.Entity.Migrations;

namespace Paw.Services.Messages.Util.Users
{
    public class ImportLogin
    {
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        private string _EmailAddress = null;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _UserName = null;

        public Guid? Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid? _Id = null;

        public Guid? TestGroupId
        {
            get { return _TestGroupId; }
            set { _TestGroupId = value; }
        }
        private Guid? _TestGroupId = null;


        public int Execute()
        {
            using (var connection = ConnectionHelper.Create("SourceDb"))
            {
                LegacyUser legacyUser = connection.Query<LegacyUser>("SELECT * FROM [Users] WHERE (Id = @Id OR @Id IS NULL) AND (Username = @Username OR @Username IS NULL) AND (EmailAddress = @EmailAddress OR @EmailAddress IS NULL) ", this).SingleOrDefault();
                User user = new User() { UserName = legacyUser.UserName, Email = legacyUser.EmailAddress, Id = legacyUser.Id, LegacyPasswordAndSalt = legacyUser.PasswordHash + "|" + legacyUser.PasswordSalt, UseLegacyPassword = true, TestFlag = true, TestGroupId = this.TestGroupId };
                using (DataContext dataContext = DataContext.CreateForMessage(this))
                {
                    dataContext.UserSet.AddOrUpdate(user);
                    return dataContext.SaveChanges();
                }
            }            
        }


    }
}
