using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface ITaskRepository : IBaseRepository<KursovaBack.Models.PersonTask>
    {
       Task< List<PersonTask>> GetAllTaskByProject(Guid projectId);
       Task<List<PersonTask>> GetAllTaskByUser(Guid projectId,Guid userId);
       void ChangeStatus(Guid Task, string status);

    }
}
