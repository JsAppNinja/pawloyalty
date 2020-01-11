using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.PetLinks
{
    public class GetPetLinkStringList
    {
        public Guid ProviderGroupId
        {
            get { return _ProviderGroupId; }
            set { _ProviderGroupId = value; }
        }
        private Guid _ProviderGroupId = Guid.Empty;

        public string Query
        {
            get { return _Query; }
            set { _Query = value; }
        }
        private string _Query = String.Empty;

        public List<string> ExecuteScalar(bool useCache = true)
        {
            #region Cache ...

            List<string> cacheResult = null;
            if (CacheHelper.TryGetResult<List<string>>($"CacheItem_PetLinkStringList_{this.ProviderGroupId}", out cacheResult))
            {
                return this.FilterCacheResult(cacheResult);
            }

            #endregion
            
            bool multiToken = false;
            string queryStart = this.Query;
            string queryEnd = this.Query;
            if (!string.IsNullOrEmpty(this.Query))
            {
                queryStart = this.Query + '%';
                queryEnd = '%' + this.Query;
            }

            List<string> split = this.Query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (split.Count > 1)
            {
                multiToken = true;
                queryStart = split[0] + '%';
                queryEnd = split[1] + '%';
            }

            List<string> result = new List<string>();
            using (DataContext context = DataContext.Create())
            {
                using (SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = @"SELECT 
                            convert(nvarchar(36),p.Id) + '|' +
                            convert(nvarchar(36),o.Id) + '|' +
                            p.Name + '|' +
                            o.FirstName + '|' +
                            o.LastName + '|' +
                            coalesce(o.PhoneNumber, '') + '|' +
                            coalesce(b.Name, '') + '|' +
                            coalesce(convert(nvarchar(10), p.DOB , 112), '')
                            FROM[Pet] p
                                JOIN[Owner] o ON p.OwnerId = o.Id
                                LEFT JOIN[Breed] b ON p.BreedId = b.Id
                                WHERE p.ProviderGroupId = @ProviderGroupId";

                        if (multiToken)
                        {
                            sqlCommand.CommandText = sqlCommand.CommandText +
                                @" AND (@QueryStart = '' OR 
                                    (o.FirstName LIKE @QueryStart
                                        AND o.LastName Like @QueryEnd) OR
                                    (o.FirstName LIKE @QueryEnd
                                        AND o.LastName Like @QueryStart) OR
                                    (p.Name LIKE @QueryStart
                                        AND o.LastName Like @QueryEnd) OR
                                    (p.Name LIKE @QueryEnd
                                        AND o.LastName Like @QueryStart) 
                                )";
                        }
                        else
                        {
                            sqlCommand.CommandText = sqlCommand.CommandText +
                                @" AND (@QueryStart = '' OR 
                                    p.Name LIKE @QueryStart OR
                                    o.FirstName LIKE @QueryStart OR
                                    o.LastName LIKE @QueryStart OR
                                    o.PhoneNumber LIKE @QueryEnd
                                )";
                        }

                        sqlCommand.Parameters.AddWithValue("@ProviderGroupId", this.ProviderGroupId);
                        sqlCommand.Parameters.AddWithValue("@QueryStart", queryStart);
                        sqlCommand.Parameters.AddWithValue("@QueryEnd", queryEnd);

                        sqlConnection.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (sqlDataReader.Read())
                            {
                                result.Add(sqlDataReader.GetString(0));
                            }
                        }
                    }
                }
            }
            return result;
        }

        private List<string> FilterCacheResult(List<string> cacheResult)
        {
            if (string.IsNullOrEmpty(this.Query))
            {
                return cacheResult;
            }

            List<string> split = this.Query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (split.Count > 1)
            {
                return cacheResult.FindAll(x => {

                    return x.Contains($"|{split[0]}") && x.Contains($"|{split[1]}");
                });
            }

            if (this.Query.Any(char.IsDigit))
            {
                return cacheResult.FindAll(x => {
                    return x.EndsWith($"|{this.Query}", StringComparison.CurrentCultureIgnoreCase);
            });
            }

            return cacheResult.FindAll(x => {
                return x.StartsWith($"|{this.Query}", StringComparison.CurrentCultureIgnoreCase); 
            });
        }
        
        public const int PetIdIndex = 0;
        public const int OwnerIdIndex = 1;
        public const int PetNameIndex = 2;
        public const int OwnerFirstNameIndex = 3;
        public const int OwnerLastNameIndex = 4;
        public const int PhoneNumberIndex = 5;
        public const int BreedIndex = 6;
        public const int DOBIndex = 7;
    }
}
