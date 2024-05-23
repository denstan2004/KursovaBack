using Dapper;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Npgsql;
using System.Data;

namespace KursovaBack.DatabaseAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";
         async System.Threading.Tasks.Task IBaseRepository<User>.Create(User user)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var sqlQuery = "INSERT INTO Users (id, username,password,role,firstname,lastname,skills,education,expirience,investment_info,avatar ) VALUES(@id, @username,@password,@role,@firstname,@lastname,@skills,@education,@expirience,@investment_info,@avatar)";
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
                    var sqlQuery = "Delete From users where id=@id";
                    db.Execute(sqlQuery, id);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public User Get(Guid id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                return  db.Query<User>("Select * FROM Users Where id=@id ", new { id }).ToList().FirstOrDefault();
            }
        }




        public async Task<List<User>> GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var users = await db.QueryAsync<User>("SELECT * FROM Users");
                return users.ToList();
            }
        }

        public async void Update(User user)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlQuery = @"
            UPDATE public.users 
            SET 
                username = @username, 
                password = @password, 
                firstname = @firstname, 
                lastname = @lastname, 
                skills = @skills, 
                education = @education, 
                expirience = @expirience, 
                investment_info = @investment_info, 
                avatar = @avatar 
            WHERE id = @id;";

                var parameters = new
                {
                    username = user.username,
                    password = user.password,
                    firstname = user.firstname,
                    lastname = user.lastname,
                    skills = user.skills,
                    education = user.education,
                    expirience = user.expirience,
                    investment_info = user.investment_info,
                    avatar = user.Avatar,
                    id = user.id
                };

                await db.ExecuteAsync(sqlQuery, parameters);
            }
        }


        public User GetByName(string name)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                return db.Query<User>("Select * FROM Users Where username=@name ", new { name }).ToList().FirstOrDefault();
            }
        }

        public List<User> GetByNamePrefix(string namePrefix)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users WHERE username LIKE CONCAT(@namePrefix, '%')", new { namePrefix }).ToList();
            }
        }


    }
}
