using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Web;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Payments;
using Paw.Services.Messages.Web.PetReservations;
using Paw.Services.Messages.Web.Reservations;
using Paw.Services.Messages.Web.ScheduleRules;
using Paw.Services.Messages.Web.Skus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationGroup = Paw.Services.Common.ReservationGroup;

namespace Paw.Services.Test.Reservation
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void SearchTest()
        {
            // var result = SearchService.QueryPetOwner(new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"), 4, "p p");
        }

        [TestMethod]
        public void ScheduleRuleSetupTest()
        {
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1");
            Guid employeeId = new Guid("712E629D-91F1-4990-96B4-4A5CD81C8588"); //Elizabeth Smith

            //new AddScheduleRule() { Id = new Guid("{CB830497-1666-4193-AF9C-E2B83B90E2FD}"),
            //    Capacity = 2,
            //    Duration = 240,
            //    EmployeeId = employeeId,
            //    ProviderId = providerId,
            //    Monday = true,
            //    Tuesday = true,
            //    Wednesday = true,
            //    Thursday = true,
            //    Friday = true,
            //    RuleStartDate = new DateTime(2018, 1, 1),
            //    RuleEndDate = null,
            //    SkuCategoryId = null,
            //    StartTime = DateTime.Now.Date.AddHours(9)
            //}.ExecuteNonQuery();

            new AddScheduleRule()
            {
                Id = new Guid("{1A479403-4062-47E3-97A6-720CBCE28297}"),
                Capacity = 2,
                Duration = 240,
                EmployeeId = employeeId,
                ProviderId = providerId,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                RuleStartDate = new DateTime(2018, 1, 1),
                RuleEndDate = null,
                SkuCategoryId = null,
                StartTime = DateTime.Now.Date.AddHours(13)
            }.ExecuteNonQuery();
        }

        [TestMethod]
        public void GroomingAppointmentSmokeTest()
        {
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1");
            DateTime appointmentDate = new DateTime(2019, 1, 7);

            Guid petId1 = new Guid("9de67d64-a67f-43b2-baed-9ca2981e50da");
            Guid petId2 = new Guid("3b796cbf-756d-4cc1-9e94-da4abe68c70c");
            Guid ownerId = new Guid("F5869C1B-90D0-4678-8F0F-AC9436AE41ED");
            
            // Step 1. Get Nav
            List<SkuCategory> skuCategoryList = new GetSkuCatetoryList() { ProviderId = providerId, PrimaryOnly = true, ShowDeleted = false }.ExecuteList();

            // Step 2. Choose 'Grooming' item
            SkuCategory skuCategory = skuCategoryList.Find(x => x.Name == "Grooming");

            // Step 3. Get Primary Sku's for 'Grooming'
            List<Sku> primarySkuList = new GetPrimarySkuList() { ProviderId = providerId, SkuCategoryId = skuCategory.Id }.ExecuteList();

            // Step 4. Choose 'Complete Groom-XL' item
            Sku sku = primarySkuList.Find(x => x.Name == "Complete Groom-XL");

            // Step 5.  Get Related Sku list
            List<Sku> relatedSkuList = new GetRelatedSkuList() { ProviderId = providerId, SkuId = sku.Id, Type = 1 }.ExecuteList();

            // Step 6. Choose Related Sku's
            Sku extra1 = relatedSkuList.Find(x => x.Name == "FURminator Upgrade");
            Sku extra2 = relatedSkuList.Find(x => x.Name == "De-Matting");
            Sku extra3 = relatedSkuList.Find(x => x.Name == "Coconult Oil Silk Treatment");

            // Step 7. Get Employee List for Schedule Filter by Employee
            List<EmployeeInfo> employeeInfoList = new GetEmployeeInfoList() { ProviderId = providerId, IncludeTerminated = false }.ExecuteList();

            // Step 8. Choose Employee 'Elizabeth  Smith'
            EmployeeInfo employeeInfo = employeeInfoList.Find(x => x.FirstName.Trim() == "Elizabeth" && x.LastName.Trim() == "Smith");

            // Step 9. Get Rule List
            List<ScheduleRule> scheduleRuleList = new GetScheduleRuleList() { ProviderId = providerId }.ExecuteList();

            // Step 10. Get Filtered Rule List
            List<ScheduleRule> filteredRuleList = new GetFilteredScheduleRuleList() { ProviderId = providerId, EmployeeIdFilterList = new List<Guid>() { employeeInfo.Id } }.ExecuteList();

            // Step 11. Get available days
            foreach (ScheduleRule scheduleRule in filteredRuleList)
            {
                for (int i = 0; i < 30; i++)
                {
                    // IsValid Date
                    bool isValidDate = scheduleRule.ValidDate(DateTime.Now.Date.AddDays(i));
                    
                }
            }

            // Step 12. Get StartTimes for given date
            List<StartTime> startTimeList = filteredRuleList.GetStartTimeList(appointmentDate);

            // Step 13. Choose first start time
            StartTime startTime = startTimeList.First();

            // Step 14. Choose Rule
            ScheduleRule selectedRule = filteredRuleList.Find(x => x.Id == startTime.Id);

            // Step 15. Add Reservation
            AddReservation addReservation = new AddReservation() { Id = Guid.NewGuid(),
                OwnerId = ownerId,
                ProviderId = providerId,
                SkuCategoryId = skuCategory.Id
            };

            // Step 16. Add Grooming Pet Reservation
            SkuLine skuLinePrimaryPet1 = new SkuLine()
            {
                ReservationId = addReservation.Id,
                ProviderId = providerId,
                ParentId = null, // Primary 
                PetId = petId1, // Required for primary

                // Sku Memo
                SkuId = sku.Id,
                Amount = sku.Amount,
                Description = sku.Description,
                Name = sku.Name,
                Quantity = 1,

                // Schedule Rule/Block Memo ...
                ScheduleRuleId = selectedRule.Id,
                StartDate = startTime.Time.Date,
                StartTime = startTime.Time
            };

            // Step 17. Add Pet1 Extra1
            SkuLine skuLineExtra1Pet1 = new SkuLine()
            {
                ReservationId = addReservation.Id,
                ProviderId = providerId,
                ParentId = skuLinePrimaryPet1.Id, // Primary 
                PetId = null, // Required for primary

                // Sku Memo
                SkuId = extra1.Id,
                Amount = null,
                Description = extra1.Description,
                Name = extra1.Name,
                Quantity = 1
            };

            // Step 18. Add Pet1 Extra2
            SkuLine skuLineExtra2Pet1 = new SkuLine()
            {
                ReservationId = addReservation.Id,
                ProviderId = providerId,
                ParentId = skuLinePrimaryPet1.Id, // Primary 
                PetId = null, // Required for primary

                // Sku Memo
                SkuId = extra2.Id,
                Amount = null,
                Description = extra2.Description,
                Name = extra2.Name,
                Quantity = 1
            };


            // Step 19. Add Grooming Pet Reservation 2
            SkuLine skuLinePrimaryPet2 = new SkuLine()
            {
                ReservationId = addReservation.Id,
                ProviderId = providerId,
                ParentId = null, // Primary 
                PetId = petId1, // Required for primary

                // Sku Memo
                SkuId = sku.Id,
                Amount = sku.Amount,
                Description = sku.Description,
                Name = sku.Name,
                Quantity = 1,

                // Schedule Rule/Block Memo ...
                ScheduleRuleId = selectedRule.Id,
                StartDate = startTime.Time.Date,
                StartTime = startTime.Time
            };

            // Step 20. Add Pet1 Extra1
            SkuLine skuLineExtra1Pet2 = new SkuLine()
            {
                ReservationId = addReservation.Id,
                ProviderId = providerId,
                ParentId = skuLinePrimaryPet2.Id, // Primary 
                PetId = null, // Required for primary

                // Sku Memo
                SkuId = extra1.Id,
                Amount = null,
                Description = extra1.Description,
                Name = extra1.Name,
                Quantity = 1
            };

            // Step 21. Add Pet1 Extra2
            SkuLine skuLineExtra2Pet2 = new SkuLine()
            {
                ReservationId = addReservation.Id,
                ProviderId = providerId,
                ParentId = skuLinePrimaryPet2.Id, // Primary 
                PetId = null, // Required for primary

                // Sku Memo
                SkuId = extra2.Id,
                Amount = null,
                Description = extra2.Description,
                Name = extra2.Name,
                Quantity = 1
            };

            // Step 23. Get CCToken (add option for swipe, nickname)
            //CCToken ccToken = new GetCCToken(){ OwnerId = ownerId }.ExecuteItem();

            //// Step 22. Send Payment (Amount == null means pay in full)
            //var addCCPaymentResult1 = new AddCCPayment() { Amount = null, ReservationId = addReservation.Id, CCToken = ccToken.Value }.ExecuteNonQuery();

            //// Step 23. Cancel Extra
            //new CancelSkuLine() { ReservationId = addReservation.Id, SkuLineId = skuLineExtra1Pet2.Id }.ExecuteNonQuery();

            //// Step 23. Send Payment (will Negative Value (Partial Refund))
            //var addCCPaymentResult2 = new AddCCPayment() { Amount = null, ReservationId = addReservation.Id, CCToken = ccToken.Value }.ExecuteNonQuery();

            //// Step 24. Add Extra
            //// new AddSkuLine() { ReservationId = addReservation.Id, SkuLineId = skuLineExtra1Pet2.Id }.ExecuteNonQuery();

            //// Step 25. Send Payment 
            //var addCCPaymentResult3 = new AddCCPayment() { Amount = null, ReservationId = addReservation.Id, CCToken = ccToken.Value }.ExecuteNonQuery();

            //// Step 26. Reschedule Pet1
            //new RescheduleBlock() { SkuLineId = new List<Guid>(){ skuLinePrimaryPet1.Id,skuLinePrimaryPet2.Id},
            //    ScheduleRuleId = selectedRule.Id,
            //    StartDate = startTime.Date.AddDays(1),
            //    StartTime = startTime.AddDays.addReservation(1) }.ExecuteItem();
        

            //// Step 27. Cancel Reservation
            //new CancelReservation() { ReservationId = addReservation.Id }.ExecuteItem();


            //// Step 28. Send Payment (Full Refund)
            //var addCCPaymentResult3 = new AddCCPayment() { Amount = null, ReservationId = addReservation.Id, CCToken = ccToken.Value }.ExecuteNonQuery();
            
        }

        [TestMethod]
        public void BoardingReservationSmokeTest()
        {
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1");
            DateTime appointmentDate = new DateTime(2019, 1, 7);

            Guid petId1 = new Guid("9de67d64-a67f-43b2-baed-9ca2981e50da");
            Guid petId2 = new Guid("3b796cbf-756d-4cc1-9e94-da4abe68c70c");
            Guid ownerId = new Guid("F5869C1B-90D0-4678-8F0F-AC9436AE41ED");

            // Step 1. Get Nav
            List<SkuCategory> skuCategoryList = new GetSkuCatetoryList() { ProviderId = providerId, PrimaryOnly = true, ShowDeleted = false }.ExecuteList();

            // Step 2. Choose 'Grooming' item
            SkuCategory skuCategory = skuCategoryList.Find(x => x.Name == "Grooming");

            // Step 3. Get Primary Sku's for 'Grooming'
            List<Sku> primarySkuList = new GetPrimarySkuList() { ProviderId = providerId, SkuCategoryId = skuCategory.Id }.ExecuteList();

        }
    }
}
