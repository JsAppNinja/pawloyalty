using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Attributes;
using Paw.Services.Messages.Web.Owners;

namespace Paw.Services.Test.Attributes
{
    [TestClass]
    public class AttributeTests
    {
        [TestMethod]
        public void GetPropertyAttributeTest()
        {
            var result = AttributeHelper.GetPropertyAttributeList<IBindControllerValue>(typeof(GetOwnerListByProviderId));

            PropertyInfo propertyInfo = typeof(GetOwnerListByProviderId).GetProperty("ProviderId");

            var list1 = propertyInfo.GetCustomAttributes();
        }
    }
}
