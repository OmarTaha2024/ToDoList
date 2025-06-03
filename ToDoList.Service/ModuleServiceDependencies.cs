using Microsoft.Extensions.DependencyInjection;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Resourceservice.Abstract;
using ToDoList.Service.Resourceservice.Implementation;
using ToDoList.Service.Services;

namespace ToDoList.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddModuleServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IToDoItemService, ToDoItemService>();
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddScoped<ILoggerService, LoggerService>();

            return services;
        }
    }
}
