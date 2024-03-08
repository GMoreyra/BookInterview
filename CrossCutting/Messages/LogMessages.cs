namespace CrossCutting.Messages;

/// <summary>
/// Provides log messages.
/// </summary>
public static class LogMessages
{
    public const string ErrorQueryLogMessage = "An error occurred while fetching the last ID.";
    public const string ErrorSavingLogMessage = "An error occurred while saving the book: {SaveBook}.";
    public const string ErrorFindBookLogMessage = "An error occurred while searching for the book with ID: {FindBookId}.";
    public const string ErrorUpdatingLogMessage = "An error occurred while updating the book: {UpdateBook}.";
}
