namespace Utils;

public static class PriceHelper
{
    public static string? ValidatePrices(double? minPrice, double? maxPrice)
    {
        if (minPrice.HasValue && minPrice < 0)
        {
            return "Minimum price must be positive.";
        }

        if (maxPrice.HasValue && maxPrice < 0)
        {
            return "Maximum price must be positive.";
        }

        if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
        {
            return "Minimum price cannot be greater than maximum price.";
        }

        return null;
    }

    public static string? GenerateValue(double? minPrice, double? maxPrice)
    {
        string? value = null;

        if (minPrice.HasValue && !maxPrice.HasValue)
        {
            value = minPrice.Value.ToString();
        }

        if (minPrice.HasValue && maxPrice.HasValue)
        {
            value = $"{minPrice.Value}&{maxPrice.Value}";
        }

        return value;
    }
}
