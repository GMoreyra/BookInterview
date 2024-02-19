using System.Globalization;

namespace Utils;

/// <summary>
/// Helper class for parsing publish dates.
/// </summary>
public static class PublishDateHelper
{
    /// <summary>
    /// Parses the date from the provided year, month, and day.
    /// </summary>
    /// <param name="year">The year of the date.</param>
    /// <param name="month">The month of the date.</param>
    /// <param name="day">The day of the date.</param>
    /// <returns>A DateTime object representing the parsed date, or null if the date could not be parsed.</returns>
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
