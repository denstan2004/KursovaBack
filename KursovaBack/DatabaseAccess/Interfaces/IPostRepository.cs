using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        List<Post> GetAllPostsByProject(Guid projectId);

    }
}
