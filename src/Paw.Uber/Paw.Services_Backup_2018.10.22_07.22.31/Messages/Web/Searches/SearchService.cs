using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Paw.Services.Messages.Web.Searches
{
    [Obsolete]
    public class SearchService
    {
        public static int GetPetLinkListExpirationInSeconds()
        {
            return 40;
        }

        public static int GetActiveProviderGroupSlidingExpirationInMinutes()
        {
            return 10;
        }

        #region Cache Keys ....

        public static string GetPetOwnerListCacheKey(Guid providerGroupId)
        {
            return string.Format("PetOwnerList_{0}", providerGroupId);
        }

        public static string GetActiveProviderGroupCacheKey(Guid providerGroupId)
        {
            return string.Format("ActiveProviderGroup_{0}", providerGroupId);
        }

        public static Guid? GetPetOwnerListCacheKeyProviderGroupId(string input)
        {
            var s = input.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            if (s.Length < 2) return null;

            Guid result;
            if (Guid.TryParse(s[1], out result))
            {
                return result;
            }

            return (Guid?)null;
        }

        #endregion

        #region Get List and Cache Insert ...

        private static PetOwnerList SeedCache(Guid providerGroupId)
        {
            return GetPetOwnerList(providerGroupId);
        }

        public static PetOwnerList GetPetOwnerList(Guid providerGroupId)
        {
            PetOwnerList result = null;

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            // Step 2. Get cache object
            if (httpContext != null)
            {
                // Mark Active
                httpContext.Cache.Add(GetActiveProviderGroupCacheKey(providerGroupId), true, null, Cache.NoAbsoluteExpiration, new TimeSpan(GetActiveProviderGroupSlidingExpirationInMinutes()), CacheItemPriority.Default, null);

                result = httpContext.Cache[GetPetOwnerListCacheKey(providerGroupId)] as PetOwnerList;
                if (result != null)
                {
                    return result;
                }
            }

            // TODO: Log the CACHE MISS


            // Step 3. Get PetList if necessary
            return GetPetOwnerListInner(providerGroupId);
            
        }

        private static PetOwnerList GetPetOwnerListInner(Guid providerGroupId)
        {
            // Step 1. Get List
            PetOwnerList result = new GetPetOwnerList() { ProviderGroupId = providerGroupId }.ExecuteList();

            // Step 2. Check for the context
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                // Active Dependency
                CacheDependency cacheDependency = new CacheDependency(null, new string[] { GetActiveProviderGroupCacheKey(providerGroupId) });

                httpContext.Cache.Insert(GetPetOwnerListCacheKey(providerGroupId), result, null, DateTime.Now.AddSeconds(GetPetLinkListExpirationInSeconds()), Cache.NoSlidingExpiration, PetOwnerListCacheItemUpdateCallbackMethod);
            }

            return result;
        }

        public static void PetOwnerListCacheItemUpdateCallbackMethod(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            // Step 0.
            expensiveObject = null;
            dependency = null;
            absoluteExpiration = DateTime.Now.AddSeconds(GetPetLinkListExpirationInSeconds());
            slidingExpiration = Cache.NoSlidingExpiration;

            if (DateTime.Now.Minute % 10 == 0)
            {
                return; // do nothing
            }

            // Step 1. Parse Key
            Guid? providerGroupId = GetPetOwnerListCacheKeyProviderGroupId(key);
            if (providerGroupId != null)
            {
                expensiveObject = new GetPetOwnerList() { ProviderGroupId = providerGroupId.Value }.ExecuteList();
            }
        }

        #endregion

        #region Searches ...

        public static List<OwnerLink> QueryOwner(Guid providerGroupId, string query)
        {
            List<OwnerLink> result = new List<OwnerLink>();

            // Get the list
            PetOwnerList petOwnerList = GetPetOwnerList(providerGroupId);

            // tokenize
            var tokenList = query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (tokenList.Count == 0) return result;

            petOwnerList.OwnerList.ForEach(x =>
            {
                OwnerLink ownerLink = new OwnerLink() { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName };

                if (x.FirstName.StartsWith(tokenList.First(), StringComparison.InvariantCultureIgnoreCase))
                {
                    ownerLink.Score = ownerLink.Score + 10;
                }

                if (x.LastName.StartsWith(tokenList.Last(), StringComparison.InvariantCultureIgnoreCase))
                {
                    ownerLink.Score = ownerLink.Score + 12;
                }

                if (ownerLink.Score > 0)
                {
                    result.Add(ownerLink);
                }
            });

            result.Sort((x,y) =>
            {
                int score = y.Score.CompareTo(x.Score);
                if (score != 0) return score;

                int lastName = x.LastName.CompareTo(y.LastName);
                if (lastName != 0) return lastName;

                return x.FirstName.CompareTo(y.FirstName);
            });

            return result;
        }

        public static List<PetOwnerLink> QueryPetOwner(Guid providerGroupId, string query)
        {
            List<PetOwnerLink> result = new List<PetOwnerLink>();

            // Get the list
            PetOwnerList petOwnerList = GetPetOwnerList(providerGroupId);

            // tokenize
            var tokenList = query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (tokenList.Count == 0) return result;

            // query
            petOwnerList.OwnerList.ForEach(o =>
            {
                if (o.PetList.Count == 0) // Owners without a pet
                {
                    PetOwnerLink petOwnerLink = new PetOwnerLink() { Id = null, Pet = null, OwnerId = o.Id, FirstName = o.FirstName, LastName = o.LastName };


                    if (o.FirstName.StartsWith(tokenList.First(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        petOwnerLink.Score = petOwnerLink.Score + 10;
                    }

                    if (o.LastName.StartsWith(tokenList.Last(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        petOwnerLink.Score = petOwnerLink.Score + 12;
                    }

                    if (petOwnerLink.Score > 0)
                    {
                        // TODO: figure out how to re-enable 

                        //result.Add(petOwnerLink);
                    }
                }

                o.PetList.ForEach(p =>
                {
                    PetOwnerLink petOwnerLink = new PetOwnerLink() { Id = p.Id, Pet = p.Name, OwnerId = o.Id, FirstName = o.FirstName, LastName = o.LastName, Breed = "Unknown"};
                    
                    // first token score
                    int firstTokenScore = 0;
                    if (o.FirstName.StartsWith(tokenList.First(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        firstTokenScore = firstTokenScore + 8;
                    }
                    else if (o.LastName.StartsWith(tokenList.First(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        firstTokenScore = firstTokenScore + 12;
                    }
                    else if (p.Name.StartsWith(tokenList.First(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        firstTokenScore = firstTokenScore + 10;
                    }
                    else if (query.All(char.IsNumber))
                    {
                        if (query.Length == 4 && p.Owner.PhoneNumber.EndsWith(query))
                        {
                            firstTokenScore = firstTokenScore + 8;
                        }
                        else if (query.Length == 5 && p.Owner.PhoneNumber.EndsWith(query))
                        {
                            firstTokenScore = firstTokenScore + 8;
                        }
                        else if (query.Length == 7 && p.Owner.PhoneNumber.EndsWith(query))
                        {
                            firstTokenScore = firstTokenScore + 10;
                        }
                        else if (query.Length == 10 && p.Owner.PhoneNumber.StartsWith(query))
                        {
                            firstTokenScore = firstTokenScore + 12;
                        }
                    }
                    
                    int lastTokenScore = 0;
                    if (o.LastName.StartsWith(tokenList.Last(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        lastTokenScore = lastTokenScore + 12;
                    }

                    if (tokenList.Count == 1)
                    {
                        if (firstTokenScore > 0)
                        {
                            petOwnerLink.Score = firstTokenScore;
                            result.Add(petOwnerLink);
                        }
                    }
                    else
                    {
                        if (firstTokenScore > 0 && lastTokenScore > 0)
                        {
                            petOwnerLink.Score = firstTokenScore + lastTokenScore;
                            result.Add(petOwnerLink);
                        }
                    }
                }
                );
            });

            result.Sort((x, y) =>
            {
                int score = y.Score.CompareTo(x.Score);
                if (score != 0) return score;
                
                int owner =  x.LastFirst.CompareTo(y.LastFirst);
                if (score != 0) return score;

                return x.Pet.CompareTo(y.Pet);
            });

            return result;
        }

        #endregion
    }
}
