using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;

namespace Paw.Services.Test
{
    [TestClass]
    public class DateTest
    {
        [TestMethod]
        public void DOBTest()
        {
            string s0 = Pet.GetDOB(DateTime.Now.AddDays(-12));
            string s1 = Pet.GetDOB(DateTime.Now.AddDays(-90));
            string s2 = Pet.GetDOB(DateTime.Now.AddDays(-400));
            string s3 = Pet.GetDOB(DateTime.Now.AddDays(-700));
            string s4 = Pet.GetDOB(DateTime.Now.AddDays(-730));
            string s5 = Pet.GetDOB(DateTime.Now.AddDays(-732));
            string s6 = Pet.GetDOB(DateTime.Now.AddDays(-734));
            string s7 = Pet.GetDOB(DateTime.Now.AddDays(-736));
        }
    }
}
