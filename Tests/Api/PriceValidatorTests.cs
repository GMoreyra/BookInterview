
using global::Api.Validators;
using Xunit;

namespace Tests.Api;
public class PriceValidatorTests
{
    [Theory]
    [InlineData(-1.0, null, "Minimum price must be positive.")]
    [InlineData(-1.0, 10.0, "Minimum price must be positive.")]
    [InlineData(null, -1.0, "Maximum price must be positive.")]
    [InlineData(10.0, -1.0, "Maximum price must be positive.")]
    [InlineData(10.0, 5.0, "Minimum price cannot be greater than maximum price.")]
    [InlineData(5.0, 5.0, null)]
    [InlineData(0.0, null, null)]
    [InlineData(null, 0.0, null)]
    [InlineData(0.0, 0.0, null)]
    [InlineData(null, null, null)]
    public void ValidatePrices_ReturnsExpectedResult(double? minPrice, double? maxPrice, string? expected)
    {
        // Arrange & Act
        var result = PriceValidator.ValidatePrices(minPrice, maxPrice);

        // Assert
        Assert.Equal(expected, result);
    }
}
