

using Microsoft.AspNetCore.Http;

namespace ToDoList.Service.Resourceservice.Abstract
{
    public interface ILoggerService
    {
        public void LogRequest(HttpContext context);
        public void ModifyResponse(HttpContext context);
        public void AddCookie(HttpContext context);
    }
}
