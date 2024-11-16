
using global::Api.Formatters;
using Xunit;

namespace Tests.Api;
public class PriceRangeFormatterTests
{
    [Theory]
    [InlineData(0.0, 0.0, "0&0")]
    [InlineData(0.0, null, "0")]
    [InlineData(null, 0.0, "0")]
    [InlineData(5.0, null, "5")]
    [InlineData(5.0, 10.0, "5&10")]
    [InlineData(null, null, null)]
    public void FormatPriceRange_ReturnsExpectedResult(double? minPrice, double? maxPrice, string? expected)
    {
        // Arrange & Act
        var result = PriceRangeFormatter.FormatPriceRange(minPrice, maxPrice);

        // Assert
        Assert.Equal(expected, result);
    }
}
