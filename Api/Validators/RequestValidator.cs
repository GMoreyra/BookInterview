using Api.Contracts;
using CrossCutting.Messages;
using System.Globalization;
using System.Text;

namespace Api.Validators;

/// <summary>
/// Provides methods to validate book requests.
/// </summary>
public static class RequestValidator
{
    /// <summary>
    /// Validates the given book request.
    /// </summary>
    /// <param name="request">The book request to validate.</param>
    /// <returns>A string containing error messages if the request is invalid, null otherwise.</returns>
    public static string? ValidateRequest<TRequest>(TRequest request) where TRequest : class, IBookRequest
    {
        StringBuilder message = new();

        if (!DateTime.TryParse(request.PublishDate, CultureInfo.InvariantCulture, out DateTime publishDate))
        {
            message.AppendLine(ErrorMessages.PublishDateErrorMessage);
        }

        if (!double.TryParse(request.Price, out double price) || price < 0)
        {
            message.AppendLine(ErrorMessages.PriceErrorMessage);
        }

        return message.Length > 0 ? message.ToString() : null;
    }
}
