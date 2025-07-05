using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoList.Data.Entities.Identity;
using ToDoList.Data.Helpers;
using ToDoList.Data.Results;
using ToDoList.Infrustructure.Abstract;
using ToDoList.Service.Abstracts;
using static ToDoList.Data.Results.JwtAuthResult;

namespace ToDoList.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _RefreshToken;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        #endregion
        #region  Ctor
        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, JwtSettings jwtSettings, IRefreshTokenRepository RefreshToken)
        {
            _jwtSettings = jwtSettings;
            _RefreshToken = RefreshToken;
            _userManager = userManager;
            _signinManager = signinManager;
        }
        #endregion
        public async Task<JwtAuthResult> GetJWTToken(ApplicationUser user)
        {
            var AccessToken = GenerateJWTToken(user);

            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMinutes(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                RefreshToken = refreshToken.TokenString,
                Token = AccessToken,
                UserId = user.Id
            };
            await _RefreshToken.AddAsync(userRefreshToken);

            var response = new JwtAuthResult();
            response.AccessToken = AccessToken;
            response.refreshToken = refreshToken;
            return response;
        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private string GenerateJWTToken(ApplicationUser user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }
        private List<Claim> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.FirstName), user.FirstName),
                new Claim(nameof(UserClaimModel.LastName), user.LastName),
            };
            return claims;
        }

        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> ValidateToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "Not Found";
            }
            var result = await _signinManager.PasswordSignInAsync(
                        email, password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {

                return "Success";
            }
            var error = string.Join("-", result.IsNotAllowed);
            return error;
        }
    }
}
