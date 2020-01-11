using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using Paw.Services.Messages.Web.Searches;
using Paw.Services.Messages.Web.Skus;

namespace Paw.Services.Test
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void SearchSmokeTest()
        {
            Guid providerId = new Guid("1543DD05-83D2-484D-9D59-16278995D4F1");
            Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB");


            var result = new GetPetLinkSearch() { ProviderGroupId = providerGroupId, Query = "jojo" }.ExecuteList(false);
        }

        [TestMethod]
        public void LoadOwnerCacheTest()
        {
            Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB");

            using (DataContext context = DataContext.Create())
            {
                List<Owner> ownerList = context.OwnerSet.Include("PetCollection").Where(x => x.ProviderGroupId == providerGroupId)
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName).ToList();

                List<string> keyList = new List<string>();
                
                string key = string.Empty;
                foreach (Owner owner in ownerList)
                {
                    if (owner.LastName == null || owner.LastName.Trim().Length < 3)
                    {
                        continue;
                    }


                    if (key == string.Empty || !owner.LastName.StartsWith(key, StringComparison.InvariantCultureIgnoreCase))
                    {
                        key = owner.LastName.Trim().Substring(0, 3);
                        keyList.Add(key);
                    }

                }
            }
        }

        [TestMethod]
        public void FastOwnerReadTest()
        {
            List<string> result = new List<string>();
            using (DataContext context = DataContext.Create())
            {
                using (SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "SELECT Id, FirstName, LastName  FROM Owner WHERE ProviderGroupId = '284A43DD-F676-41E4-A713-EBC848CF85DB'";

                        sqlConnection.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (sqlDataReader.Read())
                            {
                                result.Add($"{sqlDataReader.GetGuid(0)}|{sqlDataReader.GetString(1)}|{sqlDataReader.GetString(2)}");
                            }
                        }
                    }
                }
            }

            List<string> result2 = result.FindAll(x => x.IndexOf("|sa", StringComparison.InvariantCultureIgnoreCase) > 0);
        }

        [TestMethod]
        public void LoadPetCacheTest()
        {
            Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB");

            using (DataContext context = DataContext.Create())
            {
                List<Pet> petList = context.PetSet.Where(x => x.ProviderGroupId == providerGroupId).ToList();
            }
        }
    }
}
