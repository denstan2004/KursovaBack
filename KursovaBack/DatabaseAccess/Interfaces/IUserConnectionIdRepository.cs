using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IUserConnectionIdRepository
    {
        public Task Create(UserConnectionId entity);
        public Task<List<UserConnectionId>> GetAll();
        public Task<bool> Delete(string connectionId);
        Task<UserConnectionId> Get(string connectionId);

    }
}
