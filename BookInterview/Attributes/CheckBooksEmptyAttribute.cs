using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;

namespace Api.Attributes
{
    public class CheckBooksEmptyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is IEnumerable<BookEntity> books && !books.Any())
            {
                context.Result = new NotFoundResult();
            }
        }
    }

}
