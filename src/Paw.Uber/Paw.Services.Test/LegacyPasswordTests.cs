using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Messages.Util.Users;

namespace Paw.Services.Test
{
    [TestClass]
    public class LegacyPasswordTests
    {
        [TestMethod]
        public void LegacyPasswordTest()
        {
            int result = new ImportLogin() { EmailAddress = "garrettadlock@gmail.com" }.Execute();
        }
    }
}
