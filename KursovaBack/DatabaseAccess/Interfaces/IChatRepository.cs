using KursovaBack.DatabaseAccess.Interfaces;
using project_back.Models;

namespace project_back.DatabaseAccess.Interfaces
{
    public interface IChatRepository :IBaseRepository<ChatModel>
    {
        public Task<List<ChatModel>> GetUserChats(Guid userId);
        Task<ChatModel> IsChatExists(Guid user1Id,Guid user2Id);
    }
}
