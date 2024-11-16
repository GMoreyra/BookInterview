
using System.Globalization;

namespace Api.Validators;
/// <summary>
/// Provides validation for publish date values.
/// </summary>
public static class PublishDateValidator
{
    /// <summary>
    /// Parses the date from the provided year, month, and day.
    /// </summary>
    /// <param name="year">The year of the date.</param>
    /// <param name="month">The month of the date.</param>
    /// <param name="day">The day of the date.</param>
    /// <returns>A DateTime object representing the parsed date, or null if the date could not be parsed.</returns>
    public static DateTime? ParsePublishDate(int? year, int? month, int? day)
    {
        if (year is null)
        {
            return null;
        }

        string dateFormat = "yyyy";
        string dateString = $"{year}";

        if (month is not null)
        {
            dateFormat += "-MM";
            dateString += $"-{month:D2}";

            if (day is not null)
            {
                dateFormat += "-dd";
                dateString += $"-{day:D2}";
            }
        }

        if (DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate;
        }

        return null;
    }
}
