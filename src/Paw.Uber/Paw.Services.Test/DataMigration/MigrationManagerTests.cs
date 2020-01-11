using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test.DataMigration
{
    [TestClass]
    public class MigrationManagerTests
    {

        [TestMethod]
        public void UpsertResourceList()
        {
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1"); // Wagrwc
            Guid skuCategoryId = new Guid("6A215246-244E-4D97-A744-A5ABE486DB2B"); // Boarding
            MigrationManagerV2.UpsertResourceList("SourceDb", "DataContext", providerId, skuCategoryId);

        }
    }
}
