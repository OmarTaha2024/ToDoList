using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoList.Core.Filters
{
    public class GoogleAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length);

            using var client = new HttpClient();
            var resp = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={token}");
            if (!resp.IsSuccessStatusCode)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

}
