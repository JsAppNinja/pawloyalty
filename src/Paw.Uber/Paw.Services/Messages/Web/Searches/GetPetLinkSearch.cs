using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.ProviderGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Paw.Services.Messages.Web.Searches
{
    [Obsolete]
    public  class GetPetLinkSearch
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

        public List<PetOwnerLink> ExecuteList(bool useCache = true)
        {
            List<PetOwnerLink> result = new List<PetOwnerLink>();

            if (string.IsNullOrEmpty(this.Query))
            {
                return result;
            }

            // Step 1. Get provider group
            ProviderGroup providerGroup = new GetProviderGroup() { Id = this.ProviderGroupId }.ExecuteItem(useCache);
            if (providerGroup == null || providerGroup.OwnerCollection.Count == 0)
            {
                return result;
            }

            // Step 2. Tokenize query
            var tokenList = this.Query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (tokenList.Count == 0)
            {
                return result;
            }


            // Step 3. Query
            providerGroup.OwnerCollection.ToList().ForEach(o =>
            {
                if (o.PetCollection == null || o.PetCollection.Count == 0) // Owners without a pet
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
                else
                {
                    o.PetCollection.ToList().ForEach(p =>
                    {
                        PetOwnerLink petOwnerLink = new PetOwnerLink() { Id = p.Id, Pet = p.Name, OwnerId = o.Id, FirstName = o.FirstName, LastName = o.LastName, Breed = "Unknown" };

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
                        else if (this.Query.All(char.IsNumber))
                        {
                            if (this.Query.Length == 4 && p.Owner.PhoneNumber.EndsWith(this.Query))
                            {
                                firstTokenScore = firstTokenScore + 8;
                            }
                            else if (this.Query.Length == 5 && p.Owner.PhoneNumber.EndsWith(this.Query))
                            {
                                firstTokenScore = firstTokenScore + 8;
                            }
                            else if (this.Query.Length == 7 && p.Owner.PhoneNumber.EndsWith(this.Query))
                            {
                                firstTokenScore = firstTokenScore + 10;
                            }
                            else if (this.Query.Length == 10 && p.Owner.PhoneNumber.StartsWith(this.Query))
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

                }
            });
      
            // Step 4. Sort
            result.Sort((x, y) =>
            {
                int score = y.Score.CompareTo(x.Score);
                if (score != 0) return score;

                int owner = x.LastFirst.CompareTo(y.LastFirst);
                if (score != 0) return score;

                return x.Pet.CompareTo(y.Pet);
            });

            return result;
        }

    }
}
