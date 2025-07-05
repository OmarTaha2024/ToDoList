using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoList.Data.Entities.Identity;
using ToDoList.Data.Helpers;
using ToDoList.Infrustructure.Context;

namespace ToDoList.Infrustructure
{
    public static class ServiceRegisteration
    {
        public static async Task<IServiceCollection> AddServiceRegisterationAsync(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //});

            // jwt
            var jwtSettings = new JwtSettings();
            configuration.GetSection("Jwt").Bind(jwtSettings);

            services.AddSingleton(jwtSettings);

            services.Configure<OAuthSettings>(
          configuration.GetSection("OAuth"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
     .AddJwtBearer(x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidIssuer = jwtSettings.Issuer,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
             ValidAudience = jwtSettings.Audience,
             ValidateAudience = true,
             ValidateLifetime = true

         };
     });



            //            var yourIssuer = jwtSettings.Issuer;
            //            var yourAudience = jwtSettings.Audience;
            //            var yourKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

            //            // إعدادات Google
            //            var googleIssuer = "https://accounts.google.com";
            //            var googleAudience = "31707690009-htqfh8o2afu1gthta7ct34aj62lretqt.apps.googleusercontent.com";
            //            var googleKeys = await GoogleService.GetGoogleSecurityKeysAsync();
            //            services.AddAuthentication(options =>
            //            {
            //                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //            })
            //  .AddJwtBearer(options =>
            //  {
            //      options.RequireHttpsMetadata = false;
            //      options.SaveToken = true;
            //      options.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateIssuer = true,
            //          ValidateAudience = true,
            //          ValidateLifetime = true,
            //          ValidateIssuerSigningKey = true,
            //          IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
            //          {
            //              var keys = new List<SecurityKey>
            //              {
            //                yourKey
            //              };
            //              keys.AddRange(googleKeys);
            //              return keys;
            //          },
            //          ValidIssuers = new[] { yourIssuer, googleIssuer },
            //          ValidAudiences = new[] { yourAudience, googleAudience }
            //      };
            //  });





            return services;


        }
    }
}
