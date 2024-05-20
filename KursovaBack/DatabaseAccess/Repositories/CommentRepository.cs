using Dapper;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Microsoft.AspNetCore.Components;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace KursovaBack.DatabaseAccess.Repositories
{

    public class CommentRepository : ICommentRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";

         async System.Threading.Tasks.Task IBaseRepository<Comment>.Create(Comment entity)
        {
            entity.Id= Guid.NewGuid();
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {

                    var sqlQuery = "insert into comments (id, author_id,text,post_id) VALUES(@Id, @AuthorId,@Text,@PostId)";

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

        public Comment Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllByPost(Guid postId)
        {

            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var comments = await db.QueryAsync<Comment>("SELECT id, post_id as PostId, author_id as AuthorId , text as Text FROM comments where post_id = @postId", new {postId});
                return comments.ToList();
            }
        }
    }
}
