using KursovaBack.Models;
using KursovaBack.ViewModels;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IChatHubRepository : IBaseRepository<ChatHub>
    {
        public Task Update(ChatHub chatHub);

        public Task<ChatHub> FindHubByUsers(Guid user1Id, Guid user2Id);

        public Task<List<ChatHubResponseModel>> GetAllUserHubs(Guid userId);

        public Task<ChatHub> GetAsync(Guid id);

    }
}
