using KursovaBack.Models;
using KursovaBack.ViewModels;

namespace KursovaBack.Services.Interfaces
{
    public interface IProjectService
    {
        bool CreateProject (ProjectCreateViewModel project, Guid userId);
    }
}
