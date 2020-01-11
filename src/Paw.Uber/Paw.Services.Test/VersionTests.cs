using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Util;
using Paw.Services.Common;

namespace Paw.Services.Test
{
    [TestClass]
    public class VersionTests
    {
        [TestMethod]
        public void TriggerTest()
        {
            string s = VersionHelper.UpdateTriggerSql(typeof(SkuVersion));
        }
    }
}
