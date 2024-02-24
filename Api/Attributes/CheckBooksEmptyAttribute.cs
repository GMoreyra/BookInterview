using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;

namespace Api.Attributes;

/// <summary>
/// This attribute checks if the result of an action is an empty list of books.
/// If it is, it changes the result to a NotFoundResult.
/// </summary>
public class CheckBooksEmptyAttribute : ActionFilterAttribute
{
    /// <summary>
    /// This method is called after an action has been executed.
    /// </summary>
    /// <param name="context">The context of the action.</param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is IEnumerable<BookEntity> books && !books.Any())
        {
            context.Result = new NotFoundResult();
        }
    }
}
