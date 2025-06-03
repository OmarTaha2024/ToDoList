

using Microsoft.AspNetCore.Http;

namespace ToDoList.Service.Resourceservice.Abstract
{
    public interface ILoggerService
    {
        public abstract void LogRequest(HttpContext context);
        public abstract void ModifyResponse(HttpContext context);
    }
}
