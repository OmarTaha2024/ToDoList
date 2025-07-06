using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ToDoList.Core.Filters
{
    public class HybridAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
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
            if (ValidateMyOwnJwt(token))
            {
                return;
            }

            using var client = new HttpClient();
            var resp = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={token}");
            if (resp.IsSuccessStatusCode)
            {
                return;
            }
            context.Result = new UnauthorizedResult();
        }

        private bool ValidateMyOwnJwt(string token)
        {
            try
            {

                var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "TODOLISTApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("TODOLIST-System-Clean-Architecture-key")),
                    ValidAudience = "TODOLISTApiUsers",
                    ValidateAudience = true,
                    ValidateLifetime = true

                };

                SecurityToken validatedToken;
                var principal = handler.ValidateToken(token, validationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
