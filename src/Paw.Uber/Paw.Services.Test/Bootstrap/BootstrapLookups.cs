using Paw.Services.Messages.Util.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;

namespace Paw.Services.Test.Bootstrap
{
    [TestClass]
    public class BootstrapLookups
    {
        
        [TestMethod]
        public void GenderSetup()
        {

            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.GenderSet.AddOrUpdate(
                    new Gender { Name = "Male", DisplayOrder=10,  Id = Gender.Male },
                    new Gender { Name = "Female", DisplayOrder = 30, Id = Gender.Female },
                    new Gender { Name = "Male Neutered", DisplayOrder = 20, Id = Gender.MaleNeutered },
                    new Gender { Name = "Female Spayed", DisplayOrder = 40, Id = Gender.FemaleSpayed }
                    );
                

                context.SaveChanges();
            }
        }
        
        
    }
}
