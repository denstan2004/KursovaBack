using project_back.DatabaseAccess.Repositories;
using project_back.Models;

namespace project_back.DatabaseAccess.Interfaces
{
    public interface IMessageRepository
    {
        public Task<List<ChatMessage>> GetMessagesByChatRoomId(Guid chatRoomId);
        public Task AddMessage(ChatMessage message);

    }
}
