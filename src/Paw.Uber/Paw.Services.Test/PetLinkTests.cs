using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Messages.Web.PetLinks;

namespace Paw.Services.Test
{
    /// <summary>
    /// Summary description for PetLinkTests
    /// </summary>
    [TestClass]
    public class PetLinkTests
    {
        public PetLinkTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetPetLinkListTest()
        {
            Guid wagProviderGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB");
            Guid dogtopiaProviderGroupId = new Guid("4E234298-2477-4E6C-865C-A12F0C72DED9");
            Guid nvaProviderGroupId = new Guid("{9B5FBC0E-CCD7-45BC-9B87-E58EB1009742}");

            var getPetLinkList = new GetPetLinkStringList()
            {
                Query = "St S",
                ProviderGroupId = dogtopiaProviderGroupId }.ExecuteScalar();
        }
    }
}
