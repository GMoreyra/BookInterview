namespace Api.Formatters;

/// <summary>
/// Formatter class for price range related operations.
/// </summary>
public static class PriceRangeFormatter
{
    /// <summary>
    /// Generates a string value based on the minimum and maximum prices.
    /// </summary>
    /// <param name="minPrice">The minimum price.</param>
    /// <param name="maxPrice">The maximum price.</param>
    /// <returns>A string containing the minimum and maximum prices if both are provided, the minimum price otherwise.</returns>
    public static string? FormatPriceRange(double? minPrice, double? maxPrice)
    {
        if (minPrice.HasValue && maxPrice.HasValue)
        {
            return $"{minPrice.Value}&{maxPrice.Value}";
        }

        return minPrice?.ToString() ?? maxPrice?.ToString();
    }
}
