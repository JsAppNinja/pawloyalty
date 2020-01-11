using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Paw.Services.Messages.Web.Searches;

namespace Paw.Services.Test.Performance
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void GetPetListTest()
        {
            long memoryBeforeBytes = GC.GetTotalMemory(true);
            List<PetLink> petLinkList = PetSearchService.GetPetLinkList(new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"));
            long memoryIncreaseBytes = GC.GetTotalMemory(true) - memoryBeforeBytes;

            double mb = (memoryIncreaseBytes / 1024f) / 1024f;

            
        }


        [TestMethod]
        public void GetOwnerListTest()
        {
            long memoryBeforeBytes = GC.GetTotalMemory(true);
            var ownerList = new GetPetOwnerList() { ProviderGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB") }.ExecuteList();
            long memoryIncreaseBytes = GC.GetTotalMemory(true) - memoryBeforeBytes;

            double mb = (memoryIncreaseBytes / 1024f) / 1024f;
        }


        [TestMethod]
        public void SearchTest()
        {
            // var result = SearchService.QueryPetOwner(new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"), 4, "p p");
        }
    }
}
