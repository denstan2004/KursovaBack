using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.DatabaseAccess.Repositories;
using KursovaBack.Services.Implementations;
using KursovaBack.Services.Interfaces;

namespace KursovaBack
{
    public static class Initialize
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        
        }
        public static void InitializeServices(this IServiceCollection services)
        {
         
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();

            
        }
    }
}
