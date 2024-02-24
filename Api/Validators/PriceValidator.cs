namespace Api.Validators;

/// <summary>
/// Provides validation for price values.
/// </summary>
public static class PriceValidator
{
    private const string MinPriceNegativeError = "Minimum price must be positive.";
    private const string MaxPriceNegativeError = "Maximum price must be positive.";
    private const string MinPriceGreaterThanMaxError = "Minimum price cannot be greater than maximum price.";

    /// <summary>
    /// Validates the minimum and maximum prices.
    /// </summary>
    /// <param name="minPrice">The minimum price. Must be positive.</param>
    /// <param name="maxPrice">The maximum price. Must be positive and greater than or equal to minPrice.</param>
    /// <returns>A string containing an error message if the prices are invalid, null otherwise.</returns>
    public static string? ValidatePrices(double? minPrice, double? maxPrice)
    {
        if (minPrice.HasValue && minPrice < 0)
        {
            return MinPriceNegativeError;
        }

        if (maxPrice.HasValue && maxPrice < 0)
        {
            return MaxPriceNegativeError;
        }

        if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
        {
            return MinPriceGreaterThanMaxError;
        }

        return null;
    }
}
