using Dapper;
using Npgsql;
using project_back.DatabaseAccess.Interfaces;
using project_back.Models;
using System.Data;
using System.Data.Common;

namespace project_back.DatabaseAccess.Repositories
{
    public class MessageRepository:IMessageRepository
    {
     
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";
         
            

        public async Task<List<ChatMessage>> GetMessagesByChatRoomId(Guid chatRoomId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var query = "SELECT * FROM ChatMessages WHERE ChatRoomId = @ChatRoomId order by timestamp ";
               var messages=  await db.QueryAsync<ChatMessage>(query, new { ChatRoomId = chatRoomId });
                return messages.ToList();
            }
        }

        public async Task AddMessage(ChatMessage message)
        {
            
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var query = "INSERT INTO ChatMessages (Id, UserId, ChatRoomId, Message, Timestamp) VALUES (@Id, @UserId, @ChatRoomId, @Message, @Timestamp)";
                await db.ExecuteAsync(query, message);
            }
        }

        
    }
}

