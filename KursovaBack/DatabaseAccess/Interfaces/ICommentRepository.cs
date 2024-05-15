using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface ICommentRepository:IBaseRepository<Comment>
    {
        public Task<List<Comment>> GetAllByPost(Guid postId);
    }
}
