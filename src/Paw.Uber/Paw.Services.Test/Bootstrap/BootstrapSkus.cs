using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Paw.Services.Test.Bootstrap
{
    [TestClass]
    public class BootstrapSkus
    {
        [TestMethod]
        public void SkusSetup()
        {
            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.Database.Log = Log;

                context.SkuCategorySet.AddOrUpdate(

                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_Groom1_CategoryId,
                    Name = "Groom",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder = 30,
                    SchedulerTypeId = SchedulerType.Block,
                    NavIcon = "icon-calendar"
                },
                
                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_Groom2_CategoryId,
                    Name = "Pro Groom",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder = 40,
                    SchedulerTypeId = SchedulerType.Appointment,
                    NavIcon = "icon-clock"
                },

                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_DayCare_CategoryId,
                    Name = "Day Care",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder = 20,
                    SchedulerTypeId = SchedulerType.Resource,
                    NavIcon = "icon-calendar"
                },

                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_Boarding_CategoryId,
                    Name = "Boarding",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder= 10,
                    SchedulerTypeId = SchedulerType.Resource,
                    NavIcon = "icon-clock"
                },

                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_Training1_CategoryId,
                    Name = "Training",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder = 50,
                    SchedulerTypeId = SchedulerType.Block,
                    NavIcon = "icon-clock"
                },

                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_Training2_CategoryId,
                    Name = "Pro Training",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder = 60,
                    SchedulerTypeId = SchedulerType.Appointment,
                    NavIcon = "icon-clock"
                },

                new SkuCategory()
                {
                    Id = ProviderTestData.Wag_RedwoodCity_Tour_CategoryId,
                    Name = "Tour",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    NavDisplayOrder = 70,
                    SchedulerTypeId = SchedulerType.Block,
                    NavIcon = "icon-check"
                }
                );

                context.SkuSet.AddOrUpdate(new Sku()
                {
                    Id = ProviderTestData.Wag_FullGroom1_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Groom1_CategoryId,
                    Amount = 119.99M,
                    Description = "Full Groom",
                    Name = "Full Groom",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                },

                new Sku()
                {
                    Id = ProviderTestData.Wag_FullGroom2_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Groom2_CategoryId,
                    Amount = 119.99M,
                    Description = "Full Groom",
                    Name = "Full Groom",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                },

                new Sku()
                {
                    Id = ProviderTestData.Wag_DayCare_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_DayCare_CategoryId,
                    Amount = 119.99M,
                    Description = "Day Care",
                    Name = "Day Care",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                },

                new Sku()
                {
                    Id = ProviderTestData.Wag_Boarding_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Boarding_CategoryId,
                    Amount = 119.99M,
                    Description = "Boarding",
                    Name = "Boarding",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                },

                new Sku()
                {
                    Id = ProviderTestData.Wag_Training1_L1_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Training1_CategoryId,
                    Amount = 99.00M,
                    Description = "Training L1",
                    Name = "Training L1",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                },

                new Sku()
                {
                    Id = ProviderTestData.Wag_Training1_L2_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Training1_CategoryId,
                    Amount = 99.00M,
                    Description = "Training L2",
                    Name = "Training L2",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                },

                new Sku()
                {
                    Id = ProviderTestData.Wag_Training2_L1_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Training2_CategoryId,
                    Amount = 199.99M,
                    Description = "Pro Training L1",
                    Name = "Pro Training L1",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                });

                context.SaveChanges();
            }
        }

            private void Log(string message)
        {
            Trace.TraceInformation(message);
        }

        #region Setup Skus ...

        // based on wagrwc - '1543DD05-83D2-484D-9D59-16278995D4F1'

        public void AddPrimarySkus(DataContext context)
        {

        }

        public void AddTour(DataContext context)
        {
            context.SkuSet.AddOrUpdate(
                new Sku()
                {
                    Id = ProviderTestData.Wag_Tour_SkuId,
                    SkuCategoryId = ProviderTestData.Wag_RedwoodCity_Tour_CategoryId,
                    Amount = 199.99M,
                    Description = "30 minutes customer interview and tour",
                    Name = "New Customer Interview & Tour",
                    ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId,
                    ParentId = null
                });

            context.SaveChanges();
        }

        #endregion
    }
}
