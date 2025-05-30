﻿using Microsoft.Extensions.DependencyInjection;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Services;

namespace ToDoList.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddModuleServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IToDoItemService, ToDoItemService>();

            return services;
        }
    }
}
