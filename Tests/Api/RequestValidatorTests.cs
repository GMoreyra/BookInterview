using Api.Contracts.CreateBook;
using Api.Validators;
using CrossCutting.Messages;
using Xunit;

namespace Tests.Api;

public class RequestValidatorTests
{
    [Fact]
    public void ValidateRequest_InvalidPublishDate_ReturnsErrorMessage()
    {
        // Arrange
        var request = new CreateBookRequest
        (
            Author: "John Doe",
            Description: "A thrilling mystery novel",
            Genre: "Mystery",
            Title: "The Mysterious Event",
            PublishDate: "invalid date",
            Price: "15.99"
        );

        // Act
        var result = RequestValidator.ValidateRequest(request);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(ErrorMessages.PublishDateErrorMessage, result, StringComparison.InvariantCulture);
    }

    [Fact]
    public void ValidateRequest_InvalidPrice_ReturnsErrorMessage()
    {
        // Arrange
        var request = new CreateBookRequest
        (
            Author: "John Doe",
            Description: "A thrilling mystery novel",
            Genre: "Mystery",
            Title: "The Mysterious Event",
            PublishDate: "2022-01-01",
            Price: "invalid price"
        );

        // Act
        var result = RequestValidator.ValidateRequest(request);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(ErrorMessages.PriceErrorMessage, result, StringComparison.InvariantCulture);
    }

    [Fact]
    public void ValidateRequest_ValidRequest_ReturnsNull()
    {
        // Arrange
        var request = new CreateBookRequest
        (
            Author: "John Doe",
            Description: "A thrilling mystery novel",
            Genre: "Mystery",
            Title: "The Mysterious Event",
            PublishDate: "2022-01-01",
            Price: "10"
        );

        // Act
        var result = RequestValidator.ValidateRequest(request);

        // Assert
        Assert.Null(result);
    }
}
