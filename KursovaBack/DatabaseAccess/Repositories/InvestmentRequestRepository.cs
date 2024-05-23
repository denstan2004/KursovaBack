using Dapper;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Npgsql;
using project_back.DatabaseAccess.Interfaces;
using project_back.Models;
using System.Data;

namespace project_back.DatabaseAccess.Repositories
{
    public class InvestmentRequestRepository : IInvestmentRequestRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";

        public async void ChangeStatus(Guid requestId, string status)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlQuery = @"
            UPDATE public.investmentrequest 
            SET 
                status=@status
                
            WHERE id = @id;";

                var parameters = new
                {
                    status = status,
                    id = requestId
                };

                await db.ExecuteAsync(sqlQuery, parameters);
            }
        }

        public async Task Create(InvestmentRequest entity)
        {
            entity.Id = Guid.NewGuid();
            entity.Status = "pending";
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {

                    var sqlQuery = "insert into InvestmentRequest (id, user_id,project_id,message,contact_data,status) VALUES(@Id, @userid,@ProjectId,@message,@contactdata,@status)";

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

        public InvestmentRequest Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InvestmentRequest>> GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var posts = await db.QueryAsync<InvestmentRequest>("SELECT id, user_id as userid,project_id as ProjectId,message,contact_data as contactdata,status FROM InvestmentRequest ");
                return posts.ToList();
            }
        }

        public async Task<List<InvestmentRequest>> GetAllByProject(Guid projectId)
        {
            string pending = "pending";
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var posts =  await db.QueryAsync<InvestmentRequest>("SELECT id, user_id as userid,project_id as ProjectId,message,contact_data as contactdata,status FROM InvestmentRequest where status = @pending", new {pending});
                return posts.ToList();
            }
        }
    }
}
