using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using Paw.Services.Test.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test.Bootstrap
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void LoadProviderConfigTest()
        {
            string directory = @"C:\Users\dwbanks\Source\Repos\Paw\src\Paw.Uber\Paw.Services.Test\Setup\Templates\";
            Guid providerId = new Guid("{1543DD05-83D2-484D-9D59-16278995D4F1}"); // wagrwc
            Guid userId = BootstrapUser.UserId;

            ConfigLoader.Load<SkuCategory>(userId, providerId, Path.Combine(directory, "0010-SkuCategory.txt"));
            ConfigLoader.Load<Sku>(userId, providerId, Path.Combine(directory, "0020-Sku.txt"));
            ConfigLoader.Load<SkuGroup>(userId, providerId, Path.Combine(directory, "0040-SkuGroup.txt"));
            ConfigLoader.Load<SkuGroupSku>(userId, providerId, Path.Combine(directory, "0050-SkuGroupSku.txt"));

        }

        [TestMethod]
        public void GetExternalIdDirectoryTest()
        {
            var result0 = ConfigLoader.GetExternalIdDirectory("SkuCategory");
        }
    }
}
