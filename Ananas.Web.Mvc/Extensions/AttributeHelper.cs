using Microsoft.AspNetCore.Mvc.Filters;

namespace Ananas.Web.Mvc.Extensions
{
    public class AttributeHelper : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}