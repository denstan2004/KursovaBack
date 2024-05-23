using Microsoft.AspNetCore.Mvc;
using project_back.DatabaseAccess.Interfaces;
using project_back.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project_back.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        public MessagesController(IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        [HttpGet("GetAll")]
        public async Task<List<ChatMessage>> GetAllChatMessages([FromQuery] Guid userid1, [FromQuery] Guid userid2)
        {
            ChatModel chat = await _chatRepository.IsChatExists(userid1, userid2);
            if (chat != null)
            {
                var messages = await _messageRepository.GetMessagesByChatRoomId(chat.Id);
                return messages.ToList();
            }
            else return new List<ChatMessage>();
        }
    }
}
