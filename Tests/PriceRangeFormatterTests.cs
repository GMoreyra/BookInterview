using Api.Formatters;
using Xunit;

namespace Tests;

public class PriceRangeFormatterTests
{
    [Theory]
    [InlineData(5.0, null, "5")]
    [InlineData(5.0, 10.0, "5&10")]
    [InlineData(null, null, null)]
    public void FormatPriceRange_ReturnsExpectedResult(double? minPrice, double? maxPrice, string? expected)
    {
        var result = PriceRangeFormatter.FormatPriceRange(minPrice, maxPrice);

        Assert.Equal(expected, result);
    }
}