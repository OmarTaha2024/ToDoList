using Microsoft.AspNetCore.Http;
using ToDoList.Service.Resourceservice.Abstract;

namespace ToDoList.Service.Resourceservice.Implementation
{
    public class LoggerService : ILoggerService
    {
        public void AddCookie(HttpContext context)
        {
            context.Response.Cookies.Append("MyCookie", "CookieValue");
        }

        public void LogRequest(HttpContext context)
        {
            var info = $"{DateTime.Now} | Request: {context.Request.Method} {context.Request.Path}";

            //File.AppendAllText("requests.log", info + Environment.NewLine);
        }

        public void ModifyResponse(HttpContext context)
        {
            context.Response.Headers.Add("X-Resource-Filter", "ModifiedByResourceFilter");
        }
    }
}
