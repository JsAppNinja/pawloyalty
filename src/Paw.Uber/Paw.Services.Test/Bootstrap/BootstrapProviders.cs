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
    public class BootstrapProviders
    {
        
        [TestMethod]
        public void ProvidersSetup()
        {

            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.ProviderGroupSet.AddOrUpdate(
                    new ProviderGroup { Name = "Hula Dog, LLC.", TestFlag = true, Id = UpsertProviderGroup.HulaDogProviderGroupId },
                    new ProviderGroup { Name = "Boulder Dog, Inc.", TestFlag = true, Id = UpsertProviderGroup.BoulderDogProviderGroupId });
                
                context.ProviderSet.AddOrUpdate(new Provider() { Key = "hula-dog-101", Name = "Hula Dog (101)", PhoneNumber = "1-800-123-1234", ProviderGroupId = UpsertProviderGroup.HulaDogProviderGroupId, TimezoneInfoId = Timezone.Pacific, Id = UpsertProvider.HulaDog101ProviderId },
                    new Provider() { Key = "hula-dog-102", Name = "Hula Dog (102)", PhoneNumber = "1-800-123-1235", ProviderGroupId = UpsertProviderGroup.HulaDogProviderGroupId, TimezoneInfoId = Timezone.Pacific, Id = UpsertProvider.HulaDog102ProviderId },
                    new Provider() { Key = "boulder-dog", Name = "Boulder Dog", PhoneNumber = "1-800-456-7890", ProviderGroupId = UpsertProviderGroup.BoulderDogProviderGroupId, TimezoneInfoId = Timezone.Pacific, Id = UpsertProvider.BoulderDogProviderId });

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void EmployeesSetup()
        {
            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.EmployeeSet.AddOrUpdate(
                    new Employee { Id = new Guid("{52A165D3-223B-4559-A402-9857F828AA25}"), FirstName = "Kevin", LastName ="Harris", Initials="KH", ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId },
                    new Employee { Id = new Guid("{3FAE2E41-81D5-4C34-BABD-371B24F2F729}"), FirstName = "John", LastName = "Williams", Initials = "JW2", ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId },
                    new Employee { Id = new Guid("{CBF50AC6-5FD4-42D2-8440-520F931A0CF1}"), FirstName = "Sarah", LastName = "Garcia", Initials = "SG", ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId });

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void ResourceSetup()
        {
            
        }

        [TestMethod]
        public void TimzoneSetup()
        {
            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.TimezoneSet.AddOrUpdate(
                    new Timezone { Id = Timezone.Pacific, Name = "Pacific", DisplayOrder = 10, TZString= "America/Los_Angeles" },
                    new Timezone { Id = Timezone.Arizona, Name = "Arizona", DisplayOrder = 20, TZString = "America/Phoenix" },
                    new Timezone { Id = Timezone.Mountain, Name = "Mountain", DisplayOrder = 30, TZString = "America/Chicago" },
                    new Timezone { Id = Timezone.Central, Name = "Central", DisplayOrder = 40, TZString = "America/Los_Angeles" },
                    new Timezone { Id = Timezone.Eastern, Name = "Eastern", DisplayOrder = 50, TZString = "America/New_York" }
                    );

                context.SaveChanges();
            }
        }

        
    }
}
