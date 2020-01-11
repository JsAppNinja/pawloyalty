using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Paw.Services.Messages.Util.Users
{
    public class BootstrapUser
    {
        public static Guid UserId
        {
            get { return new Guid("{CCFD1DAE-F278-4800-B9ED-50142A963202}");  }
        }

        public int Execute()
        {
            User user = new User() { UserName = "doug@radcloud.com", Email = "doug@radcloud.com", Id = BootstrapUser.UserId, PasswordHash= "AE/fjpX1A+iDssZtM1DeOsZJDWG9w/2rzrIjVolEk6dYTFJv805LYpy+NIaOt1lhrA==", SecurityStamp = Guid.NewGuid().ToString() };
            using (DataContext dataContext = DataContext.CreateForMessage(this))
            {
                dataContext.UserSet.AddOrUpdate(user);
                return dataContext.SaveChanges();
            }
        }
    }
}
