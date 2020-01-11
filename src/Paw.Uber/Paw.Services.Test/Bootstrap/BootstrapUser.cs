using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Paw.Services.Test.Bootstrap
{
    [TestClass]
    public class BootstrapUser
    {
        public static Guid UserId
        {
            get { return new Guid("{CCFD1DAE-F278-4800-B9ED-50142A963202}"); }
        }

        [TestMethod]
        public void UserSetup()
        {
            User user = new User() { UserName = "doug@radcloud.com", Email = "doug@radcloud.com", Id = BootstrapUser.UserId, PasswordHash = "AE/fjpX1A+iDssZtM1DeOsZJDWG9w/2rzrIjVolEk6dYTFJv805LYpy+NIaOt1lhrA==", SecurityStamp = Guid.NewGuid().ToString() };
            using (DataContext dataContext = new DataContext(){ CurrentUserId = BootstrapUser.UserId })
            {
                dataContext.UserSet.AddOrUpdate(user);
                dataContext.SaveChanges();
            }
        }
        
    }
}
