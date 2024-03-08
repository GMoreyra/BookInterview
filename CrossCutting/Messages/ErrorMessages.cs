namespace CrossCutting.Messages;

/// <summary>
/// Provides error messages.
/// </summary>
public static class ErrorMessages
{
    public const string PublishDateErrorMessage = "The provided date is not valid. Please ensure the date is in the correct format.";
    public const string PriceErrorMessage = "The provided price is not valid. It should be a number greater than or equal to zero.";
    public const string PriceRangeErrorMessage = "The provided price range is not valid. Please ensure the minimum price is less than the maximum price.";
    public const string CreateBookErrorMessage = "Failed to create a Book. Please check the provided details and try again.";
    public const string UpdateBookNotFoundErrorMessage = "The book to be updated could not be found. Please check the provided ID.";
}
