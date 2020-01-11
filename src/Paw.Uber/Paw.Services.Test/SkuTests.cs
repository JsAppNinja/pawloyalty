using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Skus;

namespace Paw.Services.Test
{
    [TestClass]
    public class SkuTests
    {
        [TestMethod]
        public void GetSkuListTest()
        {
            Guid providerId = new Guid("1543dd05-83d2-484d-9d59-16278995d4f1");
            Guid? skuCategoryId = new Guid("3abcd072-3a2e-4398-8e94-35a6811c1787");
            var list = new GetSkuList() { ProviderId = providerId, SkuCategoryId = skuCategoryId }.ExecuteList();
        }

        [TestMethod]
        public void GetSkuGroupSkuListTest()
        {
            Guid providerId = new Guid("1543dd05-83d2-484d-9d59-16278995d4f1");
            Guid skuId = new Guid("3CC9A451-336B-4559-9B53-A988002834F4");
            var list = new GetSkuGroupListBySkuId() { ProviderId = providerId, SkuId = skuId, Type = 1 }.ExecuteList();
        }

        

        [TestMethod]
        public void GetPetTest()
        {
            Guid id = new Guid("{caf0da2f-8e29-48fc-b921-1cd93c6835bb}");
            Guid providerGroupId = new Guid("{284a43dd-f676-41e4-a713-ebc848cf85db}");
            var result = new GetPet() { Id = id, ProviderGroupId = providerGroupId }.ExecuteItem();
        }
    }
}
