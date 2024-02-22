﻿namespace Utils;

/// <summary>
/// Helper class for price related operations.
/// </summary>
public static class PriceHelper
{
    /// <summary>
    /// Generates a string value based on the minimum and maximum prices.
    /// </summary>
    /// <param name="minPrice">The minimum price.</param>
    /// <param name="maxPrice">The maximum price.</param>
    /// <returns>A string containing the minimum and maximum prices if both are provided, the minimum price otherwise.</returns>
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
