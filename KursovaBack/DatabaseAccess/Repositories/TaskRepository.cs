using Dapper;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Npgsql;
using System.Data;

namespace KursovaBack.DatabaseAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";

        public void ChangeStatus(Guid Task, string status)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlQuery = "Update tasks set status=@status where id=@Task";
                db.Execute(sqlQuery, new {Task, status});
            }
        }
        public async Task<List<PersonTask>> GetAllTaskByProject(Guid projectId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var tasks = await db.QueryAsync<PersonTask>("SELECT id, title, message, date , author_id as AuthorId, user_id as UserId, project_id as ProjectId , expired_date as ExpiredDate,status  FROM Tasks Where project_id=@projectId", new {projectId});
                return tasks.ToList();
            }
        }

        public async Task<List<PersonTask>> GetAllTaskByUser(Guid projectId, Guid userId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var tasks = await db.QueryAsync<PersonTask>("SELECT id, title, message, date , author_id as AuthorId, user_id as UserId, project_id as ProjectId , expired_date as ExpiredDate, status  FROM Tasks Where project_id=@projectId and user_id=@userId", new { projectId,userId });
                return tasks.ToList();
            }
        }

        async System.Threading.Tasks.Task IBaseRepository<PersonTask>.Create(PersonTask user)
        {
            user.Date=DateTime.Now;
            user.Id= Guid.NewGuid();
            user.Status = "running";
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var sqlQuery = "INSERT INTO tasks (id, title, message, date, author_id, user_id, project_id , expired_date,status  ) VALUES(@id, @title, @message, @date , @AuthorId, @UserId, @ProjectId, @ExpiredDate, @status)";
                    db.Execute(sqlQuery, user);

                }
                catch (Exception ex)
                {

                }
            }
        }

        public bool Delete(Guid id)
        {

            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var sqlQuery = "Delete From tasks where id=@id";
                    db.Execute(sqlQuery, id);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        PersonTask IBaseRepository<PersonTask>.Get(Guid id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                return db.Query<PersonTask>("Select id, title, message, date , author_id as AuthorId, user_id as UserId, project_id as ProjectId , expired_date as ExpiredDate FROM Tasks Where id=@id ", new { id }).ToList().FirstOrDefault();
            }
        }

        async Task<List<PersonTask>> IBaseRepository<PersonTask>.GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var tasks = await db.QueryAsync<PersonTask>("SELECT id, title, message, date , author_id as AuthorId, user_id as UserId, project_id as ProjectId , expired_date as ExpiredDate  FROM Tasks");
                return tasks.ToList();
            }
        }
    }
}
