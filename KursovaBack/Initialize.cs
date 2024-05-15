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
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();


        }
        public static void InitializeServices(this IServiceCollection services)
        {
         
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();

            
        }
    }
}
