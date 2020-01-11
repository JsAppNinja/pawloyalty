using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paw.Services.Util
{
    public static class TextExtensions
    {
        public static string AsString<T>(this IEnumerable<T> @this, Func<T, string> getValue, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool started = false;
            foreach (var item in @this)
            {
                if (started)
                    stringBuilder.Append(separator);
                else
                    started = true;

                stringBuilder.Append(getValue(item));
            }

            return stringBuilder.ToString();
        }

        public static string FirstLetterOrEmpty(this string @this, bool addPeriod = false)
        {
            if (string.IsNullOrWhiteSpace(@this))
                return string.Empty;

            string result = @this.Substring(0, 1);

            if(addPeriod) result = result + ".";

            return result;
        }

        public static List<string> GetList(this string @this, string separator = ",", bool eliminateEmptyValues = true)
        {
            List<string> result = new List<string>();


            result = Regex.Split(@this, separator).ToList();

            if (eliminateEmptyValues)
            {
                result = result.FindAll(x => !string.IsNullOrWhiteSpace(x));
            }

            return result;
        }
    }
}
