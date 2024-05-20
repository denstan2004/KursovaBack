using KursovaBack.Models;
using KursovaBack.ResponceModels;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        List<Post> GetAllPostsByProject(Guid projectId);
        List<PostAuthorizedReturnModel> GetAllAsAuthorized (Guid projectId,Guid userId);
        void LikePost(Guid postId, Guid userId);
        Task<List<PostFullModel>> GetAllPostFullInfo(Guid projectId);
        int GetPostLikes(Guid postId);
    }
}
