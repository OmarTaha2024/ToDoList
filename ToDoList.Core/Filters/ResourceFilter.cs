using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList.Service.Resourceservice.Abstract;
namespace ToDoList.Core.Filters
{
    public class ResourceFilter : IResourceFilter
    {
        private readonly ILoggerService _loggerService;

        public ResourceFilter(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _loggerService.LogRequest(context.HttpContext);
            _loggerService.ModifyResponse(context.HttpContext);
            _loggerService.AddCookie(context.HttpContext);

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _loggerService.LogRequest(context.HttpContext);
        }
    }
}
