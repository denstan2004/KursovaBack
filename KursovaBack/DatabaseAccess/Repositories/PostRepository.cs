using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Npgsql;
using static Dapper.SqlMapper;
using System.Data;

namespace KursovaBack.DatabaseAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";

        public System.Threading.Tasks.Task Create(Post entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Post Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPostsByProject(Guid projectId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {

                  //  var sqlQuery = "insert into projects (id, category,creator_id,name,description,analog,investment_amount,investment_money) VALUES(@Id, @Category,@CreatorId,@Name,@Description,@Analog,@InvestmentAmount,@InvestmentMoney)";

                    var posts =  db.Query<Post>("SELECT * FROM posts where project_id=@projectId", new {projectId});
                    return posts.ToList();
                }
                catch (Exception ex)
                {
                    return new List<Post>();
                }
            }
        }
    }
}
