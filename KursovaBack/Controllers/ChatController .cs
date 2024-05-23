using Microsoft.AspNetCore.Mvc;
using project_back.DatabaseAccess.Interfaces;
using project_back.DatabaseAccess.Repositories;
using project_back.Models;

namespace project_back.Controllers
{
    [Route("api/chat")]
    public class ChatController:Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;


        public ChatController(IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        [HttpGet]
        [Route("GetAll/{userId}")]
        public async Task<List<ChatModel>> GetUserChats(Guid userId)
        {
          var Chats=  await _chatRepository.GetUserChats(userId);
            return Chats;
        }

        
    }
}
