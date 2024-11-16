
using System.Globalization;
using System.Linq.Expressions;

namespace CrossCutting.Exceptions;
/// <summary>
/// Provides helper methods for argument validation.
/// </summary>
public static class Argument
{
    private const string NullValidationExpressionMessage = "The validation expression cannot be null.";
    private const string NotMemberAccessExpressionMessage = "Expression is not a member access.";
    private const string ArgumentCannotBeNullMessageFormat = "Argument '{0}' cannot be null.";

    /// <summary>
    /// Throws an ArgumentNullException if the result of the specified lambda expression evaluates to null.
    /// Automatically extracts the parameter name from the lambda expression.
    /// </summary>
    /// <typeparam name="T">The result type of the expression.</typeparam>
    /// <param name="expression">The lambda expression to evaluate, typically a reference to a variable or parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown when the result of the expression is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the expression is not a member access expression.</exception>
    public static void ThrowIfNull<T>(Expression<Func<T>> expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression), NullValidationExpressionMessage);

        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException(NotMemberAccessExpressionMessage, nameof(expression));

        var evaluateExpression = expression.Compile();
        var evaluatedValue = evaluateExpression();

        if (evaluatedValue is null)
        {
            string name = memberExpression.Member.Name;
            throw new ArgumentNullException(memberExpression.Member.Name, string.Format(CultureInfo.InvariantCulture, ArgumentCannotBeNullMessageFormat, name));
        }
    }
}
