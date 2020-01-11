using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paw.Services.Test
{
    /// <summary>
    /// Summary description for CollectionTests
    /// </summary>
    [TestClass]
    public class CollectionTests
    {
        public CollectionTests()
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
        public void DictionaryTest()
        {
            Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
            List<Guid> keylist = new List<Guid>();

            for (int i = 0; i < 1000000; i++)
            {
                Guid key = Guid.NewGuid();
                keylist.Add(key);
                dictionary.Add(key, Guid.NewGuid().ToString("N"));
            }

            foreach (var item in keylist)
            {
                string s = dictionary[item];
            }
        }

        [TestMethod]
        public void ListTest()
        {
            List<Guid> keylist = new List<Guid>();
            List<KeyValuePair<Guid, string>> dictionary = new List<KeyValuePair<Guid, string>>();

            for (int i = 0; i < 10000; i++)
            {
                Guid key = Guid.NewGuid();
                keylist.Add(key);
                dictionary.Add(new KeyValuePair<Guid,string>(key, Guid.NewGuid().ToString("N")));
            }

            foreach (Guid item in keylist)
            {
                var s = dictionary.Find(x => x.Key == item);
            }
        }
    }
}
