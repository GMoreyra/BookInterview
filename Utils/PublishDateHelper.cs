using System.Globalization;

namespace Utils;

public static class PublishDateHelper
{
    public static DateTime? ParseDate(int? year, int? month, int? day)
    {
        string? dateFormat = null;
        string? dateString = null;

        if (year is not null && month is null && day is null)
        {
            dateFormat = "yyyy";
            dateString = $"{year}";
        }
        else if (year is not null && month is not null && day is null)
        {
            dateFormat = "yyyy-MM";
            dateString = $"{year}-{month:D2}";
        }
        else if (year is not null && month is not null && day is not null)
        {
            dateFormat = "yyyy-MM-dd";
            dateString = $"{year}-{month:D2}-{day:D2}";
        }

        if (DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate;
        }

        return null;
    }
}
