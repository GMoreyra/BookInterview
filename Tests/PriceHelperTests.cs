using Utils;

namespace Tests;

public class PriceHelperTests
{
    [Theory]
    [InlineData(-1.0, null, "Minimum price must be positive.")]
    [InlineData(null, -1.0, "Maximum price must be positive.")]
    [InlineData(10.0, 5.0, "Minimum price cannot be greater than maximum price.")]
    [InlineData(5.0, 5.0, null)]
    [InlineData(null, null, null)]
    public void ValidatePrices_ReturnsExpectedResult(double? minPrice, double? maxPrice, string? expected)
    {
        var result = PriceHelper.ValidatePrices(minPrice, maxPrice);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(5.0, null, "5")]
    [InlineData(5.0, 10.0, "5&10")]
    [InlineData(null, null, null)]
    public void GenerateValue_ReturnsExpectedResult(double? minPrice, double? maxPrice, string? expected)
    {
        var result = PriceHelper.GenerateValue(minPrice, maxPrice);
        result.Should().Be(expected);
    }
}