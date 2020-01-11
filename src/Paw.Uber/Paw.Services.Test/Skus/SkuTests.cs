using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Messages.Web.Skus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test.Skus
{
    [TestClass]
    public class SkuTests
    {
        [TestMethod]
        public void GetExtrasSkuTest()
        {
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1");
            Guid skuId = new Guid("3CC9A451-336B-4559-9B53-A988002834F4");

            List<Common.Sku> skuList = new GetRelatedSkuList() { ProviderId = providerId, SkuId = skuId, Type = 1 }.ExecuteList();
        }
    }
}
