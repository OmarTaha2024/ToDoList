using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using ToDoList.Core;
using ToDoList.Infrustructure;
using ToDoList.Infrustructure.Context;
using ToDoList.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7161")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


#region Dependency injection
builder.Services.AddModuleInfrustructureDependencies()
    .AddModuleServiceDependencies()
    .AddModuleCoreDependencies()
    .AddServiceRegisterationAsync(builder.Configuration);
#endregion

#region Localization
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("ar-EG")
        };

    options.DefaultRequestCulture = new RequestCulture("ar-EG");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion


var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//    string[] roles = { "Admin", "User" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }


//    string adminEmail = "admin@MobileShop.com";
//    string adminPassword = "Admin@123";

//    if (await userManager.FindByEmailAsync(adminEmail) == null)
//    {
//        var admin = new ApplicationUser
//        {
//            UserName = adminEmail,
//            Email = adminEmail,
//            FirstName = "Main",
//            LastName = "Admin",
//        };

//        var result = await userManager.CreateAsync(admin, adminPassword);
//        if (result.Succeeded)
//        {
//            await userManager.AddToRoleAsync(admin, "Admin");
//        }
//    }
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
# region Localization MiddleWare
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion
app.UseHttpsRedirection();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
