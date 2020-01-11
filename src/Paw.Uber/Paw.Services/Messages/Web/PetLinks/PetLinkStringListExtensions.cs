using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Web.PetLinks
{
    public static class PetLinkStringListExtensions
    {
        public static Guid GetPetId(this string input)
        {
            return new Guid(input.Tokenize()[GetPetLinkStringList.PetIdIndex]);
        }

        public static Guid GetOwnerId(this string input)
        {
            return new Guid(input.Tokenize()[GetPetLinkStringList.OwnerIdIndex]);
        }

        public static string GetPetName(this string input)
        {
            return input.Tokenize()[GetPetLinkStringList.PetNameIndex];
        }

        public static string GetOwnerFirstName(this string input)
        {
            return input.Tokenize()[GetPetLinkStringList.OwnerFirstNameIndex];
        }

        public static string GetOwnerLastName(this string input)
        {
            return input.Tokenize()[GetPetLinkStringList.OwnerLastNameIndex];
        }

        public static string GetPhoneNumber(this string input)
        {
            return input.Tokenize()[GetPetLinkStringList.PhoneNumberIndex];
        }

        public static string GetBreed(this string input)
        {
            return input.Tokenize()[GetPetLinkStringList.BreedIndex];
        }

        #region JSON ...

        public static string AsJson(this string input)
        {
            return "{" +
                    $"\"Id\":\"{input.GetPetId()}\"," +
                    $"\"OwnerId\":\"{input.GetOwnerId()}\"," +
                    $"\"PetName\":\"{input.GetPetName()}\"," +
                    $"\"OwnerFirstName\":\"{input.GetOwnerFirstName()}\"," +
                    $"\"OwnerLastName\":\"{input.GetOwnerLastName()}\"," +
                    $"\"PhoneNumber\":\"{input.GetPhoneNumber()}\"," +
                    $"\"Breed\":\"{input.GetBreed()}\"," +
                    $"\"FullName\":\"{input.GetOwnerFirstName()} {input.GetOwnerLastName()}\"," +
                    $"\"PetAndOwner\":\"{input.GetPetName()} ({input.GetBreed()}) {input.GetOwnerFirstName()} {input.GetOwnerLastName()}\"" +
                    "}";
        }

        public static string AsJson(this List<string> input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool started = false;

            foreach (string item in input)
            {
                if (started)
                {
                    stringBuilder.Append(",");
                }
                else
                {
                    started = true;
                }

                stringBuilder.Append(item.AsJson());
            }

            return $"{{\"Data\":[{stringBuilder.ToString()}]}}";
        }

        #endregion

        public static DateTime? GetDOB(this string input)
        {
            DateTime result;
            if (DateTime.TryParse(input.Tokenize()[GetPetLinkStringList.DOBIndex], out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static List<string> Tokenize(this string input, string delimiter = "|")
        {
            return input.Split(new string[] { delimiter }, StringSplitOptions.None).ToList();
        }

    }
}
