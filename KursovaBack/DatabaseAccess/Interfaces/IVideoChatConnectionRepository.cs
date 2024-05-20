using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IVideoChatConnectionRepository
    {
        public Task Create(VideoChatConnection entity);

        public Task<bool>? Delete(string connectioId);

        public Task<VideoChatConnection> Get(string connectionId);

        public Task<List<VideoChatConnection>> GetAll();

    }
}