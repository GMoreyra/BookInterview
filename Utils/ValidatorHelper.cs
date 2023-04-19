using System.Text.RegularExpressions;

namespace Utils
{
    public static class ValidatorHelper
    {
        public static bool ValidateString(string? input)
        {
            if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                return true;
            }

            return false;
        }

        public static bool ValidateInteger(string? input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return int.TryParse(input, out _);
            }

            return false;
        }

        public static double? ParseDouble(string? input)
        {
            double? number = null;

            if (double.TryParse(input, out double result))
            {
                number = result;
            }

            return number;
        }

        public static DateTime? ParseDateTime(string? input)
        {
            DateTime? dateTime = null;

            if (DateTime.TryParse(input, out DateTime result))
            {
                dateTime = result;
            }

            return dateTime;
        }
    }
}