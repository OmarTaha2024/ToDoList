using System.IdentityModel.Tokens.Jwt;
using ToDoList.Data.Entities.Identity;
using ToDoList.Data.Results;

namespace ToDoList.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(string email, string password);
        public Task<JwtAuthResult> GetJWTToken(ApplicationUser user);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
    }
}
