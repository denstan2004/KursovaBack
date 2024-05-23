using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.DatabaseAccess.Repositories;
using KursovaBack.Services.Implementations;
using KursovaBack.Services.Interfaces;
using project_back.DatabaseAccess.Interfaces;
using project_back.DatabaseAccess.Repositories;

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
            services.AddScoped<IInvestmentRequestRepository, InvestmentRequestRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();





        }
        public static void InitializeServices(this IServiceCollection services)
        {
         
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();

            
        }
    }
}
