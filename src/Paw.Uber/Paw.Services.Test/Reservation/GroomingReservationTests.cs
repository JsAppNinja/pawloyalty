using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Web.Employees;
using Paw.Services.Messages.Web.Res;
using Paw.Services.Messages.Web.ScheduleRules;
using Paw.Services.Messages.Web.Skus;

namespace Paw.Services.Test.Reservation
{
    [TestClass]
    public class GroomingReservationTests
    {

        Guid _ProviderId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1");
        Guid _EmployeeId = new Guid("712E629D-91F1-4990-96B4-4A5CD81C8588"); //Elizabeth Smith

        [TestMethod]
        public void AddGroomingScheduleRuleTest()
        {
            Guid id = new Guid("8A060279-9F0A-4D76-AA03-DB8D7A44A167");

            // Step 1. Add 9AM Schedule Rule
            int result = new AddScheduleRule()
            {
                Id = id,
                Capacity = 2,
                Duration = 240,
                EmployeeId = _EmployeeId,
                ProviderId = _ProviderId,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                RuleStartDate = new DateTime(2018, 1, 1),
                RuleEndDate = null,
                SkuCategoryId = null,
                StartTime = DateTime.Now.Date.AddHours(9)
            }.ExecuteNonQuery();

            
        }

        [TestMethod]
        public void GroomingReservationSmokeTest()
        {
            DateTime appointmentDate = new DateTime(2019, 2, 15);
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1"); // WagRWC
            Guid petId1 = new Guid("b5f6fb44-51ad-4635-8244-ec7a9bd19373");
//            Guid petId2 = new Guid("");
            Guid ownerId = new Guid("898c14c2-a865-4710-802a-a4e820319ed3");
            string employeeFirstName = "Elizabeth";
            string employeeLastName = "Smith";
            List<string> addExtraSkuList = new List<string>() { "FURminator Upgrade", "De-Matting", "Coconult Oil Silk Treatment" };

            
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

            List<Sku> extraSkuList = relatedSkuList.FindAll(x => addExtraSkuList.Contains(x.Name));

            // Step 7. Get Employee List for Schedule Filter by Employee
            List<EmployeeInfo> employeeInfoList = new GetEmployeeInfoList() { ProviderId = providerId, IncludeTerminated = false }.ExecuteList();

            // Step 8. Choose Employee (ie. 'Elizabeth  Smith')
            EmployeeInfo employeeInfo = employeeInfoList.Find(x => x.FirstName.Trim().Equals(employeeFirstName, StringComparison.CurrentCultureIgnoreCase) && x.LastName.Trim().Equals(employeeLastName, StringComparison.CurrentCultureIgnoreCase));

            // Step 9. Get Rule List
            List <ScheduleRule> scheduleRuleList = new GetScheduleRuleList() { ProviderId = providerId }.ExecuteList();

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
            AddReservation addReservation = new AddReservation()
            {
                Id = Guid.NewGuid(),
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
            foreach (Sku extraSku in extraSkuList)
            {
                SkuLine extraSkuLine = new SkuLine()
                {
                    ReservationId = addReservation.Id,
                    ProviderId = providerId,
                    ParentId = skuLinePrimaryPet1.Id, // Primary 
                    PetId = null, // Required only for primary

                    // Sku Memo
                    SkuId = extraSku.Id,
                    Amount = extraSku.Amount,
                    Description = extraSku.Description,
                    Name = extraSku.Name,
                    Quantity = 1
                };
            }


            //// Step 19. Add Grooming Pet Reservation 2
            //SkuLine skuLinePrimaryPet2 = new SkuLine()
            //{
            //    ReservationId = addReservation.Id,
            //    ProviderId = providerId,
            //    ParentId = null, // Primary 
            //    PetId = petId1, // Required for primary

            //    // Sku Memo
            //    SkuId = sku.Id,
            //    Amount = sku.Amount,
            //    Description = sku.Description,
            //    Name = sku.Name,
            //    Quantity = 1,

            //    // Schedule Rule/Block Memo ...
            //    ScheduleRuleId = selectedRule.Id,
            //    StartDate = startTime.Time.Date,
            //    StartTime = startTime.Time
            //};

            //// Step 20. Add Pet1 Extra1
            //SkuLine skuLineExtra1Pet2 = new SkuLine()
            //{
            //    ReservationId = addReservation.Id,
            //    ProviderId = providerId,
            //    ParentId = skuLinePrimaryPet2.Id, // Primary 
            //    PetId = null, // Required for primary

            //    // Sku Memo
            //    SkuId = extra1.Id,
            //    Amount = null,
            //    Description = extra1.Description,
            //    Name = extra1.Name,
            //    Quantity = 1
            //};

            //// Step 21. Add Pet1 Extra2
            //SkuLine skuLineExtra2Pet2 = new SkuLine()
            //{
            //    ReservationId = addReservation.Id,
            //    ProviderId = providerId,
            //    ParentId = skuLinePrimaryPet2.Id, // Primary 
            //    PetId = null, // Required for primary

            //    // Sku Memo
            //    SkuId = extra2.Id,
            //    Amount = null,
            //    Description = extra2.Description,
            //    Name = extra2.Name,
            //    Quantity = 1
            //};


            ////Step 23.Get CCToken(add option for swipe, nickname)
            //    CCToken ccToken = new GetCCToken() { OwnerId = ownerId }.ExecuteItem();

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
            //new RescheduleBlock()
            //{
            //    SkuLineId = new List<Guid>() { skuLinePrimaryPet1.Id, skuLinePrimaryPet2.Id },
            //    ScheduleRuleId = selectedRule.Id,
            //    StartDate = startTime.Date.AddDays(1),
            //    StartTime = startTime.AddDays.addReservation(1)
            //}.ExecuteItem();


            //// Step 27. Cancel Reservation
            //new CancelReservation() { ReservationId = addReservation.Id }.ExecuteItem();


            //// Step 28. Send Payment (Full Refund)
            //var addCCPaymentResult3 = new AddCCPayment() { Amount = null, ReservationId = addReservation.Id, CCToken = ccToken.Value }.ExecuteNonQuery();

        }
    }
}
