using KursovaBack.Models;
using KursovaBack.ResponceModels;
using KursovaBack.ViewModels;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        void AddUserToProject(AddProjectUser model);
        void DeleteUserById(Guid Id);
        Task<List<ProjectMemberModel>> GetAllByProject(Guid projectId);
        public Task<List<Project>> GetAllByCategory(string category);
        public List<string> GetAllCategories();
        public List<Project> GetUserProjects(Guid userId);
    }
}
