using Dapper;
using KursovaBack.Models;
using Npgsql;
using project_back.DatabaseAccess.Interfaces;
using project_back.Models;
using System.Data;

namespace project_back.DatabaseAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";

        public async Task Create(ChatModel entity)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {

                    var sqlQuery = "insert into chats (id, user1_id,user2_id) VALUES (@Id, @user1id,@user2id)";

                    db.Execute(sqlQuery, entity);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ChatModel Get(Guid id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                return db.Query<ChatModel>("SELECT id, user1_id as user1id, user2_id as user2id ROM chats Where id=@id ", new { id }).ToList().FirstOrDefault();
            }
        }

        public async Task<List<ChatModel>> GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var chats = await db.QueryAsync<ChatModel>("SELECT id, user1_id as user1id, user2_id as user2id FROM chats");
                return chats.ToList();
            }
        }

        public async Task< List<ChatModel>> GetUserChats(Guid Id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try {
                    var chats = await db.QueryAsync<ChatModel>("SELECT Distinct id, user1_id as user1id, user2_id as user2id FROM chats where user1_id=@Id or user2_id=@Id", new { Id });
                    return chats.ToList();
                }
                catch(Exception ex)
                {
                    return new List<ChatModel> { };
                }
            }
        }

        public async Task< ChatModel> IsChatExists(Guid user1Id, Guid user2Id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var chats = await db.QueryAsync<ChatModel>("SELECT  id, user1_id as user1id, user2_id as user2id FROM chats where (user1_id=@user1id and user2_id=@user2Id) or (user2_id=@user1id and user1_id=@user2Id)", new { user1Id,user2Id  });
                return chats.ToList().FirstOrDefault();
            }
        }
    }
}
