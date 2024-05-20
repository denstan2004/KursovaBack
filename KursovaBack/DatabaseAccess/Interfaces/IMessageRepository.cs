using KursovaBack.Models;

namespace KursovaBack.DatabaseAccess.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        public Task<List<Message>> GetHubMessages(Guid hubId);
    }
}
