using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrustructure.Abstract;
using ToDoList.Infrustructure.InfrustructureBases;
using ToDoList.Infrustructure.Represatories;

namespace ToDoList.Infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddModuleInfrustructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IToDoItemRepository, ToDoItemRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
