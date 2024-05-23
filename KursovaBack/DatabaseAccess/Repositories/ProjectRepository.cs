using Dapper;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using KursovaBack.Models.Enums;
using KursovaBack.ResponceModels;
using KursovaBack.ViewModels;
using Npgsql;
using System.Data;
using System.Reflection;

namespace KursovaBack.DatabaseAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";

         async System.Threading.Tasks.Task IBaseRepository<Project>.Create(Project entity)
        {
            entity.Id= Guid.NewGuid();
            entity.InvestmentAmount = 0;
            using (IDbConnection db = new NpgsqlConnection(connectionString)) 
            {
                try
                {
                     
                    var sqlQuery = "insert into projects (id, category,creator_id,name,description,analog,investment_amount,investment_money,image) VALUES(@Id, @Category,@CreatorId,@Name,@Description,@Analog,@InvestmentAmount,@InvestmentMoney,@image)";

                    db.Execute(sqlQuery, entity);
                    sqlQuery = " insert into project_user (user_id, project_role, project_id) values  (@CreatorId, 1, @Id)";
                    db.Execute(sqlQuery,entity);
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
                    var sqlQuery = "DELETE * FROM projects WHERE id=@id";
                    db.Execute(sqlQuery, id);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public Project Get(Guid id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
               
                    return db.Query<Project>("Select  name,investment_money as InvestmentMoney,description,analog,investment_amount as InvestmentAmount,creator_id as creatorid, id , category,image FROM Projects Where id=@id ", new { id }).ToList().FirstOrDefault();
                
            }
        }

        public async Task<List<Project>> GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var projects = await db.QueryAsync<Project>("SELECT name,investment_money as InvestmentMoney,description,analog,investment_amount as InvestmentAmount,creator_id as creatorid, id , category, image FROM Projects");
                return projects.ToList();
                
            }
        }
        public void AddUserToProject(AddProjectUser model)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    int role = 1;
                    string sqlQuery = "Insert into project_user (user_id, project_id, project_role) values (@userId, @projectId, @role)";
                    db.Execute(sqlQuery, new { userId = model.userId, projectId = model.projectId, role = model.role });

                }
                catch (Exception ex)
                {

                }
            }
        }
        public void DeleteUserById(Guid Id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    
                    string sqlQuery = "DELETE FROM project_user WHERE id=@Id";
                    db.Execute(sqlQuery, Id);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task< List<ProjectMemberModel>> GetAllByProject(Guid projectId)
        {

            using(IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    
                   string sqlQuery = "select username, password, firstname, lastname, skills, education, expirience, investment_info, id, role, project_role, avatar from users join project_user on  users.id = project_user.user_id where project_user.project_id=@projectId;";
                   var projects= await  db.QueryAsync<ProjectMemberModel>(sqlQuery, new { projectId }) ;
                   return projects.ToList();
                }
                catch (Exception ex)
                {
                    return null;

                }
            }
        }

        public async Task<List<Project>> GetAllByCategory(string category)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {

                var projects = await db.QueryAsync<Project>("SELECT  name,investment_money as InvestmentMoney,description,analog,investment_amount as InvestmentAmount,creator_id as creatorid, id , category, image FROM Projects WHERE category=@category", new {category});
                return projects.ToList();

            }
        }

        public List<string> GetAllCategories()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {

                var projects =  db.Query<string>("SELECT distinct category FROM Projects ").ToList();
                return projects;

            }
        }

        public List<Project> GetUserProjects(Guid userId)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {

                var projects = db.Query<Project>(" SELECT p.name, p.investment_money AS InvestmentMoney, p.description, p.analog, p.investment_amount AS InvestmentAmount, p.creator_id AS creatorid, p.id, p.category, p.image FROM project_user pu JOIN projects p ON pu.project_id = p.id WHERE pu.user_id = @userId ", new { userId }).ToList();
                return projects;
         
            }
        }
    }
}
