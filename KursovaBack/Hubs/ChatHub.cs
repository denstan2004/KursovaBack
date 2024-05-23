using Microsoft.AspNetCore.SignalR;
using project_back.DatabaseAccess.Interfaces;
using project_back.DatabaseAccess.Repositories;
using project_back.Models;
using System.Threading.Tasks;

namespace project_back.Hubs
{
    public interface IChatClient
    {
        Task RecieveMessage(string userId, string message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        public ChatHub(IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        public async Task JoinChat(string userId, string userId2)
        {
            try
            {
               ChatModel model= await _chatRepository.IsChatExists(Guid.Parse(userId), Guid.Parse(userId2));
                if (model == null)
                {
                    Guid chatRoomGuid = Guid.NewGuid();
                    ChatModel chatModel = new ChatModel();
                    chatModel.user1Id = Guid.Parse(userId);
                    chatModel.user2Id = Guid.Parse(userId2);
                    chatModel.Id = chatRoomGuid;
                    _chatRepository.Create(chatModel);
                    await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomGuid.ToString());
                    await Clients.Group(chatRoomGuid.ToString())
                                 .RecieveMessage("Admin", $"{userId} приєднався до чату {chatRoomGuid}");
                }
                else
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, model.Id.ToString());
                    await Clients.Group(model.Id.ToString())
                                 .RecieveMessage("Admin", $"{userId} приєднався до чату {model.Id}");
                }
            }
            catch (Exception ex)
            {
                // Логування помилки для діагностики
                Console.WriteLine($"Error in JoinChat: {ex.Message}");
                throw;
            }
        }

        public async Task SendMessage(string userId, string user2Id, string message)
        {
            var chatRoom= await _chatRepository.IsChatExists(Guid.Parse(userId), Guid.Parse(user2Id));
            Guid chatRoomId= chatRoom.Id;

            try
            {
                Guid chatRoomGuid = Guid.NewGuid();
                var chatMessage = new ChatMessage
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(userId),
                    ChatRoomId =chatRoomId,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };

                await _messageRepository.AddMessage(chatMessage);
                await Clients.Group(chatRoomId.ToString())
                             .RecieveMessage(userId, message);
            }
            catch (Exception ex)
            {
                // Логування помилки для діагностики
                Console.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }
    }
}
