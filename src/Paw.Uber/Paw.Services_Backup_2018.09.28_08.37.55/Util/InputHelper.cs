using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Util
{
    public static class InputHelper
    {
        public const string MonthYearRegEx = @"^(1[0-2]|0[1-9]|\d)\/(20\d{2}|19\d{2}|0(?!0)\d|[1-9]\d)$";

        public static DateTime? ParseMonthYear(string input)
        {
            // Test is empty
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            // 
            string[] s = input.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            // Test length
            if (s.Length != 2)
            {
                return null;
            }

            // Get the month
            int month;
            if (!int.TryParse(s[0], out month) || (month < 0 || month > 12))
            {
                return null;
            }

            // Get the year
            int year;
            if (!int.TryParse(s[1], out year))
            {
                return null;
            }
            else
            {
                if (s[1].Length == 2)
                {
                    year = year + 2000;
                }
            }

            return new DateTime(year, month, 1);
        }
    }
}
