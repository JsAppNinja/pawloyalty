using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Messages.Web.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test.Messsages
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void GetEmployeeTest()
        {
            var employeeList = new GetEmployeeInfoList() { ProviderId = new Guid("1543dd05-83d2-484d-9d59-16278995d4f1")  }.ExecuteList();
        }
    }
}
